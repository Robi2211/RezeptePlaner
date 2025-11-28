using RezeptePlaner.Maui.Views;

namespace RezeptePlaner.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		// Register routes for navigation
		Routing.RegisterRoute("SummaryPage", typeof(SummaryPage));
	}
}
