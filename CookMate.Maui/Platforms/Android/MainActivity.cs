using Android.App;
using Android.Content.PM;
using Android.OS;

namespace CookMate.Maui;

/// <summary>
/// Android MainActivity - the main entry point for the Android app.
/// Configured with proper launch settings for MAUI.
/// </summary>
[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize 
        | ConfigChanges.Orientation 
        | ConfigChanges.UiMode 
        | ConfigChanges.ScreenLayout 
        | ConfigChanges.SmallestScreenSize 
        | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
}
