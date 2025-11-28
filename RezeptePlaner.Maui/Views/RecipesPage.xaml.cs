using RezeptePlaner.Maui.ViewModels;

namespace RezeptePlaner.Maui.Views;

public partial class RecipesPage : ContentPage
{
    public RecipesPage(RecipeListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
