using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RezeptePlaner.Maui.Models;
using RezeptePlaner.Maui.Services;

namespace RezeptePlaner.Maui.ViewModels;

/// <summary>
/// ViewModel for the Summary page after successful recipe creation
/// </summary>
[QueryProperty(nameof(RecipeId), "recipeId")]
public partial class SummaryViewModel : ObservableObject
{
    private readonly RecipeService _recipeService;

    [ObservableProperty]
    private string recipeId = string.Empty;

    [ObservableProperty]
    private Recipe? recipe;

    [ObservableProperty]
    private bool isLoaded;

    [ObservableProperty]
    private string successMessage = "Ihr Rezept wurde erfolgreich gespeichert!";

    public SummaryViewModel(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    partial void OnRecipeIdChanged(string value)
    {
        LoadRecipe();
    }

    /// <summary>
    /// Load the recipe details
    /// </summary>
    [RelayCommand]
    private void LoadRecipe()
    {
        if (!string.IsNullOrEmpty(RecipeId))
        {
            Recipe = _recipeService.GetRecipeById(RecipeId);
            IsLoaded = Recipe != null;
        }
    }

    /// <summary>
    /// Navigate to recipe list
    /// </summary>
    [RelayCommand]
    private async Task GoToRecipes()
    {
        await Shell.Current.GoToAsync("//RecipesPage");
    }

    /// <summary>
    /// Add another recipe
    /// </summary>
    [RelayCommand]
    private async Task AddAnotherRecipe()
    {
        await Shell.Current.GoToAsync("//AddRecipePage");
    }

    /// <summary>
    /// Toggle favorite
    /// </summary>
    [RelayCommand]
    private void ToggleFavorite()
    {
        if (Recipe != null)
        {
            _recipeService.ToggleFavorite(Recipe.Id);
        }
    }
}
