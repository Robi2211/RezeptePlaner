using CookMate.Maui.ViewModels;

namespace CookMate.Maui.Views;

/// <summary>
/// Week Planner page code-behind.
/// </summary>
public partial class WeekPlannerPage : ContentPage
{
    private readonly WeekPlannerViewModel _viewModel;
    
    public WeekPlannerPage(WeekPlannerViewModel viewModel)
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
