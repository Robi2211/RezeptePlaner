using CookMate.Maui.ViewModels;

namespace CookMate.Maui.Views;

/// <summary>
/// Recipe Library page code-behind.
/// </summary>
public partial class RecipeLibraryPage : ContentPage
{
    private readonly RecipeLibraryViewModel _viewModel;
    
    public RecipeLibraryPage(RecipeLibraryViewModel viewModel)
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
