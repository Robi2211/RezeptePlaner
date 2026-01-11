using RezeptePlaner.Maui.ViewModels;

namespace RezeptePlaner.Maui.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel _viewModel;

    public FavoritesPage(FavoritesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadFavoritesCommand.Execute(null);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        
        // Cleanup ViewModel event subscriptions
        _viewModel.Cleanup();
    }
}
