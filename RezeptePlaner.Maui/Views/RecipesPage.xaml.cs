using RezeptePlaner.Maui.ViewModels;

namespace RezeptePlaner.Maui.Views;

public partial class RecipesPage : ContentPage
{
    private bool _isSizeChangeSubscribed;

    public RecipesPage(RecipeListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
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
        
        // Subscribe to SizeChanged event only if not already subscribed
        if (!_isSizeChangeSubscribed)
        {
            SizeChanged += OnPageSizeChanged;
            _isSizeChangeSubscribed = true;
        }
        
        UpdateGridColumns();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        
        // Unsubscribe from SizeChanged event if subscribed
        if (_isSizeChangeSubscribed)
        {
            SizeChanged -= OnPageSizeChanged;
            _isSizeChangeSubscribed = false;
        }
    }
}
