namespace CookMate.Maui.Models;

/// <summary>
/// Represents a meal planned for a specific day.
/// Used in the Week Planner feature.
/// </summary>
public class PlannedMeal
{
    public string RecipeId { get; set; } = string.Empty;
    
    public MealType MealType { get; set; }
    
    /// <summary>
    /// Reference to the actual recipe (populated at runtime).
    /// </summary>
    public Recipe? Recipe { get; set; }
}

/// <summary>
/// Types of meals during the day.
/// </summary>
public enum MealType
{
    Breakfast,
    Lunch,
    Dinner
}

/// <summary>
/// Represents a day in the week planner with its planned meals.
/// </summary>
public class DayPlan
{
    public string DayName { get; set; } = string.Empty;
    
    public string DateString { get; set; } = string.Empty;
    
    public bool IsToday { get; set; }
    
    public List<PlannedMeal> Meals { get; set; } = new();
}
