using CookMate.Maui.ViewModels;
using CookMate.Maui.Views;

namespace CookMate.Maui.Services;

/// <summary>
/// Navigation service for type-safe page navigation with parameter support.
/// Uses Shell navigation for consistent cross-platform behavior.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Navigates to a page using its route name.
    /// </summary>
    Task NavigateToAsync(string route);
    
    /// <summary>
    /// Navigates to a page with parameters.
    /// </summary>
    Task NavigateToAsync(string route, IDictionary<string, object> parameters);
    
    /// <summary>
    /// Navigates back to the previous page.
    /// </summary>
    Task GoBackAsync();
}

/// <summary>
/// Shell-based navigation service implementation.
/// Provides smooth navigation with animation support.
/// </summary>
public class NavigationService : INavigationService
{
    public async Task NavigateToAsync(string route)
    {
        await Shell.Current.GoToAsync(route);
    }
    
    public async Task NavigateToAsync(string route, IDictionary<string, object> parameters)
    {
        await Shell.Current.GoToAsync(route, parameters);
    }
    
    public async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}

/// <summary>
/// Route constants for type-safe navigation.
/// Keeps all route strings in one place for maintainability.
/// </summary>
public static class Routes
{
    public const string Dashboard = "//dashboard";
    public const string Recipes = "//recipes";
    public const string RecipeDetail = "recipedetail";
    public const string Planner = "//planner";
    public const string Favorites = "//favorites";
    public const string AddRecipe = "addrecipe";
    public const string DesignSystem = "//design";
}
