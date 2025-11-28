using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookMate.Maui.Models;
using CookMate.Maui.Services;
using System.Collections.ObjectModel;

namespace CookMate.Maui.ViewModels;

/// <summary>
/// ViewModel for the Dashboard page.
/// Displays today's meals, weekly overview, and quick stats.
/// Follows MVVM pattern with full data binding support.
/// </summary>
public partial class DashboardViewModel : ObservableObject
{
    private readonly IRecipeService _recipeService;
    private readonly IWeekPlannerService _weekPlannerService;
    private readonly INavigationService _navigationService;
    
    [ObservableProperty]
    private bool _isLoading;
    
    [ObservableProperty]
    private DayPlan? _todayPlan;
    
    [ObservableProperty]
    private ObservableCollection<DayPlan> _weekPlans = new();
    
    [ObservableProperty]
    private int _totalRecipes;
    
    [ObservableProperty]
    private int _totalFavorites;
    
    [ObservableProperty]
    private ObservableCollection<Recipe> _trendingRecipes = new();
    
    [ObservableProperty]
    private string _dateDisplay = string.Empty;
    
    public DashboardViewModel(
        IRecipeService recipeService,
        IWeekPlannerService weekPlannerService,
        INavigationService navigationService)
    {
        _recipeService = recipeService;
        _weekPlannerService = weekPlannerService;
        _navigationService = navigationService;
        
        // Set the current date display
        DateDisplay = DateTime.Now.ToString("dddd, d. MMMM", new System.Globalization.CultureInfo("de-DE"));
    }
    
    /// <summary>
    /// Initializes the dashboard data asynchronously.
    /// Called when the page appears.
    /// </summary>
    [RelayCommand]
    private async Task LoadDataAsync()
    {
        if (IsLoading) return;
        
        IsLoading = true;
        
        try
        {
            // Load all data in parallel for better performance
            var recipesTask = _recipeService.GetRecipesAsync();
            var weekPlanTask = _weekPlannerService.GetWeekPlanAsync();
            var todayPlanTask = _weekPlannerService.GetTodaysPlanAsync();
            
            await Task.WhenAll(recipesTask, weekPlanTask, todayPlanTask);
            
            var recipes = (await recipesTask).ToList();
            var weekPlans = (await weekPlanTask).ToList();
            
            // Update stats
            TotalRecipes = recipes.Count;
            TotalFavorites = recipes.Count(r => r.IsFavorite);
            
            // Update today's plan
            TodayPlan = await todayPlanTask;
            
            // Update week plans (excluding weekends for the overview)
            WeekPlans = new ObservableCollection<DayPlan>(
                weekPlans.Take(5)); // Monday to Friday
            
            // Set trending recipes (top 3, skipping the first)
            TrendingRecipes = new ObservableCollection<Recipe>(
                recipes.Skip(1).Take(3));
        }
        catch (Exception ex)
        {
            // Log error - in production, use proper logging
            System.Diagnostics.Debug.WriteLine($"Error loading dashboard: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    /// <summary>
    /// Navigates to the recipe detail page.
    /// </summary>
    [RelayCommand]
    private async Task ViewRecipeAsync(string recipeId)
    {
        if (string.IsNullOrEmpty(recipeId)) return;
        
        var parameters = new Dictionary<string, object>
        {
            { "RecipeId", recipeId }
        };
        
        await _navigationService.NavigateToAsync(Routes.RecipeDetail, parameters);
    }
    
    /// <summary>
    /// Navigates to the week planner page.
    /// </summary>
    [RelayCommand]
    private async Task GoToWeekPlannerAsync()
    {
        await _navigationService.NavigateToAsync(Routes.Planner);
    }
    
    /// <summary>
    /// Navigates to the recipes library.
    /// </summary>
    [RelayCommand]
    private async Task GoToRecipesAsync()
    {
        await _navigationService.NavigateToAsync(Routes.Recipes);
    }
}
