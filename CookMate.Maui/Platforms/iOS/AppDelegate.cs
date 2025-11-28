using Foundation;

namespace CookMate.Maui;

/// <summary>
/// iOS AppDelegate - the main entry point for the iOS app.
/// Configured for MAUI on iOS/iPadOS.
/// </summary>
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
