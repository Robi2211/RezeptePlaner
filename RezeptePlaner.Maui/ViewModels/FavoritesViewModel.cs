using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using RezeptePlaner.Maui.Models;
using RezeptePlaner.Maui.Services;

namespace RezeptePlaner.Maui.ViewModels;

/// <summary>
/// ViewModel for the Favorites page
/// </summary>
public partial class FavoritesViewModel : ObservableObject
{
    private readonly RecipeService _recipeService;

    [ObservableProperty]
    private ObservableCollection<Recipe> favoriteRecipes = new();

    [ObservableProperty]
    private ObservableCollection<Recipe> filteredRecipes = new();

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private int favoriteCount;

    [ObservableProperty]
    private Recipe? selectedRecipe;

    public FavoritesViewModel(RecipeService recipeService)
    {
        _recipeService = recipeService;
        
        // Subscribe to favorites changes
        _recipeService.FavoritesChanged += OnFavoritesChanged;
        
        LoadFavorites();
    }

    private void OnFavoritesChanged(object? sender, EventArgs e)
    {
        LoadFavorites();
    }

    public void Cleanup()
    {
        // Unsubscribe from events to prevent memory leaks
        _recipeService.FavoritesChanged -= OnFavoritesChanged;
    }

    [RelayCommand]
    public void LoadFavorites()
    {
        var allRecipes = _recipeService.GetRecipes();
        FavoriteRecipes = new ObservableCollection<Recipe>(allRecipes.Where(r => r.IsFavorite));
        FavoriteCount = FavoriteRecipes.Count;
        FilterRecipes();
    }

    [RelayCommand]
    private void FilterRecipes()
    {
        var filtered = FavoriteRecipes.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var searchLower = SearchText.ToLowerInvariant();
            filtered = filtered.Where(r =>
                r.Title.ToLowerInvariant().Contains(searchLower) ||
                r.Tags.Any(t => t.ToLowerInvariant().Contains(searchLower)));
        }

        FilteredRecipes = new ObservableCollection<Recipe>(filtered);
    }

    [RelayCommand]
    private void ToggleFavorite(Recipe recipe)
    {
        if (recipe != null)
        {
            _recipeService.ToggleFavorite(recipe.Id);
            // Note: LoadFavorites will be called automatically via OnFavoritesChanged event handler
        }
    }

    [RelayCommand]
    private async Task ViewRecipe(Recipe recipe)
    {
        if (recipe != null)
        {
            await Shell.Current.GoToAsync($"RecipeDetailPage?recipeId={recipe.Id}");
        }
    }

    [RelayCommand]
    private async Task NavigateToRecipes()
    {
        await Shell.Current.GoToAsync("//RecipesPage");
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterRecipes();
    }
}
