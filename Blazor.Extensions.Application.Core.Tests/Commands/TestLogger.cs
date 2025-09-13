namespace Blazor.Extensions.Application.Core.Tests.Commands;

public partial class CommandBaseTests
{
    private sealed class TestLogger : Microsoft.Extensions.Logging.ILogger
    {
        private sealed class NoopScope : IDisposable { public void Dispose() { } }
        public IDisposable BeginScope<TState>(TState state) => new NoopScope();
        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => false;
        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) { }
    }
}
