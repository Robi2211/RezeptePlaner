namespace CookMate.Maui.Models;

/// <summary>
/// Represents a recipe in the CookMate application.
/// This model mirrors the original TypeScript Recipe type but follows C# conventions.
/// </summary>
public class Recipe
{
    public string Id { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;
    
    public RecipeCategory Category { get; set; }
    
    /// <summary>
    /// Preparation time in minutes.
    /// </summary>
    public int PrepTime { get; set; }
    
    /// <summary>
    /// Cooking time in minutes.
    /// </summary>
    public int CookTime { get; set; }
    
    /// <summary>
    /// Total time is calculated as prep + cook time.
    /// </summary>
    public int TotalTime => PrepTime + CookTime;
    
    public int Servings { get; set; }
    
    public RecipeDifficulty Difficulty { get; set; }
    
    public List<string> Ingredients { get; set; } = new();
    
    public List<string> Instructions { get; set; } = new();
    
    /// <summary>
    /// URL to the recipe image.
    /// </summary>
    public string Image { get; set; } = string.Empty;
    
    public bool IsFavorite { get; set; }
    
    public List<string> Tags { get; set; } = new();
}

/// <summary>
/// Recipe categories matching the original TypeScript enum.
/// </summary>
public enum RecipeCategory
{
    Breakfast,
    Lunch,
    Dinner,
    Snack
}

/// <summary>
/// Difficulty levels for recipes.
/// </summary>
public enum RecipeDifficulty
{
    Easy,
    Medium,
    Hard
}
