using Foundation;

namespace CookMate.Maui;

/// <summary>
/// MacCatalyst AppDelegate - the main entry point for the macOS app.
/// Configured for MAUI on macOS via Catalyst.
/// </summary>
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
