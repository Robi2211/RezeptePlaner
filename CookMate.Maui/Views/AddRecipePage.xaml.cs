using CookMate.Maui.ViewModels;

namespace CookMate.Maui.Views;

/// <summary>
/// Add Recipe page code-behind.
/// </summary>
public partial class AddRecipePage : ContentPage
{
    public AddRecipePage(AddRecipeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
