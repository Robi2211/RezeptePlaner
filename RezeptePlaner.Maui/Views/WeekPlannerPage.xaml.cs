using RezeptePlaner.Maui.ViewModels;

namespace RezeptePlaner.Maui.Views;

public partial class WeekPlannerPage : ContentPage
{
    public WeekPlannerPage(WeekPlannerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
