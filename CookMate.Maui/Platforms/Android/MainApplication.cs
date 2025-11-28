using Android.App;
using Android.Runtime;

namespace CookMate.Maui;

/// <summary>
/// Android Application class.
/// Required for proper MAUI initialization on Android.
/// </summary>
[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
