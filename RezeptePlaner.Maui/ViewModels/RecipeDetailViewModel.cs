using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RezeptePlaner.Maui.Models;
using RezeptePlaner.Maui.Services;

namespace RezeptePlaner.Maui.ViewModels;

/// <summary>
/// ViewModel for the Recipe Detail page
/// </summary>
[QueryProperty(nameof(RecipeId), "recipeId")]
public partial class RecipeDetailViewModel : ObservableObject
{
    private readonly RecipeService _recipeService;

    [ObservableProperty]
    private string recipeId = string.Empty;

    [ObservableProperty]
    private Recipe? recipe;

    [ObservableProperty]
    private bool isLoading;

    public RecipeDetailViewModel(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    partial void OnRecipeIdChanged(string value)
    {
        LoadRecipe();
    }

    [RelayCommand]
    private void LoadRecipe()
    {
        if (string.IsNullOrEmpty(RecipeId)) return;

        IsLoading = true;
        Recipe = _recipeService.GetRecipeById(RecipeId);
        IsLoading = false;
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private void ToggleFavorite()
    {
        if (Recipe != null)
        {
            _recipeService.ToggleFavorite(Recipe.Id);
            Recipe.IsFavorite = !Recipe.IsFavorite;
            OnPropertyChanged(nameof(Recipe));
        }
    }
}
