namespace AlvrModeSwitch;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        AppSettings? savedSettings = AppSettingsStore.Load();
        bool firstRun = savedSettings is null;
        AppSettings settings = savedSettings ?? new AppSettings();

        if (firstRun || !File.Exists(settings.TailscalePath))
        {
            using var welcomeForm = new WelcomeForm(settings, firstRun);
            if (welcomeForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            settings.CopyFrom(welcomeForm.Settings);
            AppSettingsStore.Save(settings);
        }

        Application.Run(new MainForm(settings));
    }
}
