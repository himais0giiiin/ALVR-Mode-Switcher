#nullable disable

namespace AlvrModeSwitch;

partial class WelcomeForm
{
    private System.ComponentModel.IContainer components = null;
    private Label languageLabel;
    private Panel languagePanel;
    private RadioButton japaneseRadioButton;
    private RadioButton englishRadioButton;
    private Label themeLabel;
    private Panel themePanel;
    private RadioButton lightRadioButton;
    private RadioButton darkRadioButton;
    private Label tailscaleLabel;
    private TextBox tailscalePathTextBox;
    private Button browseButton;
    private Button cancelButton;
    private Button continueButton;

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
        languageLabel = new Label();
        languagePanel = new Panel();
        japaneseRadioButton = new RadioButton();
        englishRadioButton = new RadioButton();
        themeLabel = new Label();
        themePanel = new Panel();
        lightRadioButton = new RadioButton();
        darkRadioButton = new RadioButton();
        tailscaleLabel = new Label();
        tailscalePathTextBox = new TextBox();
        browseButton = new Button();
        cancelButton = new Button();
        continueButton = new Button();
        languagePanel.SuspendLayout();
        themePanel.SuspendLayout();
        SuspendLayout();
        // 
        // languageLabel
        // 
        languageLabel.AutoSize = true;
        languageLabel.Font = new Font("Yu Gothic UI Semibold", 9.5F, FontStyle.Bold);
        languageLabel.Location = new Point(20, 16);
        languageLabel.Name = "languageLabel";
        languageLabel.Size = new Size(34, 17);
        languageLabel.TabIndex = 0;
        languageLabel.Text = "言語";
        // 
        // languagePanel
        // 
        languagePanel.Controls.Add(japaneseRadioButton);
        languagePanel.Controls.Add(englishRadioButton);
        languagePanel.Location = new Point(20, 36);
        languagePanel.Name = "languagePanel";
        languagePanel.Size = new Size(180, 28);
        languagePanel.TabIndex = 1;
        // 
        // japaneseRadioButton
        // 
        japaneseRadioButton.AutoSize = true;
        japaneseRadioButton.Location = new Point(1, 3);
        japaneseRadioButton.Name = "japaneseRadioButton";
        japaneseRadioButton.Size = new Size(61, 19);
        japaneseRadioButton.TabIndex = 0;
        japaneseRadioButton.Text = "日本語";
        japaneseRadioButton.UseVisualStyleBackColor = true;
        japaneseRadioButton.CheckedChanged += LanguageRadioButton_CheckedChanged;
        // 
        // englishRadioButton
        // 
        englishRadioButton.AutoSize = true;
        englishRadioButton.Location = new Point(92, 3);
        englishRadioButton.Name = "englishRadioButton";
        englishRadioButton.Size = new Size(63, 19);
        englishRadioButton.TabIndex = 1;
        englishRadioButton.Text = "English";
        englishRadioButton.UseVisualStyleBackColor = true;
        englishRadioButton.CheckedChanged += LanguageRadioButton_CheckedChanged;
        // 
        // themeLabel
        // 
        themeLabel.AutoSize = true;
        themeLabel.Font = new Font("Yu Gothic UI Semibold", 9.5F, FontStyle.Bold);
        themeLabel.Location = new Point(220, 16);
        themeLabel.Name = "themeLabel";
        themeLabel.Size = new Size(42, 17);
        themeLabel.TabIndex = 2;
        themeLabel.Text = "テーマ";
        // 
        // themePanel
        // 
        themePanel.Controls.Add(lightRadioButton);
        themePanel.Controls.Add(darkRadioButton);
        themePanel.Location = new Point(220, 36);
        themePanel.Name = "themePanel";
        themePanel.Size = new Size(180, 28);
        themePanel.TabIndex = 3;
        // 
        // lightRadioButton
        // 
        lightRadioButton.AutoSize = true;
        lightRadioButton.Location = new Point(1, 3);
        lightRadioButton.Name = "lightRadioButton";
        lightRadioButton.Size = new Size(52, 19);
        lightRadioButton.TabIndex = 0;
        lightRadioButton.Text = "ライト";
        lightRadioButton.UseVisualStyleBackColor = true;
        lightRadioButton.CheckedChanged += ThemeRadioButton_CheckedChanged;
        // 
        // darkRadioButton
        // 
        darkRadioButton.AutoSize = true;
        darkRadioButton.Location = new Point(82, 3);
        darkRadioButton.Name = "darkRadioButton";
        darkRadioButton.Size = new Size(53, 19);
        darkRadioButton.TabIndex = 1;
        darkRadioButton.Text = "ダーク";
        darkRadioButton.UseVisualStyleBackColor = true;
        darkRadioButton.CheckedChanged += ThemeRadioButton_CheckedChanged;
        // 
        // tailscaleLabel
        // 
        tailscaleLabel.AutoSize = true;
        tailscaleLabel.Font = new Font("Yu Gothic UI Semibold", 9.5F, FontStyle.Bold);
        tailscaleLabel.Location = new Point(20, 78);
        tailscaleLabel.Name = "tailscaleLabel";
        tailscaleLabel.Size = new Size(62, 17);
        tailscaleLabel.TabIndex = 4;
        tailscaleLabel.Text = "Tailscale";
        // 
        // tailscalePathTextBox
        // 
        tailscalePathTextBox.BorderStyle = BorderStyle.FixedSingle;
        tailscalePathTextBox.Location = new Point(21, 101);
        tailscalePathTextBox.Name = "tailscalePathTextBox";
        tailscalePathTextBox.ReadOnly = true;
        tailscalePathTextBox.Size = new Size(292, 23);
        tailscalePathTextBox.TabIndex = 5;
        // 
        // browseButton
        // 
        browseButton.Cursor = Cursors.Hand;
        browseButton.FlatStyle = FlatStyle.Flat;
        browseButton.Location = new Point(321, 99);
        browseButton.Name = "browseButton";
        browseButton.Size = new Size(79, 27);
        browseButton.TabIndex = 6;
        browseButton.Text = "参照";
        browseButton.UseVisualStyleBackColor = false;
        browseButton.Click += BrowseButton_Click;
        // 
        // cancelButton
        // 
        cancelButton.Cursor = Cursors.Hand;
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.FlatStyle = FlatStyle.Flat;
        cancelButton.Location = new Point(226, 151);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(84, 32);
        cancelButton.TabIndex = 7;
        cancelButton.Text = "キャンセル";
        cancelButton.UseVisualStyleBackColor = false;
        // 
        // continueButton
        // 
        continueButton.Cursor = Cursors.Hand;
        continueButton.FlatStyle = FlatStyle.Flat;
        continueButton.Font = new Font("Yu Gothic UI Semibold", 9F, FontStyle.Bold);
        continueButton.Location = new Point(316, 151);
        continueButton.Name = "continueButton";
        continueButton.Size = new Size(84, 32);
        continueButton.TabIndex = 8;
        continueButton.Text = "開始";
        continueButton.UseVisualStyleBackColor = false;
        continueButton.Click += ContinueButton_Click;
        // 
        // WelcomeForm
        // 
        AcceptButton = continueButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = cancelButton;
        ClientSize = new Size(420, 202);
        Controls.Add(continueButton);
        Controls.Add(cancelButton);
        Controls.Add(browseButton);
        Controls.Add(tailscalePathTextBox);
        Controls.Add(tailscaleLabel);
        Controls.Add(themePanel);
        Controls.Add(themeLabel);
        Controls.Add(languagePanel);
        Controls.Add(languageLabel);
        Font = new Font("Yu Gothic UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "WelcomeForm";
        ShowIcon = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "初期設定";
        languagePanel.ResumeLayout(false);
        languagePanel.PerformLayout();
        themePanel.ResumeLayout(false);
        themePanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
