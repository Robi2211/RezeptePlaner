using RezeptePlaner.Maui.ViewModels;

namespace RezeptePlaner.Maui.Views;

public partial class HelpPage : ContentPage
{
    public HelpPage(HelpViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
