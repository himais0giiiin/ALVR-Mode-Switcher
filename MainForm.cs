namespace AlvrModeSwitch;

public partial class MainForm : Form
{
    private readonly AppSettings _settings;
    private TailscalePreferences? _preferences;

    public MainForm()
        : this(new AppSettings
        {
            TailscalePath = TailscaleModeService.FindExecutable() ?? string.Empty,
        })
    {
    }

    internal MainForm(AppSettings settings)
    {
        _settings = settings;
        InitializeComponent();
        ApplyLanguage();
        ApplyTheme();
    }

    protected override async void OnShown(EventArgs e)
    {
        base.OnShown(e);
        await RefreshStatusAsync();
    }

    private async void AlvrButton_Click(object? sender, EventArgs e)
    {
        await SetModeAsync(acceptRoutes: false);
    }

    private async void TailscaleButton_Click(object? sender, EventArgs e)
    {
        await SetModeAsync(acceptRoutes: true);
    }

    private async void RefreshButton_Click(object? sender, EventArgs e)
    {
        await RefreshStatusAsync();
    }

    private void LanguageButton_Click(object? sender, EventArgs e)
    {
        _settings.IsEnglish = !_settings.IsEnglish;
        AppSettingsStore.Save(_settings);
        ApplyLanguage();
    }

    private void ThemeButton_Click(object? sender, EventArgs e)
    {
        _settings.IsDarkTheme = !_settings.IsDarkTheme;
        AppSettingsStore.Save(_settings);
        ApplyLanguage();
        ApplyTheme();
    }

    private async void SettingsButton_Click(object? sender, EventArgs e)
    {
        using var settingsForm = new WelcomeForm(_settings, firstRun: false);
        if (settingsForm.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _settings.CopyFrom(settingsForm.Settings);
        AppSettingsStore.Save(_settings);
        ApplyLanguage();
        ApplyTheme();
        await RefreshStatusAsync();
    }

    private async Task SetModeAsync(bool acceptRoutes)
    {
        SetBusy(
            true,
            Localize("切り替え中...", "Switching..."));

        try
        {
            await TailscaleModeService.SetAcceptRoutesAsync(_settings.TailscalePath, acceptRoutes);
            TailscalePreferences preferences = await TailscaleModeService.GetPreferencesAsync(
                _settings.TailscalePath);

            if (preferences.AcceptRoutes != acceptRoutes)
            {
                throw new InvalidOperationException(Localize(
                    "設定の反映を確認できませんでした。",
                    "Could not verify that the setting was applied."));
            }

            _preferences = preferences;
            ApplyStatus(preferences);
        }
        catch (Exception exception)
        {
            ShowError(exception);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private async Task RefreshStatusAsync()
    {
        SetBusy(true, Localize("確認中...", "Checking..."));

        try
        {
            _preferences = await TailscaleModeService.GetPreferencesAsync(_settings.TailscalePath);
            ApplyStatus(_preferences);
        }
        catch (Exception exception)
        {
            _preferences = null;
            ShowError(exception);
        }
        finally
        {
            SetBusy(false);
        }
    }

    private void ApplyStatus(TailscalePreferences preferences)
    {
        ThemePalette palette = AppTheme.Get(_settings.IsDarkTheme);

        if (preferences.AcceptRoutes)
        {
            statusTitleLabel.Text = Localize("Tailscaleの接続を優先中です！", "Using subnet routes!");
        }
        else
        {
            statusTitleLabel.Text = Localize("ALVRの接続を優先中です！", "Direct connection to HMD!");
        }

        StyleModeButtons(preferences.AcceptRoutes, palette);
    }

    private void SetBusy(bool busy, string? message = null)
    {
        UseWaitCursor = busy;
        alvrButton.Enabled = !busy;
        tailscaleButton.Enabled = !busy;
        refreshButton.Enabled = !busy;
        languageButton.Enabled = !busy;
        themeButton.Enabled = !busy;
        settingsButton.Enabled = !busy;

        if (message is not null)
        {
            statusTitleLabel.Text = message;
        }
    }

    private void ShowError(Exception exception)
    {
        statusTitleLabel.Text = Localize("状態を取得できません", "Could not get status");
        StyleModeButtons(null, AppTheme.Get(_settings.IsDarkTheme));
    }

    private void ApplyLanguage()
    {
        languageButton.Text = _settings.IsEnglish ? "JA" : "EN";
        languageButton.AccessibleName = Localize("英語に切り替え", "Switch to Japanese");
        themeButton.Text = _settings.IsDarkTheme
            ? Localize("ライト", "Light")
            : Localize("ダーク", "Dark");
        settingsButton.Text = Localize("設定", "Settings");
        refreshButton.Text = Localize("再読込", "Refresh");
        alvrButton.Text = Localize("ALVRを使う", "Use ALVR");
        tailscaleButton.Text = Localize("Tailscale経路を使う", "Use Tailscale route");

        if (_preferences is not null)
        {
            ApplyStatus(_preferences);
        }
        else
        {
            statusTitleLabel.Text = Localize("確認中...", "Checking...");
        }
    }

    private void ApplyTheme()
    {
        ThemePalette palette = AppTheme.Get(_settings.IsDarkTheme);
        BackColor = palette.Background;
        statusPanel.BackColor = palette.Surface;
        statusTitleLabel.ForeColor = palette.Text;

        StyleToolbarButton(languageButton, palette);
        StyleToolbarButton(themeButton, palette);
        StyleToolbarButton(settingsButton, palette);
        StyleToolbarButton(refreshButton, palette);

        StyleModeButtons(_preferences?.AcceptRoutes, palette);
    }

    private static void StyleToolbarButton(Button button, ThemePalette palette)
    {
        button.BackColor = palette.Surface;
        button.ForeColor = palette.Text;
        button.FlatAppearance.BorderColor = palette.Border;
        button.FlatAppearance.MouseOverBackColor = palette.SurfaceHover;
    }

    private void StyleModeButtons(bool? acceptRoutes, ThemePalette palette)
    {
        foreach (Button button in new[] { alvrButton, tailscaleButton })
        {
            button.BackColor = palette.Surface;
            button.ForeColor = palette.Text;
            button.FlatAppearance.BorderColor = palette.Border;
            button.FlatAppearance.MouseOverBackColor = palette.SurfaceHover;
        }

        if (acceptRoutes is null)
        {
            return;
        }

        Button activeButton = acceptRoutes.Value ? tailscaleButton : alvrButton;
        activeButton.ForeColor = palette.Primary;
        activeButton.FlatAppearance.BorderColor = palette.Primary;
    }

    private string Localize(string japanese, string english) =>
        _settings.IsEnglish ? english : japanese;
}
