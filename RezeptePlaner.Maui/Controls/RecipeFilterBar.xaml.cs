using System.Collections.ObjectModel;

namespace RezeptePlaner.Maui.Controls;

/// <summary>
/// Custom control for filtering recipes with search, category, difficulty, and time filters
/// </summary>
public partial class RecipeFilterBar : ContentView
{
    // Responsive breakpoints - consistent with RecipesPage
    private const double SmallScreenMaxWidth = 600;
    private const double MediumScreenMaxWidth = 1200;
    
    private bool _isSizeChangeSubscribed;

    #region Bindable Properties

    /// <summary>
    /// Search text for filtering recipes
    /// </summary>
    public static readonly BindableProperty SearchTextProperty =
        BindableProperty.Create(
            nameof(SearchText),
            typeof(string),
            typeof(RecipeFilterBar),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: OnSearchTextChanged);

    public string SearchText
    {
        get => (string)GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
    }

    /// <summary>
    /// Selected category for filtering
    /// </summary>
    public static readonly BindableProperty SelectedCategoryProperty =
        BindableProperty.Create(
            nameof(SelectedCategory),
            typeof(string),
            typeof(RecipeFilterBar),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnSelectedCategoryChanged);

    public string? SelectedCategory
    {
        get => (string?)GetValue(SelectedCategoryProperty);
        set => SetValue(SelectedCategoryProperty, value);
    }

    /// <summary>
    /// Selected difficulty level for filtering
    /// </summary>
    public static readonly BindableProperty SelectedDifficultyProperty =
        BindableProperty.Create(
            nameof(SelectedDifficulty),
            typeof(string),
            typeof(RecipeFilterBar),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnSelectedDifficultyChanged);

    public string? SelectedDifficulty
    {
        get => (string?)GetValue(SelectedDifficultyProperty);
        set => SetValue(SelectedDifficultyProperty, value);
    }

    /// <summary>
    /// Maximum cooking time in minutes for filtering
    /// </summary>
    public static readonly BindableProperty MaxTimeMinutesProperty =
        BindableProperty.Create(
            nameof(MaxTimeMinutes),
            typeof(int),
            typeof(RecipeFilterBar),
            120,
            BindingMode.TwoWay,
            propertyChanged: OnMaxTimeMinutesChanged);

    public int MaxTimeMinutes
    {
        get => (int)GetValue(MaxTimeMinutesProperty);
        set => SetValue(MaxTimeMinutesProperty, value);
    }

    /// <summary>
    /// Collection of available categories
    /// </summary>
    public static readonly BindableProperty CategoriesProperty =
        BindableProperty.Create(
            nameof(Categories),
            typeof(ObservableCollection<string>),
            typeof(RecipeFilterBar),
            new ObservableCollection<string>());

    public ObservableCollection<string> Categories
    {
        get => (ObservableCollection<string>)GetValue(CategoriesProperty);
        set => SetValue(CategoriesProperty, value);
    }

    /// <summary>
    /// Collection of available difficulty levels
    /// </summary>
    public static readonly BindableProperty DifficultiesProperty =
        BindableProperty.Create(
            nameof(Difficulties),
            typeof(ObservableCollection<string>),
            typeof(RecipeFilterBar),
            new ObservableCollection<string>());

    public ObservableCollection<string> Difficulties
    {
        get => (ObservableCollection<string>)GetValue(DifficultiesProperty);
        set => SetValue(DifficultiesProperty, value);
    }

    /// <summary>
    /// Number of recipes found after filtering
    /// </summary>
    public static readonly BindableProperty ResultCountProperty =
        BindableProperty.Create(
            nameof(ResultCount),
            typeof(int),
            typeof(RecipeFilterBar),
            0);

    public int ResultCount
    {
        get => (int)GetValue(ResultCountProperty);
        set => SetValue(ResultCountProperty, value);
    }

    #endregion

    #region Events

    /// <summary>
    /// Event raised when any filter value changes
    /// </summary>
    public event EventHandler? FilterChanged;

    #endregion

    public RecipeFilterBar()
    {
        InitializeComponent();
    }

    private void OnControlSizeChanged(object? sender, EventArgs e)
    {
        UpdateVisualState();
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

        VisualStateManager.GoToState(FilterGrid, stateName);
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        UpdateVisualState();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        if (Handler != null && !_isSizeChangeSubscribed)
        {
            // Subscribe to SizeChanged when handler is attached
            SizeChanged += OnControlSizeChanged;
            _isSizeChangeSubscribed = true;
        }
        else if (Handler == null && _isSizeChangeSubscribed)
        {
            // Unsubscribe when handler is detached to prevent memory leaks
            SizeChanged -= OnControlSizeChanged;
            _isSizeChangeSubscribed = false;
        }
    }

    #region Property Changed Handlers

    private static void OnSearchTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RecipeFilterBar control)
        {
            control.FilterChanged?.Invoke(control, EventArgs.Empty);
        }
    }

    private static void OnSelectedCategoryChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RecipeFilterBar control)
        {
            control.FilterChanged?.Invoke(control, EventArgs.Empty);
        }
    }

    private static void OnSelectedDifficultyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RecipeFilterBar control)
        {
            control.FilterChanged?.Invoke(control, EventArgs.Empty);
        }
    }

    private static void OnMaxTimeMinutesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RecipeFilterBar control)
        {
            control.FilterChanged?.Invoke(control, EventArgs.Empty);
        }
    }

    #endregion
}
