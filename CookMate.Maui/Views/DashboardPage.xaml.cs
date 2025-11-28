using CookMate.Maui.ViewModels;

namespace CookMate.Maui.Views;

/// <summary>
/// Dashboard page code-behind.
/// Implements initialization and lifecycle events.
/// </summary>
public partial class DashboardPage : ContentPage
{
    private readonly DashboardViewModel _viewModel;
    
    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
    
    /// <summary>
    /// Called when the page appears - loads data.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        // Load dashboard data when page appears
        await _viewModel.LoadDataCommand.ExecuteAsync(null);
    }
}
