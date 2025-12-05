using RezeptePlaner.Maui.ViewModels;

namespace RezeptePlaner.Maui.Views;

public partial class DesignPage : ContentPage
{
    public DesignPage(DesignViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
