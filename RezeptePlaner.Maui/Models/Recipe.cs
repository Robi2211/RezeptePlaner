using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace RezeptePlaner.Maui.Models;

/// <summary>
/// Recipe category enumeration
/// </summary>
public enum RecipeCategory
{
    Frühstück,
    Mittagessen,
    Abendessen,
    Snack
}

/// <summary>
/// Difficulty level enumeration
/// </summary>
public enum DifficultyLevel
{
    Einfach,
    Mittel,
    Schwer
}

/// <summary>
/// Recipe model representing a cooking recipe
/// </summary>
public partial class Recipe : ObservableObject
{
    [ObservableProperty]
    private string id = string.Empty;

    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private RecipeCategory category = RecipeCategory.Mittagessen;

    [ObservableProperty]
    private int prepTime;

    [ObservableProperty]
    private int cookTime;

    [ObservableProperty]
    private int servings = 2;

    [ObservableProperty]
    private DifficultyLevel difficulty = DifficultyLevel.Einfach;

    [ObservableProperty]
    private ObservableCollection<string> ingredients = new();

    [ObservableProperty]
    private ObservableCollection<string> instructions = new();

    [ObservableProperty]
    private string imageUrl = string.Empty;

    [ObservableProperty]
    private bool isFavorite;

    [ObservableProperty]
    private ObservableCollection<string> tags = new();

    [ObservableProperty]
    private DateTime createdAt = DateTime.Now;

    [ObservableProperty]
    private bool isVegetarian;

    [ObservableProperty]
    private bool isVegan;

    [ObservableProperty]
    private bool isGlutenFree;

    /// <summary>
    /// Total cooking time (prep + cook)
    /// </summary>
    public int TotalTime => PrepTime + CookTime;

    /// <summary>
    /// Display text for difficulty
    /// </summary>
    public string DifficultyDisplay => Difficulty.ToString();

    /// <summary>
    /// Display text for category
    /// </summary>
    public string CategoryDisplay => Category.ToString();

    /// <summary>
    /// Ingredients as formatted string for display
    /// </summary>
    public string IngredientsDisplay => string.Join("\n• ", Ingredients);

    /// <summary>
    /// Instructions as formatted string for display
    /// </summary>
    public string InstructionsDisplay => string.Join("\n", Instructions.Select((step, index) => $"{index + 1}. {step}"));
}
