using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using RezeptePlaner.Maui.Models;
using RezeptePlaner.Maui.Services;

namespace RezeptePlaner.Maui.ViewModels;

/// <summary>
/// ViewModel for adding a new recipe with full validation
/// </summary>
public partial class AddRecipeViewModel : ObservableObject
{
    private readonly RecipeService _recipeService;

    // Form fields
    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private RecipeCategory selectedCategory = RecipeCategory.Mittagessen;

    [ObservableProperty]
    private DifficultyLevel selectedDifficulty = DifficultyLevel.Einfach;

    [ObservableProperty]
    private int prepTime = 15;

    [ObservableProperty]
    private int cookTime = 30;

    [ObservableProperty]
    private int servings = 2;

    [ObservableProperty]
    private string ingredientsText = string.Empty;

    [ObservableProperty]
    private string instructionsText = string.Empty;

    [ObservableProperty]
    private string tagsText = string.Empty;

    [ObservableProperty]
    private bool isVegetarian;

    [ObservableProperty]
    private bool isVegan;

    [ObservableProperty]
    private bool isGlutenFree;

    [ObservableProperty]
    private DateTime plannedDate = DateTime.Today;

    // Validation errors
    [ObservableProperty]
    private string titleError = string.Empty;

    [ObservableProperty]
    private string ingredientsError = string.Empty;

    [ObservableProperty]
    private string instructionsError = string.Empty;

    [ObservableProperty]
    private bool hasErrors;

    [ObservableProperty]
    private bool isFormValid;

    [ObservableProperty]
    private bool showSuccessMessage;

    [ObservableProperty]
    private Recipe? lastAddedRecipe;

    // Available options for pickers
    public ObservableCollection<RecipeCategory> Categories { get; } = 
        new(Enum.GetValues<RecipeCategory>());

    public ObservableCollection<DifficultyLevel> DifficultyLevels { get; } = 
        new(Enum.GetValues<DifficultyLevel>());

    public int MinServings => 1;
    public int MaxServings => 20;
    public int MinPrepTime => 0;
    public int MaxPrepTime => 180;
    public int MinCookTime => 0;
    public int MaxCookTime => 480;

    public AddRecipeViewModel(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    /// <summary>
    /// Validate the form
    /// </summary>
    [RelayCommand]
    private void ValidateForm()
    {
        HasErrors = false;
        TitleError = string.Empty;
        IngredientsError = string.Empty;
        InstructionsError = string.Empty;

        // Title validation
        if (string.IsNullOrWhiteSpace(Title))
        {
            TitleError = "Bitte geben Sie einen Titel ein.";
            HasErrors = true;
        }
        else if (Title.Length < 3)
        {
            TitleError = "Der Titel muss mindestens 3 Zeichen haben.";
            HasErrors = true;
        }
        else if (Title.Length > 100)
        {
            TitleError = "Der Titel darf maximal 100 Zeichen haben.";
            HasErrors = true;
        }

        // Ingredients validation
        if (string.IsNullOrWhiteSpace(IngredientsText))
        {
            IngredientsError = "Bitte geben Sie mindestens eine Zutat ein.";
            HasErrors = true;
        }
        else
        {
            var ingredientLines = IngredientsText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            if (ingredientLines.Length < 1)
            {
                IngredientsError = "Bitte geben Sie mindestens eine Zutat ein.";
                HasErrors = true;
            }
        }

        // Instructions validation
        if (string.IsNullOrWhiteSpace(InstructionsText))
        {
            InstructionsError = "Bitte geben Sie mindestens einen Zubereitungsschritt ein.";
            HasErrors = true;
        }
        else
        {
            var instructionLines = InstructionsText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            if (instructionLines.Length < 1)
            {
                InstructionsError = "Bitte geben Sie mindestens einen Zubereitungsschritt ein.";
                HasErrors = true;
            }
        }

        // Servings validation
        if (Servings < MinServings || Servings > MaxServings)
        {
            HasErrors = true;
        }

        IsFormValid = !HasErrors;
    }

    /// <summary>
    /// Save the recipe
    /// </summary>
    [RelayCommand]
    private async Task SaveRecipe()
    {
        ValidateForm();

        if (!IsFormValid)
        {
            await Shell.Current.DisplayAlert("Fehler", "Bitte korrigieren Sie die markierten Felder.", "OK");
            return;
        }

        var recipe = new Recipe
        {
            Title = Title.Trim(),
            Category = SelectedCategory,
            Difficulty = SelectedDifficulty,
            PrepTime = PrepTime,
            CookTime = CookTime,
            Servings = Servings,
            Ingredients = new ObservableCollection<string>(
                IngredientsText.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Trim())
                    .Where(i => !string.IsNullOrWhiteSpace(i))),
            Instructions = new ObservableCollection<string>(
                InstructionsText.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Trim())
                    .Where(i => !string.IsNullOrWhiteSpace(i))),
            Tags = new ObservableCollection<string>(
                TagsText.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Trim().ToLowerInvariant())
                    .Where(t => !string.IsNullOrWhiteSpace(t))),
            IsVegetarian = IsVegetarian,
            IsVegan = IsVegan,
            IsGlutenFree = IsGlutenFree,
            CreatedAt = DateTime.Now
        };

        _recipeService.AddRecipe(recipe);
        LastAddedRecipe = recipe;
        ShowSuccessMessage = true;
    }

    /// <summary>
    /// Reset the form
    /// </summary>
    [RelayCommand]
    private void ResetForm()
    {
        Title = string.Empty;
        SelectedCategory = RecipeCategory.Mittagessen;
        SelectedDifficulty = DifficultyLevel.Einfach;
        PrepTime = 15;
        CookTime = 30;
        Servings = 2;
        IngredientsText = string.Empty;
        InstructionsText = string.Empty;
        TagsText = string.Empty;
        IsVegetarian = false;
        IsVegan = false;
        IsGlutenFree = false;
        PlannedDate = DateTime.Today;

        TitleError = string.Empty;
        IngredientsError = string.Empty;
        InstructionsError = string.Empty;
        HasErrors = false;
        IsFormValid = false;
        ShowSuccessMessage = false;
        LastAddedRecipe = null;
    }

    /// <summary>
    /// Navigate to summary page
    /// </summary>
    [RelayCommand]
    private async Task ViewSummary()
    {
        if (LastAddedRecipe != null)
        {
            await Shell.Current.GoToAsync($"//SummaryPage?recipeId={LastAddedRecipe.Id}");
        }
    }

    /// <summary>
    /// Add another recipe
    /// </summary>
    [RelayCommand]
    private void AddAnother()
    {
        ResetForm();
    }

    partial void OnTitleChanged(string value) => ValidateFormCommand.Execute(null);
    partial void OnIngredientsTextChanged(string value) => ValidateFormCommand.Execute(null);
    partial void OnInstructionsTextChanged(string value) => ValidateFormCommand.Execute(null);
}
