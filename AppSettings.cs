using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AlvrModeSwitch;

internal sealed class AppSettings
{
    public string Language { get; set; } =
        CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ja" ? "ja" : "en";

    public string Theme { get; set; } = "light";

    public string TailscalePath { get; set; } = string.Empty;

    [JsonIgnore]
    public bool IsEnglish
    {
        get => Language == "en";
        set => Language = value ? "en" : "ja";
    }

    [JsonIgnore]
    public bool IsDarkTheme
    {
        get => Theme == "dark";
        set => Theme = value ? "dark" : "light";
    }

    public AppSettings Clone() => new()
    {
        Language = Language,
        Theme = Theme,
        TailscalePath = TailscalePath,
    };

    public void CopyFrom(AppSettings source)
    {
        Language = source.Language;
        Theme = source.Theme;
        TailscalePath = source.TailscalePath;
    }
}

internal static class AppSettingsStore
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static string SettingsPath { get; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "ALVR Mode Switch",
        "settings.json");

    public static AppSettings? Load()
    {
        if (!File.Exists(SettingsPath))
        {
            return null;
        }

        try
        {
            AppSettings? settings = JsonSerializer.Deserialize<AppSettings>(
                File.ReadAllText(SettingsPath),
                JsonOptions);

            if (settings is null)
            {
                return null;
            }

            settings.Language = settings.Language == "en" ? "en" : "ja";
            settings.Theme = settings.Theme == "dark" ? "dark" : "light";
            return settings;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    public static void Save(AppSettings settings)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath)!);
        File.WriteAllText(SettingsPath, JsonSerializer.Serialize(settings, JsonOptions));
    }
}
