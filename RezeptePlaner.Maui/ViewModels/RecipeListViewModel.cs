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
    private DifficultyLevel? selectedDifficulty;

    [ObservableProperty]
    private string? selectedCategoryName;

    [ObservableProperty]
    private string? selectedDifficultyName;

    [ObservableProperty]
    private int maxTimeMinutes = 120;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private Recipe? selectedRecipe;

    [ObservableProperty]
    private int resultCount;

    /// <summary>
    /// Available categories for filtering
    /// </summary>
    public ObservableCollection<RecipeCategory> Categories { get; } = 
        new(Enum.GetValues<RecipeCategory>());

    /// <summary>
    /// Available difficulty levels for filtering
    /// </summary>
    public ObservableCollection<DifficultyLevel> Difficulties { get; } = 
        new(Enum.GetValues<DifficultyLevel>());

    /// <summary>
    /// Category names as strings for the filter bar
    /// </summary>
    public ObservableCollection<string> CategoryNames { get; } = 
        new(Enum.GetValues<RecipeCategory>().Select(c => c.ToString()));

    /// <summary>
    /// Difficulty names as strings for the filter bar
    /// </summary>
    public ObservableCollection<string> DifficultyNames { get; } = 
        new(Enum.GetValues<DifficultyLevel>().Select(d => d.ToString()));

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

        if (SelectedDifficulty.HasValue)
        {
            filtered = filtered.Where(r => r.Difficulty == SelectedDifficulty.Value);
        }

        if (MaxTimeMinutes > 0)
        {
            filtered = filtered.Where(r => r.TotalTime <= MaxTimeMinutes);
        }

        FilteredRecipes = new ObservableCollection<Recipe>(filtered);
        ResultCount = FilteredRecipes.Count;
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
        SelectedCategoryName = null;
        SelectedDifficulty = null;
        SelectedDifficultyName = null;
        MaxTimeMinutes = 120;
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

    partial void OnSelectedCategoryNameChanged(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            SelectedCategory = null;
        }
        else if (Enum.TryParse<RecipeCategory>(value, out var category))
        {
            SelectedCategory = category;
        }
    }

    partial void OnSelectedDifficultyChanged(DifficultyLevel? value)
    {
        FilterRecipes();
    }

    partial void OnSelectedDifficultyNameChanged(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            SelectedDifficulty = null;
        }
        else if (Enum.TryParse<DifficultyLevel>(value, out var difficulty))
        {
            SelectedDifficulty = difficulty;
        }
    }

    partial void OnMaxTimeMinutesChanged(int value)
    {
        FilterRecipes();
    }

    /// <summary>
    /// Handles recipe selection and navigates to detail page
    /// Resets selection after navigation to allow selecting the same recipe again
    /// </summary>
    async partial void OnSelectedRecipeChanged(Recipe? value)
    {
        if (value != null)
        {
            await NavigateToRecipeDetail(value);
            SelectedRecipe = null; // Reset selection
        }
    }

    [RelayCommand]
    private async Task NavigateToRecipeDetail(Recipe recipe)
    {
        await Shell.Current.GoToAsync($"RecipeDetailPage?recipeId={recipe.Id}");
    }
}
