using RezeptePlaner.Maui.ViewModels;

namespace RezeptePlaner.Maui.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel _viewModel;

    public FavoritesPage(FavoritesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
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
            FavoritesGridLayout.Span = 3;
        }
        else if (width > 800)
        {
            // Medium: 2 columns
            FavoritesGridLayout.Span = 2;
        }
        else
        {
            // Small/mobile: 1 column
            FavoritesGridLayout.Span = 1;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateGridColumns();
        _viewModel.LoadFavoritesCommand.Execute(null);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Unsubscribe from event to prevent memory leaks
        SizeChanged -= OnPageSizeChanged;
    }
}
