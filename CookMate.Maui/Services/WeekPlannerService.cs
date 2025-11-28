using CookMate.Maui.Models;

namespace CookMate.Maui.Services;

/// <summary>
/// Service for managing the weekly meal planner.
/// Handles planned meals across the week with async operations.
/// </summary>
public interface IWeekPlannerService
{
    /// <summary>
    /// Gets all day plans for the current week.
    /// </summary>
    Task<IEnumerable<DayPlan>> GetWeekPlanAsync();
    
    /// <summary>
    /// Adds a meal to a specific day.
    /// </summary>
    Task AddMealAsync(string dayName, PlannedMeal meal);
    
    /// <summary>
    /// Removes a meal from a specific day.
    /// </summary>
    Task RemoveMealAsync(string dayName, string recipeId);
    
    /// <summary>
    /// Gets the total number of planned meals for the week.
    /// </summary>
    Task<int> GetTotalPlannedMealsAsync();
    
    /// <summary>
    /// Gets today's planned meals.
    /// </summary>
    Task<DayPlan?> GetTodaysPlanAsync();
}

/// <summary>
/// In-memory implementation of the week planner service.
/// Maintains weekly meal plans with sample data.
/// </summary>
public class WeekPlannerService : IWeekPlannerService
{
    private readonly Dictionary<string, List<PlannedMeal>> _weekPlan;
    private readonly IRecipeService _recipeService;
    
    // Static list of weekday names in German
    private static readonly string[] WeekDays = 
    { 
        "Montag", "Dienstag", "Mittwoch", "Donnerstag", 
        "Freitag", "Samstag", "Sonntag" 
    };
    
    private static readonly string[] Dates = 
    { 
        "18. Nov", "19. Nov", "20. Nov", "21. Nov", 
        "22. Nov", "23. Nov", "24. Nov" 
    };
    
    public WeekPlannerService(IRecipeService recipeService)
    {
        _recipeService = recipeService;
        _weekPlan = InitializeSampleWeekPlan();
    }
    
    public async Task<IEnumerable<DayPlan>> GetWeekPlanAsync()
    {
        var dayPlans = new List<DayPlan>();
        
        for (int i = 0; i < WeekDays.Length; i++)
        {
            var dayName = WeekDays[i];
            var meals = _weekPlan.ContainsKey(dayName) 
                ? _weekPlan[dayName] 
                : new List<PlannedMeal>();
            
            // Populate recipe references
            foreach (var meal in meals)
            {
                meal.Recipe = await _recipeService.GetRecipeByIdAsync(meal.RecipeId);
            }
            
            dayPlans.Add(new DayPlan
            {
                DayName = dayName,
                DateString = Dates[i],
                IsToday = i == 3, // Thursday is "today" in the sample data
                Meals = meals.Where(m => m.Recipe != null).ToList()
            });
        }
        
        return dayPlans;
    }
    
    public Task AddMealAsync(string dayName, PlannedMeal meal)
    {
        if (!_weekPlan.ContainsKey(dayName))
        {
            _weekPlan[dayName] = new List<PlannedMeal>();
        }
        
        _weekPlan[dayName].Add(meal);
        return Task.CompletedTask;
    }
    
    public Task RemoveMealAsync(string dayName, string recipeId)
    {
        if (_weekPlan.ContainsKey(dayName))
        {
            _weekPlan[dayName].RemoveAll(m => m.RecipeId == recipeId);
        }
        return Task.CompletedTask;
    }
    
    public Task<int> GetTotalPlannedMealsAsync()
    {
        var count = _weekPlan.Values.Sum(meals => meals.Count);
        return Task.FromResult(count);
    }
    
    public async Task<DayPlan?> GetTodaysPlanAsync()
    {
        var weekPlan = await GetWeekPlanAsync();
        return weekPlan.FirstOrDefault(d => d.IsToday);
    }
    
    /// <summary>
    /// Initializes sample week plan data matching the original React app.
    /// </summary>
    private static Dictionary<string, List<PlannedMeal>> InitializeSampleWeekPlan()
    {
        return new Dictionary<string, List<PlannedMeal>>
        {
            ["Montag"] = new List<PlannedMeal>
            {
                new PlannedMeal { RecipeId = "1", MealType = MealType.Breakfast },
                new PlannedMeal { RecipeId = "3", MealType = MealType.Lunch }
            },
            ["Dienstag"] = new List<PlannedMeal>
            {
                new PlannedMeal { RecipeId = "4", MealType = MealType.Breakfast },
                new PlannedMeal { RecipeId = "2", MealType = MealType.Dinner }
            },
            ["Mittwoch"] = new List<PlannedMeal>
            {
                new PlannedMeal { RecipeId = "1", MealType = MealType.Breakfast },
                new PlannedMeal { RecipeId = "5", MealType = MealType.Dinner }
            },
            ["Donnerstag"] = new List<PlannedMeal>
            {
                new PlannedMeal { RecipeId = "6", MealType = MealType.Breakfast },
                new PlannedMeal { RecipeId = "3", MealType = MealType.Lunch }
            },
            ["Freitag"] = new List<PlannedMeal>
            {
                new PlannedMeal { RecipeId = "4", MealType = MealType.Breakfast },
                new PlannedMeal { RecipeId = "2", MealType = MealType.Dinner }
            },
            ["Samstag"] = new List<PlannedMeal>(),
            ["Sonntag"] = new List<PlannedMeal>()
        };
    }
}
