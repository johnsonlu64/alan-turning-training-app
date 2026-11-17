namespace TuringTrainer.Services;

public enum ThemeChoice
{
    System = 0,
    Light = 1,
    Dark = 2,
}

public static class ThemeService
{
    private const string PreferenceKey = "app_theme";

    private static Application? _app;

    private static readonly Dictionary<string, (Color Light, Color Dark)> Palette = new()
    {
        ["PageBg"]     = (Color.FromArgb("#F4F4F1"), Color.FromArgb("#101010")),
        ["NavBg"]      = (Color.FromArgb("#FFFFFF"), Color.FromArgb("#151515")),
        ["CardBg"]     = (Color.FromArgb("#FFFFFF"), Color.FromArgb("#1C1C1C")),
        ["InsetBg"]    = (Color.FromArgb("#ECECE7"), Color.FromArgb("#262626")),
        ["AccentTint"] = (Color.FromArgb("#FBEFC2"), Color.FromArgb("#332B12")),

        ["CardBorder"]  = (Color.FromArgb("#DBDBD4"), Color.FromArgb("#343434")),
        ["DividerLine"] = (Color.FromArgb("#E3E3DD"), Color.FromArgb("#2A2A2A")),

        ["TextMain"]   = (Color.FromArgb("#141414"), Color.FromArgb("#FFFFFF")),
        ["TextSub"]    = (Color.FromArgb("#575754"), Color.FromArgb("#A5A5A5")),
        ["TextMuted"]  = (Color.FromArgb("#686865"), Color.FromArgb("#999999")),
        ["TextDim"]    = (Color.FromArgb("#7A7A76"), Color.FromArgb("#777777")),

        ["TextAccent"] = (Color.FromArgb("#F5C542"), Color.FromArgb("#F5C542")),
    };
    public static ThemeChoice Current { get; private set; } = ThemeChoice.Dark;

    public static bool IsDark => _app?.RequestedTheme != AppTheme.Light;

    public static void Initialize(Application app)
    {
        _app = app;

        string saved = Preferences.Default.Get(PreferenceKey, nameof(ThemeChoice.Dark));
        Current = Enum.TryParse(saved, out ThemeChoice choice) ? choice : ThemeChoice.Dark;

        _app.RequestedThemeChanged += (s, e) => ApplyColors();

        Apply(Current);
    }

    public static void Apply(ThemeChoice choice)
    {
        if (_app is null)
            return;

        Current = choice;
        Preferences.Default.Set(PreferenceKey, choice.ToString());

        _app.UserAppTheme = choice switch
        {
            ThemeChoice.Light => AppTheme.Light,
            ThemeChoice.Dark => AppTheme.Dark,
            _ => AppTheme.Unspecified,
        };

        ApplyColors();
    }

    private static void ApplyColors()
    {
        if (_app is null)
            return;

        bool dark = IsDark;

        foreach (KeyValuePair<string, (Color Light, Color Dark)> entry in Palette)
        {
            Color color = dark ? entry.Value.Dark : entry.Value.Light;
            _app.Resources[entry.Key] = color;
            _app.Resources[entry.Key + "Brush"] = new SolidColorBrush(color);
        }
    }
}
