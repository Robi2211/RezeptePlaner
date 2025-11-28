using RezeptePlaner.Maui.ViewModels;

namespace RezeptePlaner.Maui.Views;

public partial class SummaryPage : ContentPage
{
    public SummaryPage(SummaryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
