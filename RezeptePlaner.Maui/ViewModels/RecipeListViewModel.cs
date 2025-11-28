using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using RezeptePlaner.Maui.Models;
using RezeptePlaner.Maui.Services;

namespace RezeptePlaner.Maui.ViewModels;

/// <summary>
/// ViewModel for the recipe list/overview page
/// </summary>
public partial class RecipeListViewModel : ObservableObject
{
    private readonly RecipeService _recipeService;

    [ObservableProperty]
    private ObservableCollection<Recipe> recipes = new();

    [ObservableProperty]
    private ObservableCollection<Recipe> filteredRecipes = new();

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private RecipeCategory? selectedCategory;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private Recipe? selectedRecipe;

    public RecipeListViewModel(RecipeService recipeService)
    {
        _recipeService = recipeService;
        LoadRecipes();
    }

    /// <summary>
    /// Load all recipes
    /// </summary>
    [RelayCommand]
    private void LoadRecipes()
    {
        IsLoading = true;
        Recipes = _recipeService.GetRecipes();
        FilterRecipes();
        IsLoading = false;
    }

    /// <summary>
    /// Filter recipes based on search and category
    /// </summary>
    [RelayCommand]
    private void FilterRecipes()
    {
        var filtered = Recipes.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var searchLower = SearchText.ToLowerInvariant();
            filtered = filtered.Where(r => 
                r.Title.ToLowerInvariant().Contains(searchLower) ||
                r.Tags.Any(t => t.ToLowerInvariant().Contains(searchLower)) ||
                r.Ingredients.Any(i => i.ToLowerInvariant().Contains(searchLower)));
        }

        if (SelectedCategory.HasValue)
        {
            filtered = filtered.Where(r => r.Category == SelectedCategory.Value);
        }

        FilteredRecipes = new ObservableCollection<Recipe>(filtered);
    }

    /// <summary>
    /// Toggle favorite status for a recipe
    /// </summary>
    [RelayCommand]
    private void ToggleFavorite(Recipe recipe)
    {
        _recipeService.ToggleFavorite(recipe.Id);
    }

    /// <summary>
    /// Clear all filters
    /// </summary>
    [RelayCommand]
    private void ClearFilters()
    {
        SearchText = string.Empty;
        SelectedCategory = null;
        FilterRecipes();
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterRecipes();
    }

    partial void OnSelectedCategoryChanged(RecipeCategory? value)
    {
        FilterRecipes();
    }
}
