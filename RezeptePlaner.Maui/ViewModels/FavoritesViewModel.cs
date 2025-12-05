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
        LoadFavorites();
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
            LoadFavorites();
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
