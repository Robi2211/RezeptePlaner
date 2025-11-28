using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookMate.Maui.Models;
using CookMate.Maui.Services;
using System.Collections.ObjectModel;

namespace CookMate.Maui.ViewModels;

/// <summary>
/// ViewModel for the Add Recipe page.
/// Handles form data and validation for creating new recipes.
/// </summary>
public partial class AddRecipeViewModel : ObservableObject
{
    private readonly IRecipeService _recipeService;
    private readonly INavigationService _navigationService;
    
    [ObservableProperty]
    private bool _isSaving;
    
    [ObservableProperty]
    private string _title = string.Empty;
    
    [ObservableProperty]
    private RecipeCategory _selectedCategory = RecipeCategory.Lunch;
    
    [ObservableProperty]
    private RecipeDifficulty _selectedDifficulty = RecipeDifficulty.Medium;
    
    [ObservableProperty]
    private int _prepTime = 15;
    
    [ObservableProperty]
    private int _cookTime = 30;
    
    [ObservableProperty]
    private int _servings = 4;
    
    [ObservableProperty]
    private string _imageUrl = string.Empty;
    
    [ObservableProperty]
    private ObservableCollection<string> _ingredients = new() { string.Empty };
    
    [ObservableProperty]
    private ObservableCollection<string> _instructions = new() { string.Empty };
    
    [ObservableProperty]
    private ObservableCollection<string> _tags = new();
    
    [ObservableProperty]
    private string _currentTag = string.Empty;
    
    [ObservableProperty]
    private bool _hasImage;
    
    /// <summary>
    /// Available categories for the picker.
    /// </summary>
    public ObservableCollection<CategoryOption> Categories { get; } = new()
    {
        new CategoryOption { Value = RecipeCategory.Breakfast, Label = "Frühstück" },
        new CategoryOption { Value = RecipeCategory.Lunch, Label = "Mittagessen" },
        new CategoryOption { Value = RecipeCategory.Dinner, Label = "Abendessen" },
        new CategoryOption { Value = RecipeCategory.Snack, Label = "Snack" }
    };
    
    /// <summary>
    /// Available difficulties for the picker.
    /// </summary>
    public ObservableCollection<DifficultyOption> Difficulties { get; } = new()
    {
        new DifficultyOption { Value = RecipeDifficulty.Easy, Label = "Einfach" },
        new DifficultyOption { Value = RecipeDifficulty.Medium, Label = "Mittel" },
        new DifficultyOption { Value = RecipeDifficulty.Hard, Label = "Schwer" }
    };
    
    public AddRecipeViewModel(
        IRecipeService recipeService,
        INavigationService navigationService)
    {
        _recipeService = recipeService;
        _navigationService = navigationService;
    }
    
    /// <summary>
    /// Updates HasImage when ImageUrl changes.
    /// </summary>
    partial void OnImageUrlChanged(string value)
    {
        HasImage = !string.IsNullOrWhiteSpace(value);
    }
    
    /// <summary>
    /// Adds a new empty ingredient field.
    /// </summary>
    [RelayCommand]
    private void AddIngredient()
    {
        Ingredients.Add(string.Empty);
    }
    
    /// <summary>
    /// Removes an ingredient at the specified index.
    /// </summary>
    [RelayCommand]
    private void RemoveIngredient(int index)
    {
        if (index >= 0 && index < Ingredients.Count && Ingredients.Count > 1)
        {
            Ingredients.RemoveAt(index);
        }
    }
    
    /// <summary>
    /// Adds a new empty instruction step.
    /// </summary>
    [RelayCommand]
    private void AddInstruction()
    {
        Instructions.Add(string.Empty);
    }
    
    /// <summary>
    /// Removes an instruction at the specified index.
    /// </summary>
    [RelayCommand]
    private void RemoveInstruction(int index)
    {
        if (index >= 0 && index < Instructions.Count && Instructions.Count > 1)
        {
            Instructions.RemoveAt(index);
        }
    }
    
    /// <summary>
    /// Adds the current tag to the tags list.
    /// </summary>
    [RelayCommand]
    private void AddTag()
    {
        var tag = CurrentTag.Trim();
        if (!string.IsNullOrEmpty(tag) && !Tags.Contains(tag))
        {
            Tags.Add(tag);
            CurrentTag = string.Empty;
        }
    }
    
    /// <summary>
    /// Removes a tag from the list.
    /// </summary>
    [RelayCommand]
    private void RemoveTag(string tag)
    {
        if (!string.IsNullOrEmpty(tag))
        {
            Tags.Remove(tag);
        }
    }
    
    /// <summary>
    /// Clears the image URL.
    /// </summary>
    [RelayCommand]
    private void ClearImage()
    {
        ImageUrl = string.Empty;
    }
    
    /// <summary>
    /// Saves the recipe and navigates back.
    /// </summary>
    [RelayCommand]
    private async Task SaveRecipeAsync()
    {
        // Validation
        if (string.IsNullOrWhiteSpace(Title))
        {
            await Application.Current!.MainPage!.DisplayAlert(
                "Fehler",
                "Bitte gib einen Titel ein",
                "OK");
            return;
        }
        
        var filteredIngredients = Ingredients.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
        if (filteredIngredients.Count == 0)
        {
            await Application.Current!.MainPage!.DisplayAlert(
                "Fehler",
                "Bitte füge mindestens eine Zutat hinzu",
                "OK");
            return;
        }
        
        var filteredInstructions = Instructions.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
        if (filteredInstructions.Count == 0)
        {
            await Application.Current!.MainPage!.DisplayAlert(
                "Fehler",
                "Bitte füge mindestens einen Zubereitungsschritt hinzu",
                "OK");
            return;
        }
        
        IsSaving = true;
        
        try
        {
            var newRecipe = new Recipe
            {
                Id = DateTime.Now.Ticks.ToString(),
                Title = Title.Trim(),
                Category = SelectedCategory,
                PrepTime = PrepTime,
                CookTime = CookTime,
                Servings = Servings,
                Difficulty = SelectedDifficulty,
                Ingredients = filteredIngredients,
                Instructions = filteredInstructions,
                Image = !string.IsNullOrWhiteSpace(ImageUrl) 
                    ? ImageUrl 
                    : "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?w=800",
                IsFavorite = false,
                Tags = Tags.ToList()
            };
            
            await _recipeService.AddRecipeAsync(newRecipe);
            await _navigationService.GoBackAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error saving recipe: {ex.Message}");
            await Application.Current!.MainPage!.DisplayAlert(
                "Fehler",
                "Das Rezept konnte nicht gespeichert werden",
                "OK");
        }
        finally
        {
            IsSaving = false;
        }
    }
    
    /// <summary>
    /// Cancels and navigates back.
    /// </summary>
    [RelayCommand]
    private async Task CancelAsync()
    {
        await _navigationService.GoBackAsync();
    }
}
