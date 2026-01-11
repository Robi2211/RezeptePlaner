using RezeptePlaner.Maui.ViewModels;

namespace RezeptePlaner.Maui.Views;

public partial class RecipesPage : ContentPage
{
    public RecipesPage(RecipeListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        
        // Set up responsive layout based on window width
        SizeChanged += OnPageSizeChanged;
    }

    private void OnPageSizeChanged(object? sender, EventArgs e)
    {
        UpdateGridColumns();
    }

    private void UpdateGridColumns()
    {
        var width = Width;
        
        // Validate width to handle initial layout phases
        if (width <= 0 || double.IsNaN(width) || double.IsInfinity(width))
        {
            return;
        }
        
        if (width > 1200)
        {
            // Large/desktop: 3 columns
            GridLayout.Span = 3;
        }
        else if (width > 800)
        {
            // Medium: 2 columns
            GridLayout.Span = 2;
        }
        else
        {
            // Small/mobile: 1 column
            GridLayout.Span = 1;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateGridColumns();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Unsubscribe from event to prevent memory leaks
        SizeChanged -= OnPageSizeChanged;
    }
}
