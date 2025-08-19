namespace Blazor.Extensions.Components.Core.Helpers;

public class PeriodicTask
{
    private readonly TimeSpan _timeSpan;
    public CancellationToken CancellationToken { get; set; }
    public bool IsActive { get; set; }

    public PeriodicTask(Action action, TimeSpan timeSpan)
    {
        ArgumentNullException.ThrowIfNull(action);

        _timeSpan = timeSpan;
        Task.Run(async () =>
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_timeSpan);
                if (IsActive)
                {
                    action();
                }
            }
        });
    }
}
