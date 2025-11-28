using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookMate.Maui.Models;
using CookMate.Maui.Services;
using System.Collections.ObjectModel;

namespace CookMate.Maui.ViewModels;

/// <summary>
/// ViewModel for the Recipe Library page.
/// Provides search, filtering, and recipe listing functionality.
/// </summary>
public partial class RecipeLibraryViewModel : ObservableObject
{
    private readonly IRecipeService _recipeService;
    private readonly INavigationService _navigationService;
    
    private List<Recipe> _allRecipes = new();
    
    [ObservableProperty]
    private bool _isLoading;
    
    [ObservableProperty]
    private string _searchQuery = string.Empty;
    
    [ObservableProperty]
    private RecipeCategory? _selectedCategory;
    
    [ObservableProperty]
    private RecipeDifficulty? _selectedDifficulty;
    
    [ObservableProperty]
    private int _maxTimeMinutes = 120;
    
    [ObservableProperty]
    private ObservableCollection<Recipe> _filteredRecipes = new();
    
    [ObservableProperty]
    private int _resultCount;
    
    /// <summary>
    /// Available category filter options.
    /// </summary>
    public ObservableCollection<CategoryOption> Categories { get; } = new()
    {
        new CategoryOption { Value = null, Label = "Alle Kategorien" },
        new CategoryOption { Value = RecipeCategory.Breakfast, Label = "Frühstück" },
        new CategoryOption { Value = RecipeCategory.Lunch, Label = "Mittagessen" },
        new CategoryOption { Value = RecipeCategory.Dinner, Label = "Abendessen" },
        new CategoryOption { Value = RecipeCategory.Snack, Label = "Snack" }
    };
    
    /// <summary>
    /// Available difficulty filter options.
    /// </summary>
    public ObservableCollection<DifficultyOption> Difficulties { get; } = new()
    {
        new DifficultyOption { Value = null, Label = "Alle Schwierigkeiten" },
        new DifficultyOption { Value = RecipeDifficulty.Easy, Label = "Einfach" },
        new DifficultyOption { Value = RecipeDifficulty.Medium, Label = "Mittel" },
        new DifficultyOption { Value = RecipeDifficulty.Hard, Label = "Schwer" }
    };
    
    public RecipeLibraryViewModel(
        IRecipeService recipeService,
        INavigationService navigationService)
    {
        _recipeService = recipeService;
        _navigationService = navigationService;
    }
    
    /// <summary>
    /// Triggered when search query changes - applies filters.
    /// </summary>
    partial void OnSearchQueryChanged(string value)
    {
        ApplyFilters();
    }
    
    /// <summary>
    /// Triggered when category filter changes - applies filters.
    /// </summary>
    partial void OnSelectedCategoryChanged(RecipeCategory? value)
    {
        ApplyFilters();
    }
    
    /// <summary>
    /// Triggered when difficulty filter changes - applies filters.
    /// </summary>
    partial void OnSelectedDifficultyChanged(RecipeDifficulty? value)
    {
        ApplyFilters();
    }
    
    /// <summary>
    /// Triggered when max time filter changes - applies filters.
    /// </summary>
    partial void OnMaxTimeMinutesChanged(int value)
    {
        ApplyFilters();
    }
    
    /// <summary>
    /// Loads all recipes from the service.
    /// </summary>
    [RelayCommand]
    private async Task LoadDataAsync()
    {
        if (IsLoading) return;
        
        IsLoading = true;
        
        try
        {
            _allRecipes = (await _recipeService.GetRecipesAsync()).ToList();
            ApplyFilters();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading recipes: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    /// <summary>
    /// Applies all current filters to the recipe list.
    /// </summary>
    private void ApplyFilters()
    {
        var query = _allRecipes.AsEnumerable();
        
        // Search filter
        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            var lowerQuery = SearchQuery.ToLowerInvariant();
            query = query.Where(r =>
                r.Title.ToLowerInvariant().Contains(lowerQuery) ||
                r.Tags.Any(t => t.ToLowerInvariant().Contains(lowerQuery)));
        }
        
        // Category filter
        if (SelectedCategory.HasValue)
        {
            query = query.Where(r => r.Category == SelectedCategory.Value);
        }
        
        // Difficulty filter
        if (SelectedDifficulty.HasValue)
        {
            query = query.Where(r => r.Difficulty == SelectedDifficulty.Value);
        }
        
        // Time filter
        if (MaxTimeMinutes < 120)
        {
            query = query.Where(r => r.TotalTime <= MaxTimeMinutes);
        }
        
        var results = query.ToList();
        FilteredRecipes = new ObservableCollection<Recipe>(results);
        ResultCount = results.Count;
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
    /// Toggles the favorite status of a recipe.
    /// </summary>
    [RelayCommand]
    private async Task ToggleFavoriteAsync(string recipeId)
    {
        if (string.IsNullOrEmpty(recipeId)) return;
        
        await _recipeService.ToggleFavoriteAsync(recipeId);
        
        // Update the local collection
        var recipe = FilteredRecipes.FirstOrDefault(r => r.Id == recipeId);
        if (recipe != null)
        {
            recipe.IsFavorite = !recipe.IsFavorite;
            // Force UI update by replacing the item
            var index = FilteredRecipes.IndexOf(recipe);
            FilteredRecipes[index] = recipe;
        }
        
        // Also update in the cached list
        var cachedRecipe = _allRecipes.FirstOrDefault(r => r.Id == recipeId);
        if (cachedRecipe != null)
        {
            cachedRecipe.IsFavorite = recipe?.IsFavorite ?? false;
        }
    }
    
    /// <summary>
    /// Navigates to add new recipe page.
    /// </summary>
    [RelayCommand]
    private async Task AddRecipeAsync()
    {
        await _navigationService.NavigateToAsync(Routes.AddRecipe);
    }
}

/// <summary>
/// Represents a category filter option for the picker.
/// </summary>
public class CategoryOption
{
    public RecipeCategory? Value { get; set; }
    public string Label { get; set; } = string.Empty;
    
    public override string ToString() => Label;
}

/// <summary>
/// Represents a difficulty filter option for the picker.
/// </summary>
public class DifficultyOption
{
    public RecipeDifficulty? Value { get; set; }
    public string Label { get; set; } = string.Empty;
    
    public override string ToString() => Label;
}
