using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookMate.Maui.Models;
using CookMate.Maui.Services;
using System.Collections.ObjectModel;

namespace CookMate.Maui.ViewModels;

/// <summary>
/// ViewModel for the Week Planner page.
/// Manages weekly meal planning with add/remove functionality.
/// </summary>
public partial class WeekPlannerViewModel : ObservableObject
{
    private readonly IWeekPlannerService _weekPlannerService;
    private readonly IRecipeService _recipeService;
    private readonly INavigationService _navigationService;
    
    [ObservableProperty]
    private bool _isLoading;
    
    [ObservableProperty]
    private ObservableCollection<DayPlan> _dayPlans = new();
    
    [ObservableProperty]
    private int _totalPlannedMeals;
    
    [ObservableProperty]
    private string _weekDateRange = "18. - 24. November 2025";
    
    [ObservableProperty]
    private ObservableCollection<Recipe> _availableRecipes = new();
    
    public WeekPlannerViewModel(
        IWeekPlannerService weekPlannerService,
        IRecipeService recipeService,
        INavigationService navigationService)
    {
        _weekPlannerService = weekPlannerService;
        _recipeService = recipeService;
        _navigationService = navigationService;
    }
    
    /// <summary>
    /// Loads the weekly plan data.
    /// </summary>
    [RelayCommand]
    private async Task LoadDataAsync()
    {
        if (IsLoading) return;
        
        IsLoading = true;
        
        try
        {
            var plans = await _weekPlannerService.GetWeekPlanAsync();
            DayPlans = new ObservableCollection<DayPlan>(plans);
            
            TotalPlannedMeals = await _weekPlannerService.GetTotalPlannedMealsAsync();
            
            // Load available recipes for adding meals
            var recipes = await _recipeService.GetRecipesAsync();
            AvailableRecipes = new ObservableCollection<Recipe>(recipes);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading week plan: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    /// <summary>
    /// Removes a meal from a specific day.
    /// </summary>
    [RelayCommand]
    private async Task RemoveMealAsync(RemoveMealParameter parameter)
    {
        if (parameter == null) return;
        
        await _weekPlannerService.RemoveMealAsync(parameter.DayName, parameter.RecipeId);
        
        // Refresh the data
        await LoadDataAsync();
    }
    
    /// <summary>
    /// Adds a meal to a specific day.
    /// Uses an action sheet to select the recipe and meal type.
    /// </summary>
    [RelayCommand]
    private async Task AddMealAsync(string dayName)
    {
        if (string.IsNullOrEmpty(dayName) || AvailableRecipes.Count == 0) return;
        
        // Show recipe selection dialog
        var recipeNames = AvailableRecipes.Select(r => r.Title).ToArray();
        var selectedRecipe = await Application.Current!.MainPage!.DisplayActionSheet(
            $"Rezept für {dayName} auswählen",
            "Abbrechen",
            null,
            recipeNames);
        
        if (string.IsNullOrEmpty(selectedRecipe) || selectedRecipe == "Abbrechen")
            return;
        
        var recipe = AvailableRecipes.FirstOrDefault(r => r.Title == selectedRecipe);
        if (recipe == null) return;
        
        // Show meal type selection
        var mealType = await Application.Current.MainPage.DisplayActionSheet(
            "Mahlzeit auswählen",
            "Abbrechen",
            null,
            "Frühstück", "Mittagessen", "Abendessen");
        
        if (string.IsNullOrEmpty(mealType) || mealType == "Abbrechen")
            return;
        
        var mealTypeEnum = mealType switch
        {
            "Frühstück" => MealType.Breakfast,
            "Mittagessen" => MealType.Lunch,
            _ => MealType.Dinner
        };
        
        var plannedMeal = new PlannedMeal
        {
            RecipeId = recipe.Id,
            MealType = mealTypeEnum
        };
        
        await _weekPlannerService.AddMealAsync(dayName, plannedMeal);
        
        // Refresh the data
        await LoadDataAsync();
    }
    
    /// <summary>
    /// Navigates to recipe detail page.
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
    /// Navigates to previous week (placeholder).
    /// </summary>
    [RelayCommand]
    private Task PreviousWeekAsync()
    {
        // In a full implementation, this would navigate to the previous week
        System.Diagnostics.Debug.WriteLine("Navigate to previous week");
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Navigates to next week (placeholder).
    /// </summary>
    [RelayCommand]
    private Task NextWeekAsync()
    {
        // In a full implementation, this would navigate to the next week
        System.Diagnostics.Debug.WriteLine("Navigate to next week");
        return Task.CompletedTask;
    }
}

/// <summary>
/// Parameter class for removing a meal from a day.
/// </summary>
public class RemoveMealParameter
{
    public string DayName { get; set; } = string.Empty;
    public string RecipeId { get; set; } = string.Empty;
}
