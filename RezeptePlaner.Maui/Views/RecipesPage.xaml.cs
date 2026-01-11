using RezeptePlaner.Maui.ViewModels;

namespace RezeptePlaner.Maui.Views;

public partial class RecipesPage : ContentPage
{
    // Responsive breakpoints - consistent with RecipeFilterBar
    private const double SmallScreenMaxWidth = 600;
    private const double MediumScreenMaxWidth = 1200;
    
    private bool _isSizeChangeSubscribed;

    public RecipesPage(RecipeListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void OnPageSizeChanged(object? sender, EventArgs e)
    {
        UpdateGridColumns();
        UpdateVisualState();
    }

    private void UpdateGridColumns()
    {
        var width = Width;
        
        // Validate width to handle initial layout phases
        if (width <= 0 || double.IsNaN(width) || double.IsInfinity(width))
        {
            return;
        }
        
        // Adaptive item spacing based on screen size
        if (width < SmallScreenMaxWidth)
        {
            // Small/mobile: 1 column, reduced spacing
            GridLayout.Span = 1;
            GridLayout.HorizontalItemSpacing = 16;
            GridLayout.VerticalItemSpacing = 16;
        }
        else if (width < MediumScreenMaxWidth)
        {
            // Medium/tablet: 2 columns, standard spacing
            GridLayout.Span = 2;
            GridLayout.HorizontalItemSpacing = 20;
            GridLayout.VerticalItemSpacing = 20;
        }
        else
        {
            // Large/desktop: 3 columns, larger spacing
            GridLayout.Span = 3;
            GridLayout.HorizontalItemSpacing = 24;
            GridLayout.VerticalItemSpacing = 24;
        }
    }

    private void UpdateVisualState()
    {
        var width = Width;

        // Validate width to handle initial layout phases
        if (width <= 0 || double.IsNaN(width) || double.IsInfinity(width))
        {
            return;
        }

        string stateName;
        if (width < SmallScreenMaxWidth)
        {
            stateName = "Small";
        }
        else if (width < MediumScreenMaxWidth)
        {
            stateName = "Medium";
        }
        else
        {
            stateName = "Large";
        }

        VisualStateManager.GoToState(MainContentGrid, stateName);
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        UpdateGridColumns();
        UpdateVisualState();
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
        UpdateVisualState();
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
