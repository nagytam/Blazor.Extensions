using System.Diagnostics;
namespace Blazor.Extensions.App.Server.Tests.Helpers;

public class WebAppRunner
{
    Process? _process;

    public void Run()
    { 
        var assemblyPath = System.Reflection.Assembly.GetAssembly(typeof(WebAppRunner)).Location;
        var projectPath = Path.GetFullPath(Path.Combine(assemblyPath, "..", "..", "..", "..", "..", "Blazor.Extensions.App.Server"));
        //var exePath = Path.GetFullPath(Path.Combine(projectPath, "bin", "Debug", "net9.0", "Blazor.Extensions.App.Server.exe"));
        _process = Process.Start(new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = "/K dotnet run Blazor.Extensions.App.Server.csproj",
            WorkingDirectory = projectPath,
            RedirectStandardOutput = true,
            RedirectStandardError = false,
            UseShellExecute = false,
            CreateNoWindow = true
        });

        var isStarted = false;
        while (!isStarted)
        {
            var line = _process?.StandardOutput.ReadLine();
            if (line != null && line.Contains("Now listening on:"))
            {
                isStarted = true;
            }
            Console.WriteLine(line);
        }
    }

    public void Stop()
    {
        if (_process is null)
            return;

        try
        {
            if (!_process.HasExited)
            {
                // Kill entire process tree (.NET 5+ cross-platform)
                _process.Kill(entireProcessTree: true);
            }
        }
        catch
        {
            // Swallow exceptions to avoid masking test cleanup failures.
        }
        finally
        {
            try { _process.Dispose(); } catch { }
            _process = null;
        }
    }
}
