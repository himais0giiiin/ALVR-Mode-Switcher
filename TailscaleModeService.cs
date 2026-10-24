using System.Diagnostics;
using System.Text.Json;

namespace AlvrModeSwitch;

internal sealed record TailscalePreferences(bool AcceptRoutes, bool IsRunning);

internal static class TailscaleModeService
{
    public static string? FindExecutable()
    {
        string[] candidates =
        [
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "Tailscale",
                "tailscale.exe"),
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Tailscale",
                "tailscale.exe"),
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Tailscale",
                "tailscale.exe"),
        ];

        string? installedPath = candidates.FirstOrDefault(File.Exists);
        if (installedPath is not null)
        {
            return installedPath;
        }

        string[] pathDirectories = (Environment.GetEnvironmentVariable("PATH") ?? string.Empty)
            .Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return pathDirectories
            .Select(directory => Path.Combine(directory, "tailscale.exe"))
            .FirstOrDefault(File.Exists);
    }

    public static async Task<TailscalePreferences> GetPreferencesAsync(string executablePath)
    {
        string json = await RunAsync(executablePath, "debug", "prefs");
        using JsonDocument document = JsonDocument.Parse(json);
        JsonElement root = document.RootElement;

        bool acceptRoutes = root.TryGetProperty("RouteAll", out JsonElement routeAll)
            && routeAll.GetBoolean();
        bool isRunning = root.TryGetProperty("WantRunning", out JsonElement wantRunning)
            && wantRunning.GetBoolean();

        return new TailscalePreferences(acceptRoutes, isRunning);
    }

    public static Task SetAcceptRoutesAsync(string executablePath, bool enabled) =>
        RunAsync(executablePath, "set", $"--accept-routes={enabled.ToString().ToLowerInvariant()}");

    private static async Task<string> RunAsync(string executablePath, params string[] arguments)
    {
        if (!File.Exists(executablePath))
        {
            throw new FileNotFoundException(
                "Tailscale executable was not found.",
                executablePath);
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = executablePath,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };

        foreach (string argument in arguments)
        {
            startInfo.ArgumentList.Add(argument);
        }

        using var process = new Process { StartInfo = startInfo };
        process.Start();

        Task<string> standardOutput = process.StandardOutput.ReadToEndAsync();
        Task<string> standardError = process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync();

        string output = await standardOutput;
        string error = await standardError;

        if (process.ExitCode != 0)
        {
            string message = string.IsNullOrWhiteSpace(error) ? output : error;
            throw new InvalidOperationException(
                string.IsNullOrWhiteSpace(message)
                    ? $"Tailscaleコマンドが終了コード {process.ExitCode} で失敗しました"
                    : message.Trim());
        }

        return output;
    }
}
