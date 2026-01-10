using Microsoft.Extensions.DependencyInjection;

namespace RezeptePlaner.Maui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var window = new Window(new AppShell());

#if WINDOWS
		// Hide the default Windows title bar for a custom navigation experience
		window.Created += (s, e) =>
		{
			if (window.Handler?.PlatformView is Microsoft.UI.Xaml.Window winUIWindow)
			{
				var handle = WinRT.Interop.WindowNative.GetWindowHandle(winUIWindow);
				var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
				var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
				
				if (appWindow is not null)
				{
					var transparentColor = Microsoft.UI.Colors.Transparent;
					// Hide the default title bar
					appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
					appWindow.TitleBar.ButtonBackgroundColor = transparentColor;
					appWindow.TitleBar.ButtonInactiveBackgroundColor = transparentColor;
				}
			}
		};
#endif

		return window;
	}
}