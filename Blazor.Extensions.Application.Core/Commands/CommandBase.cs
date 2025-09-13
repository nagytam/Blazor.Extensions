using Blazor.Extensions.Application.Core.Controls;
using Blazor.Extensions.Application.Core.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Blazor.Extensions.Application.Core.Commands;

public abstract class CommandBase(IRefreshable? control)
{
    #region Id
    public virtual string Id => GetType()?.FullName ?? nameof(CommandBase);
    #endregion

    #region Icon
    public string IconCss => !IsInProgress ? Icon : ProgressIcon;
    private string _cssClass = string.Empty;
    public virtual string CssClass
    {
        get
        {
            if (IsInProgress)
            {
                return "e-danger";
            }
            else
            {
                return _cssClass;
            }
        }
        protected set
        {
            _cssClass = value;
        }
    }
    protected virtual string Icon => "fa-solid fa-lock";
    protected string ProgressIcon => !IsCancelled ? "fa-solid fa-xmark" : "fa fa-spinner fa-spin";
    public bool IsInProgress { get; protected set; }

    protected bool IsCancelled => IsInProgress &&
                                    _cancellationToken != null &&
                                    _cancellationToken.Value.IsCancellationRequested;
    #endregion

    #region Content
    public string Content => !IsInProgress ? Label : ProgressLabel;
    public virtual string Label => "LABEL";
    protected string ProgressLabel => !IsCancelled ? $"CANCEL [{Stopwatch.ElapsedMilliseconds / 1000} seconds]" : "CANCELLED";
    #endregion

    #region HotKey
    protected virtual string HotKey { get; set; } = string.Empty;
    public string AccessKey
    {
        get
        {
            if (IsInProgress && !IsCancelled)
            {
                return "Esc";
            }
            else
            {
                return HotKey.ToUpperInvariant();
            }
        }
    }
    #endregion

    #region Tooltip
    public virtual string Title => string.Empty;
    public string GetHotKeyTooltip()
    {
        if (!string.IsNullOrEmpty(AccessKey))
        {
            return Environment.NewLine + "HotKey: Alt+" + AccessKey;
        }
        return string.Empty;
    }
    #endregion

    #region IsDisabled
    public bool IsDisabled => IsCancelled || !IsExecutable;
    public virtual bool IsExecutable => true;
    #endregion

    #region IsPrimary
    public virtual bool IsPrimary => false;
    #endregion

    #region IsVisible
    public virtual bool IsVisible => true;
    #endregion

    #region Constructor
    public IRefreshable? Control = control;
    #endregion

    #region OnClickAsync & StartExecuteAsync & OnExecuteAsync
    private Task? _task = null;
    private CancellationTokenSource? _cancellationTokenSource = null;
    private CancellationToken? _cancellationToken = null;
    protected Stopwatch Stopwatch { get; set; } = new Stopwatch();

    public async Task OnClickAsync(MouseEventArgs _)
    {
        if (!IsInProgress)
        {
            IsInProgress = true;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            if (_task == null)
            {
                _task = Task.Run(StartExecuteAsync);
            }
        }
        else
        {
            if (_cancellationTokenSource != null)
            {
                await _cancellationTokenSource.CancelAsync();
                //Control?.Logger.LogInformation("Command {Id} OnClickAsync cancellation was requested", Id);
            }
        }
    }

    private async Task StartExecuteAsync()
    {
        var periodicTask = new PeriodicTask(() => { Control?.Refresh(); }, new TimeSpan(0, 0, 0, 0, 100));
        try
        {
            //Control?.Logger.LogInformation("Command {Id} OnClickAsync started", Id);
            Stopwatch.StartNew();
            Stopwatch.Start();
            // ensure the periodic refresh uses the same cancellation token and can stop
            if (_cancellationToken != null)
            {
                periodicTask.CancellationToken = _cancellationToken.Value;
            }
            periodicTask.IsActive = true;
            if (_cancellationToken != null)
            {
                await OnExecuteAsync(_cancellationToken.Value);
            }
            if (_cancellationToken != null && !_cancellationToken.Value.IsCancellationRequested)
            {
                //Control?.Logger.LogInformation("Command {Id} OnClickAsync finished", Id);
            }
            else
            {
                //Control?.Logger.LogInformation("Command {Id} OnClickAsync cancelled", Id);
            }
        }
        catch (Exception exception)
        {
            // Control?.Logger.LogError(exception, "Command {Id} OnClickAsync exception", Id);
        }
        finally
        {
            Stopwatch.Stop();
            Stopwatch.Reset();
            periodicTask.IsActive = false;
            if (_cancellationTokenSource != null)
            {
                await _cancellationTokenSource.CancelAsync();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
            _cancellationToken = null;
        }
        _task = null;
        IsInProgress = false;
        Control?.Refresh();
    }

    public virtual Task OnExecuteAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    #endregion
}
