namespace RezeptePlaner.Maui.Controls;

public partial class NavigationHeader : ContentView
{
    public static readonly BindableProperty CurrentPageProperty =
        BindableProperty.Create(nameof(CurrentPage), typeof(string), typeof(NavigationHeader), string.Empty, propertyChanged: OnCurrentPageChanged);

    public string CurrentPage
    {
        get => (string)GetValue(CurrentPageProperty);
        set => SetValue(CurrentPageProperty, value);
    }

    public NavigationHeader()
    {
        InitializeComponent();
    }

    private static void OnCurrentPageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NavigationHeader header && newValue is string currentPage)
        {
            header.UpdateActiveButton(currentPage);
        }
    }

    private void UpdateActiveButton(string currentPage)
    {
        // Reset all buttons to default color
        var defaultColor = (Color)Application.Current!.Resources["ForegroundMuted"];
        var activeColor = (Color)Application.Current!.Resources["Foreground"];

        DashboardButton.TextColor = defaultColor;
        RecipesButton.TextColor = defaultColor;
        WeekPlanButton.TextColor = defaultColor;
        FavoritesButton.TextColor = defaultColor;

        // Set active button color
        switch (currentPage)
        {
            case "Dashboard":
                DashboardButton.TextColor = activeColor;
                break;
            case "Rezepte":
                RecipesButton.TextColor = activeColor;
                break;
            case "Wochenplan":
                WeekPlanButton.TextColor = activeColor;
                break;
            case "Favoriten":
                FavoritesButton.TextColor = activeColor;
                break;
        }
    }

    private async void OnDashboardClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//DashboardPage");
    }

    private async void OnRecipesClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//RecipesPage");
    }

    private async void OnWeekPlanClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//WeekPlannerPage");
    }

    private async void OnFavoritesClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//FavoritesPage");
    }

    private async void OnNewRecipeClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AddRecipePage");
    }
}
