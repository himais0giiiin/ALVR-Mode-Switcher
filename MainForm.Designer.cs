#nullable disable

namespace AlvrModeSwitch;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel statusPanel;
    private Label statusTitleLabel;
    private Button alvrButton;
    private Button tailscaleButton;
    private Button refreshButton;
    private Button languageButton;
    private Button themeButton;
    private Button settingsButton;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        statusPanel = new Panel();
        statusTitleLabel = new Label();
        alvrButton = new Button();
        tailscaleButton = new Button();
        refreshButton = new Button();
        languageButton = new Button();
        themeButton = new Button();
        settingsButton = new Button();
        statusPanel.SuspendLayout();
        SuspendLayout();
        // 
        // statusPanel
        // 
        statusPanel.BorderStyle = BorderStyle.FixedSingle;
        statusPanel.Controls.Add(statusTitleLabel);
        statusPanel.Location = new Point(12, 12);
        statusPanel.Name = "statusPanel";
        statusPanel.Size = new Size(266, 46);
        statusPanel.TabIndex = 0;
        // 
        // statusTitleLabel
        // 
        statusTitleLabel.AutoEllipsis = true;
        statusTitleLabel.Font = new Font("Yu Gothic UI Semibold", 10.5F, FontStyle.Bold);
        statusTitleLabel.Location = new Point(3, 12);
        statusTitleLabel.Name = "statusTitleLabel";
        statusTitleLabel.Size = new Size(258, 21);
        statusTitleLabel.TabIndex = 0;
        statusTitleLabel.Text = "確認中...";
        // 
        // alvrButton
        // 
        alvrButton.Cursor = Cursors.Hand;
        alvrButton.FlatStyle = FlatStyle.Flat;
        alvrButton.Font = new Font("Yu Gothic UI Semibold", 9F, FontStyle.Bold);
        alvrButton.Location = new Point(12, 68);
        alvrButton.Name = "alvrButton";
        alvrButton.Size = new Size(130, 34);
        alvrButton.TabIndex = 1;
        alvrButton.Text = "ALVRを使う";
        alvrButton.UseVisualStyleBackColor = false;
        alvrButton.Click += AlvrButton_Click;
        // 
        // tailscaleButton
        // 
        tailscaleButton.Cursor = Cursors.Hand;
        tailscaleButton.FlatStyle = FlatStyle.Flat;
        tailscaleButton.Font = new Font("Yu Gothic UI Semibold", 9F, FontStyle.Bold);
        tailscaleButton.Location = new Point(148, 68);
        tailscaleButton.Name = "tailscaleButton";
        tailscaleButton.Size = new Size(130, 34);
        tailscaleButton.TabIndex = 2;
        tailscaleButton.Text = "Tailscale経路を使う";
        tailscaleButton.UseVisualStyleBackColor = false;
        tailscaleButton.Click += TailscaleButton_Click;
        // 
        // refreshButton
        // 
        refreshButton.Cursor = Cursors.Hand;
        refreshButton.FlatStyle = FlatStyle.Flat;
        refreshButton.Font = new Font("Yu Gothic UI", 8F);
        refreshButton.Location = new Point(12, 108);
        refreshButton.Name = "refreshButton";
        refreshButton.Size = new Size(72, 26);
        refreshButton.TabIndex = 3;
        refreshButton.Text = "更新";
        refreshButton.UseVisualStyleBackColor = false;
        refreshButton.Click += RefreshButton_Click;
        // 
        // languageButton
        // 
        languageButton.Cursor = Cursors.Hand;
        languageButton.FlatStyle = FlatStyle.Flat;
        languageButton.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold);
        languageButton.Location = new Point(126, 108);
        languageButton.Name = "languageButton";
        languageButton.Size = new Size(40, 26);
        languageButton.TabIndex = 4;
        languageButton.Text = "EN";
        languageButton.UseVisualStyleBackColor = false;
        languageButton.Click += LanguageButton_Click;
        // 
        // themeButton
        // 
        themeButton.Cursor = Cursors.Hand;
        themeButton.FlatStyle = FlatStyle.Flat;
        themeButton.Font = new Font("Yu Gothic UI", 8F);
        themeButton.Location = new Point(172, 108);
        themeButton.Name = "themeButton";
        themeButton.Size = new Size(50, 26);
        themeButton.TabIndex = 5;
        themeButton.Text = "ダーク";
        themeButton.UseVisualStyleBackColor = false;
        themeButton.Click += ThemeButton_Click;
        // 
        // settingsButton
        // 
        settingsButton.Cursor = Cursors.Hand;
        settingsButton.FlatStyle = FlatStyle.Flat;
        settingsButton.Font = new Font("Yu Gothic UI", 8F);
        settingsButton.Location = new Point(228, 108);
        settingsButton.Name = "settingsButton";
        settingsButton.Size = new Size(50, 26);
        settingsButton.TabIndex = 6;
        settingsButton.Text = "設定";
        settingsButton.UseVisualStyleBackColor = false;
        settingsButton.Click += SettingsButton_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(290, 145);
        Controls.Add(settingsButton);
        Controls.Add(themeButton);
        Controls.Add(languageButton);
        Controls.Add(refreshButton);
        Controls.Add(tailscaleButton);
        Controls.Add(alvrButton);
        Controls.Add(statusPanel);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "ALVR Mode Switcher";
        statusPanel.ResumeLayout(false);
        ResumeLayout(false);
    }
}
