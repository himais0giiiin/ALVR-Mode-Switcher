namespace AlvrModeSwitch;

internal sealed record ThemePalette(
    Color Background,
    Color Surface,
    Color SurfaceHover,
    Color Border,
    Color Text,
    Color MutedText,
    Color Primary,
    Color PrimaryHover,
    Color Error);

internal static class AppTheme
{
    public static ThemePalette Get(bool dark) => dark
        ? new ThemePalette(
            Color.FromArgb(17, 19, 24),
            Color.FromArgb(27, 31, 39),
            Color.FromArgb(35, 40, 50),
            Color.FromArgb(48, 54, 64),
            Color.FromArgb(243, 244, 246),
            Color.FromArgb(161, 169, 184),
            Color.FromArgb(91, 141, 239),
            Color.FromArgb(112, 157, 242),
            Color.FromArgb(248, 113, 113))
        : new ThemePalette(
            Color.FromArgb(245, 246, 248),
            Color.White,
            Color.FromArgb(249, 250, 251),
            Color.FromArgb(220, 225, 232),
            Color.FromArgb(23, 26, 33),
            Color.FromArgb(101, 112, 131),
            Color.FromArgb(37, 99, 235),
            Color.FromArgb(59, 130, 246),
            Color.FromArgb(185, 28, 28));
}
