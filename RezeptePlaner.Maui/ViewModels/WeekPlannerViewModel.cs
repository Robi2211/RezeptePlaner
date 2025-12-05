using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using RezeptePlaner.Maui.Models;
using RezeptePlaner.Maui.Services;

namespace RezeptePlaner.Maui.ViewModels;

/// <summary>
/// ViewModel for the Week Planner page
/// </summary>
public partial class WeekPlannerViewModel : ObservableObject
{
    private readonly RecipeService _recipeService;

    [ObservableProperty]
    private ObservableCollection<Recipe> recipes = new();

    [ObservableProperty]
    private ObservableCollection<PlannerDay> weekDays = new();

    [ObservableProperty]
    private string weekDateRange = string.Empty;

    [ObservableProperty]
    private int totalPlannedMeals;

    [ObservableProperty]
    private Recipe? selectedRecipe;

    public WeekPlannerViewModel(RecipeService recipeService)
    {
        _recipeService = recipeService;
        LoadWeekPlanner();
    }

    [RelayCommand]
    private void LoadWeekPlanner()
    {
        Recipes = _recipeService.GetRecipes();
        
        var today = DateTime.Today;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
        var endOfWeek = startOfWeek.AddDays(6);
        
        WeekDateRange = $"{startOfWeek:d. MMMM} - {endOfWeek:d. MMMM yyyy}";

        string[] dayNames = { "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag", "Sonntag" };
        
        WeekDays = new ObservableCollection<PlannerDay>();
        
        for (int i = 0; i < 7; i++)
        {
            var date = startOfWeek.AddDays(i);
            var isToday = date == today;
            
            // Create sample meals for each day
            var meals = new ObservableCollection<PlannedMeal>();
            
            if (i < 5) // Weekdays have planned meals
            {
                var recipeIndex = i % Recipes.Count;
                if (Recipes.Count > 0)
                {
                    meals.Add(new PlannedMeal
                    {
                        Recipe = Recipes[recipeIndex],
                        MealType = "Frühstück"
                    });
                    
                    if (Recipes.Count > 1)
                    {
                        meals.Add(new PlannedMeal
                        {
                            Recipe = Recipes[(recipeIndex + 1) % Recipes.Count],
                            MealType = i % 2 == 0 ? "Mittagessen" : "Abendessen"
                        });
                    }
                }
            }
            
            WeekDays.Add(new PlannerDay
            {
                DayName = dayNames[i],
                DateString = date.ToString("d. MMM", new System.Globalization.CultureInfo("de-DE")),
                IsToday = isToday,
                Meals = meals
            });
        }

        TotalPlannedMeals = WeekDays.Sum(d => d.Meals.Count);
    }

    [RelayCommand]
    private async Task ViewRecipe(Recipe recipe)
    {
        if (recipe != null)
        {
            await Shell.Current.GoToAsync($"RecipeDetailPage?recipeId={recipe.Id}");
        }
    }

    [RelayCommand]
    private void RemoveMeal(PlannedMeal meal)
    {
        if (meal != null)
        {
            foreach (var day in WeekDays)
            {
                if (day.Meals.Contains(meal))
                {
                    day.Meals.Remove(meal);
                    break;
                }
            }
            TotalPlannedMeals = WeekDays.Sum(d => d.Meals.Count);
        }
    }

    [RelayCommand]
    private async Task AddMeal(PlannerDay day)
    {
        if (day == null || Recipes.Count == 0) return;

        // Simple implementation - in real app would show a picker
        var recipeNames = Recipes.Select(r => r.Title).ToArray();
        var selectedRecipeName = await Shell.Current.DisplayActionSheet(
            $"Rezept für {day.DayName} auswählen",
            "Abbrechen",
            null,
            recipeNames);

        if (selectedRecipeName == null || selectedRecipeName == "Abbrechen") return;

        var selectedRecipe = Recipes.FirstOrDefault(r => r.Title == selectedRecipeName);
        if (selectedRecipe == null) return;

        var mealType = await Shell.Current.DisplayActionSheet(
            "Mahlzeit auswählen",
            "Abbrechen",
            null,
            "Frühstück", "Mittagessen", "Abendessen");

        if (mealType == null || mealType == "Abbrechen") return;

        day.Meals.Add(new PlannedMeal
        {
            Recipe = selectedRecipe,
            MealType = mealType
        });

        TotalPlannedMeals = WeekDays.Sum(d => d.Meals.Count);
    }

    [RelayCommand]
    private void NavigatePreviousWeek()
    {
        // In a real app, this would load the previous week's data
    }

    [RelayCommand]
    private void NavigateNextWeek()
    {
        // In a real app, this would load the next week's data
    }
}

/// <summary>
/// Model for a planner day
/// </summary>
public partial class PlannerDay : ObservableObject
{
    [ObservableProperty]
    private string dayName = string.Empty;

    [ObservableProperty]
    private string dateString = string.Empty;

    [ObservableProperty]
    private bool isToday;

    [ObservableProperty]
    private ObservableCollection<PlannedMeal> meals = new();
}

/// <summary>
/// Model for a planned meal
/// </summary>
public partial class PlannedMeal : ObservableObject
{
    [ObservableProperty]
    private Recipe? recipe;

    [ObservableProperty]
    private string mealType = string.Empty;
}
