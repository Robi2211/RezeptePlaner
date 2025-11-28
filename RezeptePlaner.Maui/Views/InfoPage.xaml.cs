using RezeptePlaner.Maui.ViewModels;

namespace RezeptePlaner.Maui.Views;

public partial class InfoPage : ContentPage
{
    public InfoPage(InfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
