using RezeptePlaner.Maui.Views;

namespace RezeptePlaner.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		// Register routes for navigation
		Routing.RegisterRoute("RecipesPage", typeof(RecipesPage));
		Routing.RegisterRoute("WeekPlannerPage", typeof(WeekPlannerPage));
		Routing.RegisterRoute("FavoritesPage", typeof(FavoritesPage));
		Routing.RegisterRoute("DesignPage", typeof(DesignPage));
		Routing.RegisterRoute("AddRecipePage", typeof(AddRecipePage));
		Routing.RegisterRoute("RecipeDetailPage", typeof(RecipeDetailPage));
		Routing.RegisterRoute("SummaryPage", typeof(SummaryPage));
		Routing.RegisterRoute("InfoPage", typeof(InfoPage));
		Routing.RegisterRoute("HelpPage", typeof(HelpPage));
	}
}
