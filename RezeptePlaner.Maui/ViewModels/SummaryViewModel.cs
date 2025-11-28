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

    [ObservableProperty]
    private string? errorMessage;

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
    private async Task LoadRecipe()
    {
        ErrorMessage = null;
        
        if (string.IsNullOrEmpty(RecipeId))
        {
            IsLoaded = false;
            ErrorMessage = "Keine Rezept-ID angegeben.";
            return;
        }

        Recipe = _recipeService.GetRecipeById(RecipeId);
        
        if (Recipe == null)
        {
            IsLoaded = false;
            ErrorMessage = "Das Rezept konnte nicht gefunden werden.";
            await Shell.Current.DisplayAlert("Fehler", ErrorMessage, "OK");
        }
        else
        {
            IsLoaded = true;
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
    /// Toggle favorite and refresh UI
    /// </summary>
    [RelayCommand]
    private async Task ToggleFavorite()
    {
        if (Recipe != null)
        {
            _recipeService.ToggleFavorite(Recipe.Id);
            // Refresh the recipe to update UI
            Recipe = _recipeService.GetRecipeById(Recipe.Id);
            
            var statusMessage = Recipe?.IsFavorite == true 
                ? "Zu Favoriten hinzugefügt!" 
                : "Von Favoriten entfernt.";
            await Shell.Current.DisplayAlert("Favoriten", statusMessage, "OK");
        }
    }
}
