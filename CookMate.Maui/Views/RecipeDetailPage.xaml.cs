using CookMate.Maui.ViewModels;

namespace CookMate.Maui.Views;

/// <summary>
/// Recipe Detail page code-behind.
/// </summary>
public partial class RecipeDetailPage : ContentPage
{
    public RecipeDetailPage(RecipeDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
