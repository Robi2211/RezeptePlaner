using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using RezeptePlaner.Maui.Models;
using RezeptePlaner.Maui.Services;

namespace RezeptePlaner.Maui.ViewModels;

/// <summary>
/// ViewModel for the Dashboard page
/// </summary>
public partial class DashboardViewModel : ObservableObject
{
    private readonly RecipeService _recipeService;

    [ObservableProperty]
    private ObservableCollection<Recipe> recipes = new();

    [ObservableProperty]
    private ObservableCollection<Recipe> todayRecipes = new();

    [ObservableProperty]
    private ObservableCollection<Recipe> trendingRecipes = new();

    [ObservableProperty]
    private ObservableCollection<WeekDay> weekDays = new();

    [ObservableProperty]
    private int totalRecipes;

    [ObservableProperty]
    private int totalFavorites;

    [ObservableProperty]
    private string currentDate = string.Empty;

    [ObservableProperty]
    private Recipe? selectedRecipe;

    public DashboardViewModel(RecipeService recipeService)
    {
        _recipeService = recipeService;
        LoadDashboardData();
    }

    [RelayCommand]
    private void LoadDashboardData()
    {
        Recipes = _recipeService.GetRecipes();
        TotalRecipes = Recipes.Count;
        TotalFavorites = Recipes.Count(r => r.IsFavorite);
        CurrentDate = DateTime.Now.ToString("dddd, d. MMMM", new System.Globalization.CultureInfo("de-DE"));

        // Get today's recipes (first 2 recipes as example)
        TodayRecipes = new ObservableCollection<Recipe>(Recipes.Take(2));

        // Get trending recipes (recipes 1-3)
        TrendingRecipes = new ObservableCollection<Recipe>(Recipes.Skip(1).Take(3));

        // Create week days overview
        var today = DateTime.Today;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
        
        WeekDays = new ObservableCollection<WeekDay>();
        string[] dayNames = { "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag", "Sonntag" };
        
        for (int i = 0; i < 5; i++) // Show weekdays only
        {
            var date = startOfWeek.AddDays(i);
            var isToday = date == today;
            var dayRecipes = new ObservableCollection<Recipe>(Recipes.Skip(i % Recipes.Count).Take(2));
            
            WeekDays.Add(new WeekDay
            {
                DayName = dayNames[i],
                DateString = date.ToString("d. MMM", new System.Globalization.CultureInfo("de-DE")),
                IsToday = isToday,
                Recipes = dayRecipes
            });
        }
    }

    [RelayCommand]
    private async Task NavigateToRecipes()
    {
        await Shell.Current.GoToAsync("//RecipesPage");
    }

    [RelayCommand]
    private async Task NavigateToPlanner()
    {
        await Shell.Current.GoToAsync("//WeekPlannerPage");
    }

    [RelayCommand]
    private async Task ViewRecipe(Recipe recipe)
    {
        if (recipe != null)
        {
            await Shell.Current.GoToAsync($"RecipeDetailPage?recipeId={recipe.Id}");
        }
    }
}

/// <summary>
/// Model for week day display
/// </summary>
public partial class WeekDay : ObservableObject
{
    [ObservableProperty]
    private string dayName = string.Empty;

    [ObservableProperty]
    private string dateString = string.Empty;

    [ObservableProperty]
    private bool isToday;

    [ObservableProperty]
    private ObservableCollection<Recipe> recipes = new();
}
