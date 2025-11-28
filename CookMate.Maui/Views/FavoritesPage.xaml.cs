using CookMate.Maui.ViewModels;

namespace CookMate.Maui.Views;

/// <summary>
/// Favorites page code-behind.
/// </summary>
public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel _viewModel;
    
    public FavoritesPage(FavoritesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataCommand.ExecuteAsync(null);
    }
}
