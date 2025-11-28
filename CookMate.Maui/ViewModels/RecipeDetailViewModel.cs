using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookMate.Maui.Models;
using CookMate.Maui.Services;

namespace CookMate.Maui.ViewModels;

/// <summary>
/// ViewModel for the Recipe Detail page.
/// Displays full recipe information including ingredients and instructions.
/// Uses query parameters to receive the recipe ID.
/// </summary>
[QueryProperty(nameof(RecipeId), "RecipeId")]
public partial class RecipeDetailViewModel : ObservableObject
{
    private readonly IRecipeService _recipeService;
    private readonly INavigationService _navigationService;
    
    [ObservableProperty]
    private bool _isLoading;
    
    [ObservableProperty]
    private string _recipeId = string.Empty;
    
    [ObservableProperty]
    private Recipe? _recipe;
    
    [ObservableProperty]
    private string _categoryLabel = string.Empty;
    
    [ObservableProperty]
    private string _difficultyLabel = string.Empty;
    
    public RecipeDetailViewModel(
        IRecipeService recipeService,
        INavigationService navigationService)
    {
        _recipeService = recipeService;
        _navigationService = navigationService;
    }
    
    /// <summary>
    /// Called when RecipeId is set via navigation parameters.
    /// Triggers data loading.
    /// </summary>
    partial void OnRecipeIdChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _ = LoadRecipeAsync();
        }
    }
    
    /// <summary>
    /// Loads the recipe data from the service.
    /// </summary>
    [RelayCommand]
    private async Task LoadRecipeAsync()
    {
        if (string.IsNullOrEmpty(RecipeId) || IsLoading) return;
        
        IsLoading = true;
        
        try
        {
            Recipe = await _recipeService.GetRecipeByIdAsync(RecipeId);
            
            if (Recipe != null)
            {
                CategoryLabel = GetCategoryLabel(Recipe.Category);
                DifficultyLabel = GetDifficultyLabel(Recipe.Difficulty);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading recipe: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    /// <summary>
    /// Toggles the favorite status of the current recipe.
    /// </summary>
    [RelayCommand]
    private async Task ToggleFavoriteAsync()
    {
        if (Recipe == null) return;
        
        await _recipeService.ToggleFavoriteAsync(Recipe.Id);
        Recipe.IsFavorite = !Recipe.IsFavorite;
        
        // Force property change notification
        OnPropertyChanged(nameof(Recipe));
    }
    
    /// <summary>
    /// Navigates back to the previous page.
    /// </summary>
    [RelayCommand]
    private async Task GoBackAsync()
    {
        await _navigationService.GoBackAsync();
    }
    
    /// <summary>
    /// Shares the recipe (placeholder for platform-specific implementation).
    /// </summary>
    [RelayCommand]
    private async Task ShareRecipeAsync()
    {
        if (Recipe == null) return;
        
        await Share.RequestAsync(new ShareTextRequest
        {
            Title = "Rezept teilen",
            Text = $"Schau dir dieses Rezept an: {Recipe.Title}",
            Subject = Recipe.Title
        });
    }
    
    /// <summary>
    /// Gets the German label for a recipe category.
    /// </summary>
    private static string GetCategoryLabel(RecipeCategory category)
    {
        return category switch
        {
            RecipeCategory.Breakfast => "Frühstück",
            RecipeCategory.Lunch => "Mittagessen",
            RecipeCategory.Dinner => "Abendessen",
            RecipeCategory.Snack => "Snack",
            _ => category.ToString()
        };
    }
    
    /// <summary>
    /// Gets the German label for a difficulty level.
    /// </summary>
    private static string GetDifficultyLabel(RecipeDifficulty difficulty)
    {
        return difficulty switch
        {
            RecipeDifficulty.Easy => "Einfach",
            RecipeDifficulty.Medium => "Mittel",
            RecipeDifficulty.Hard => "Schwer",
            _ => difficulty.ToString()
        };
    }
}
