using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookMate.Maui.Models;
using CookMate.Maui.Services;
using System.Collections.ObjectModel;

namespace CookMate.Maui.ViewModels;

/// <summary>
/// ViewModel for the Favorites page.
/// Displays and manages favorite recipes with search functionality.
/// </summary>
public partial class FavoritesViewModel : ObservableObject
{
    private readonly IRecipeService _recipeService;
    private readonly INavigationService _navigationService;
    
    private List<Recipe> _allFavorites = new();
    
    [ObservableProperty]
    private bool _isLoading;
    
    [ObservableProperty]
    private string _searchQuery = string.Empty;
    
    [ObservableProperty]
    private ObservableCollection<Recipe> _filteredFavorites = new();
    
    [ObservableProperty]
    private int _favoriteCount;
    
    [ObservableProperty]
    private bool _hasNoFavorites;
    
    [ObservableProperty]
    private bool _hasNoSearchResults;
    
    public FavoritesViewModel(
        IRecipeService recipeService,
        INavigationService navigationService)
    {
        _recipeService = recipeService;
        _navigationService = navigationService;
    }
    
    /// <summary>
    /// Triggered when search query changes.
    /// </summary>
    partial void OnSearchQueryChanged(string value)
    {
        ApplySearch();
    }
    
    /// <summary>
    /// Loads favorite recipes from the service.
    /// </summary>
    [RelayCommand]
    private async Task LoadDataAsync()
    {
        if (IsLoading) return;
        
        IsLoading = true;
        
        try
        {
            _allFavorites = (await _recipeService.GetFavoriteRecipesAsync()).ToList();
            FavoriteCount = _allFavorites.Count;
            HasNoFavorites = _allFavorites.Count == 0;
            ApplySearch();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading favorites: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    /// <summary>
    /// Applies search filter to favorites.
    /// </summary>
    private void ApplySearch()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            FilteredFavorites = new ObservableCollection<Recipe>(_allFavorites);
        }
        else
        {
            var lowerQuery = SearchQuery.ToLowerInvariant();
            var results = _allFavorites.Where(r =>
                r.Title.ToLowerInvariant().Contains(lowerQuery) ||
                r.Tags.Any(t => t.ToLowerInvariant().Contains(lowerQuery)));
            
            FilteredFavorites = new ObservableCollection<Recipe>(results);
        }
        
        HasNoSearchResults = !HasNoFavorites && FilteredFavorites.Count == 0;
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
    /// Removes a recipe from favorites.
    /// </summary>
    [RelayCommand]
    private async Task ToggleFavoriteAsync(string recipeId)
    {
        if (string.IsNullOrEmpty(recipeId)) return;
        
        await _recipeService.ToggleFavoriteAsync(recipeId);
        
        // Refresh the list
        await LoadDataAsync();
    }
    
    /// <summary>
    /// Navigates to recipes page to discover more recipes.
    /// </summary>
    [RelayCommand]
    private async Task DiscoverRecipesAsync()
    {
        await _navigationService.NavigateToAsync(Routes.Recipes);
    }
}
