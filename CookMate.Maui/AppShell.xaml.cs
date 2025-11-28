using CookMate.Maui.Services;
using CookMate.Maui.Views;

namespace CookMate.Maui;

/// <summary>
/// AppShell provides the main navigation structure for the application.
/// Configures Shell-based navigation routes for the recipe app.
/// </summary>
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        // Register routes for pages that aren't in the TabBar
        // These use modal or push navigation
        Routing.RegisterRoute(Routes.RecipeDetail, typeof(RecipeDetailPage));
        Routing.RegisterRoute(Routes.AddRecipe, typeof(AddRecipePage));
    }
}
