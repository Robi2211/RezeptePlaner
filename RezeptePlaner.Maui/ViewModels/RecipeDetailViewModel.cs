using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RezeptePlaner.Maui.Models;
using RezeptePlaner.Maui.Services;
using System.Collections.ObjectModel;

namespace RezeptePlaner.Maui.ViewModels;

/// <summary>
/// Represents an instruction with its step number
/// </summary>
public class InstructionStep
{
    /// <summary>
    /// The step number in the instruction sequence
    /// </summary>
    public int Number { get; init; }
    
    /// <summary>
    /// The instruction text
    /// </summary>
    public string Text { get; init; } = string.Empty;
}

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

    [ObservableProperty]
    private ObservableCollection<InstructionStep> instructionSteps = new();

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

        try
        {
            IsLoading = true;
            Recipe = _recipeService.GetRecipeById(RecipeId);
            
            // Create indexed instruction steps
            if (Recipe?.Instructions != null)
            {
                InstructionSteps = new ObservableCollection<InstructionStep>(
                    Recipe.Instructions.Select((text, index) => new InstructionStep
                    {
                        Number = index + 1,
                        Text = text
                    })
                );
            }
            else
            {
                InstructionSteps = new ObservableCollection<InstructionStep>();
            }
        }
        finally
        {
            IsLoading = false;
        }
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
