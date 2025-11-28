namespace CookMate.Maui;

/// <summary>
/// Main Application class.
/// Sets up the AppShell as the main page.
/// </summary>
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Creates the main window for the application.
    /// Uses AppShell for Shell-based navigation.
    /// </summary>
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell())
        {
            Title = "CookMate - Rezepteplaner"
        };
    }
}
