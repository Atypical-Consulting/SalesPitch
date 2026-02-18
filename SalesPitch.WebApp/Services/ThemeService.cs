using MudBlazor;

namespace SalesPitch.WebApp.Services;

public class ThemeService
{
    private bool _isDarkMode;

    public MudTheme Theme { get; private set; }

    public bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            _isDarkMode = value;
            OnThemeChanged?.Invoke();
        }
    }

    public event Action? OnThemeChanged;

    public ThemeService()
    {
        Theme = CreateCustomTheme();
    }

    private static MudTheme CreateCustomTheme()
    {
        return new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary = "#6366f1", // Indigo-500
                Secondary = "#8b5cf6", // Violet-500
                Success = "#10b981", // Emerald-500
                Warning = "#f59e0b", // Amber-500
                Error = "#ef4444", // Red-500
                Info = "#3b82f6", // Blue-500
                Surface = "#ffffff",
                Background = "#f8fafc", // Slate-50
                AppbarBackground = "#ffffff",
                DrawerBackground = "#ffffff",
                DrawerText = "#1e293b", // Slate-800
                TextPrimary = "#0f172a", // Slate-900
                TextSecondary = "#64748b", // Slate-500
                ActionDefault = "#94a3b8", // Slate-400
                Divider = "#e2e8f0", // Slate-200
                LinesDefault = "#e2e8f0" // Slate-200
            },
            PaletteDark = new PaletteDark
            {
                Primary = "#818cf8", // Indigo-400
                Secondary = "#a78bfa", // Violet-400
                Success = "#34d399", // Emerald-400
                Warning = "#fbbf24", // Amber-400
                Error = "#f87171", // Red-400
                Info = "#60a5fa", // Blue-400
                Surface = "#1e293b", // Slate-800
                Background = "#0f172a", // Slate-900
                AppbarBackground = "#1e293b",
                DrawerBackground = "#1e293b",
                DrawerText = "#f1f5f9", // Slate-100
                TextPrimary = "#f8fafc", // Slate-50
                TextSecondary = "#94a3b8", // Slate-400
                ActionDefault = "#64748b", // Slate-500
                Divider = "#475569", // Slate-600
                LinesDefault = "#475569" // Slate-600
            }
        };
    }

    public void ToggleDarkMode()
    {
        IsDarkMode = !IsDarkMode;
    }
}