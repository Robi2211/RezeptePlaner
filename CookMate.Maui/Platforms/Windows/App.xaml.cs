using Microsoft.UI.Xaml;

namespace CookMate.Maui.WinUI;

/// <summary>
/// Windows App entry point.
/// Provides proper initialization for WinUI/MAUI on Windows.
/// </summary>
public partial class App : MauiWinUIApplication
{
    /// <summary>
    /// Initializes the Windows application.
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
