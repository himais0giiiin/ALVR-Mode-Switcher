namespace AlvrModeSwitch;

public partial class WelcomeForm : Form
{
    private readonly AppSettings _workingSettings;
    private readonly bool _firstRun;
    private bool _initializing = true;
    private bool _pathWasAutoDetected;

    internal AppSettings Settings => _workingSettings.Clone();

    public WelcomeForm()
        : this(new AppSettings(), firstRun: true)
    {
    }

    internal WelcomeForm(AppSettings settings, bool firstRun)
    {
        _workingSettings = settings.Clone();
        _firstRun = firstRun;
        InitializeComponent();

        if (!File.Exists(_workingSettings.TailscalePath))
        {
            string? detectedPath = TailscaleModeService.FindExecutable();
            if (detectedPath is not null)
            {
                _workingSettings.TailscalePath = detectedPath;
                _pathWasAutoDetected = true;
            }
        }

        japaneseRadioButton.Checked = !_workingSettings.IsEnglish;
        englishRadioButton.Checked = _workingSettings.IsEnglish;
        lightRadioButton.Checked = !_workingSettings.IsDarkTheme;
        darkRadioButton.Checked = _workingSettings.IsDarkTheme;
        tailscalePathTextBox.Text = _workingSettings.TailscalePath;
        _initializing = false;

        ApplyLanguage();
        ApplyTheme();
    }

    private void LanguageRadioButton_CheckedChanged(object? sender, EventArgs e)
    {
        if (_initializing)
        {
            return;
        }

        _workingSettings.IsEnglish = englishRadioButton.Checked;
        ApplyLanguage();
    }

    private void ThemeRadioButton_CheckedChanged(object? sender, EventArgs e)
    {
        if (_initializing)
        {
            return;
        }

        _workingSettings.IsDarkTheme = darkRadioButton.Checked;
        ApplyLanguage();
        ApplyTheme();
    }

    private void BrowseButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = Localize("Tailscaleを選択", "Select Tailscale"),
            Filter = "tailscale.exe|tailscale.exe|Executable files (*.exe)|*.exe",
            CheckFileExists = true,
            FileName = "tailscale.exe",
        };

        if (File.Exists(_workingSettings.TailscalePath))
        {
            dialog.InitialDirectory = Path.GetDirectoryName(_workingSettings.TailscalePath);
        }

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        if (!string.Equals(Path.GetFileName(dialog.FileName), "tailscale.exe", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show(
                this,
                Localize("tailscale.exeを選択してください。", "Select tailscale.exe."),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        _workingSettings.TailscalePath = dialog.FileName;
        _pathWasAutoDetected = false;
        tailscalePathTextBox.Text = dialog.FileName;
        ApplyPathStatus();
    }

    private void ContinueButton_Click(object? sender, EventArgs e)
    {
        if (!File.Exists(_workingSettings.TailscalePath))
        {
            MessageBox.Show(
                this,
                Localize(
                    "Tailscaleを自動検出できませんでした。tailscale.exeを選択してください。",
                    "Tailscale was not detected. Select tailscale.exe."),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void ApplyLanguage()
    {
        Text = Localize(_firstRun ? "初期設定" : "設定", _firstRun ? "Welcome" : "Settings");
        languageLabel.Text = Localize("言語", "Language");
        themeLabel.Text = Localize("テーマ", "Theme");
        lightRadioButton.Text = Localize("ライト", "Light");
        darkRadioButton.Text = Localize("ダーク", "Dark");
        browseButton.Text = Localize("参照", "Browse");
        cancelButton.Text = Localize("キャンセル", "Cancel");
        continueButton.Text = Localize(_firstRun ? "開始" : "保存", _firstRun ? "Continue" : "Save");
        ApplyPathStatus();
    }

    private void ApplyPathStatus()
    {
        bool pathExists = File.Exists(_workingSettings.TailscalePath);
        ThemePalette palette = AppTheme.Get(_workingSettings.IsDarkTheme);
        tailscaleLabel.ForeColor = pathExists ? palette.Text : palette.Error;
        tailscaleLabel.Text = pathExists
            ? _pathWasAutoDetected
                ? Localize("Tailscale（自動検出）", "Tailscale (detected)")
                : "Tailscale"
            : Localize("Tailscale（未選択）", "Tailscale (not selected)");
    }

    private void ApplyTheme()
    {
        ThemePalette palette = AppTheme.Get(_workingSettings.IsDarkTheme);
        BackColor = palette.Background;
        languageLabel.ForeColor = palette.Text;
        themeLabel.ForeColor = palette.Text;
        languagePanel.BackColor = palette.Background;
        themePanel.BackColor = palette.Background;
        japaneseRadioButton.ForeColor = palette.Text;
        englishRadioButton.ForeColor = palette.Text;
        lightRadioButton.ForeColor = palette.Text;
        darkRadioButton.ForeColor = palette.Text;
        tailscalePathTextBox.BackColor = palette.Surface;
        tailscalePathTextBox.ForeColor = palette.Text;

        StyleSecondaryButton(browseButton, palette);
        StyleSecondaryButton(cancelButton, palette);
        continueButton.BackColor = palette.Primary;
        continueButton.ForeColor = Color.White;
        continueButton.FlatAppearance.BorderColor = palette.Primary;
        continueButton.FlatAppearance.MouseOverBackColor = palette.PrimaryHover;
        ApplyPathStatus();
    }

    private static void StyleSecondaryButton(Button button, ThemePalette palette)
    {
        button.BackColor = palette.Surface;
        button.ForeColor = palette.Text;
        button.FlatAppearance.BorderColor = palette.Border;
        button.FlatAppearance.MouseOverBackColor = palette.SurfaceHover;
    }

    private string Localize(string japanese, string english) =>
        _workingSettings.IsEnglish ? english : japanese;
}
