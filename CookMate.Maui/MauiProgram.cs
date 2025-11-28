using CommunityToolkit.Maui;
using CookMate.Maui.Converters;
using CookMate.Maui.Services;
using CookMate.Maui.ViewModels;
using CookMate.Maui.Views;
using Microsoft.Extensions.Logging;

namespace CookMate.Maui;

/// <summary>
/// MauiProgram configures the application's dependency injection,
/// services, and startup behavior.
/// 
/// This follows MAUI best practices with:
/// - Service registration for dependency injection
/// - ViewModel/View registration for navigation
/// - CommunityToolkit.Maui for enhanced controls
/// </summary>
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit() // Enable CommunityToolkit.Maui
            .ConfigureFonts(fonts =>
            {
                // Register custom fonts
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register Services (Singleton - shared across the app)
        builder.Services.AddSingleton<IRecipeService, RecipeService>();
        builder.Services.AddSingleton<IWeekPlannerService, WeekPlannerService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();

        // Register ViewModels (Transient - new instance each time)
        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<RecipeLibraryViewModel>();
        builder.Services.AddTransient<RecipeDetailViewModel>();
        builder.Services.AddTransient<WeekPlannerViewModel>();
        builder.Services.AddTransient<FavoritesViewModel>();
        builder.Services.AddTransient<AddRecipeViewModel>();

        // Register Pages (Transient - for proper navigation)
        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<RecipeLibraryPage>();
        builder.Services.AddTransient<RecipeDetailPage>();
        builder.Services.AddTransient<WeekPlannerPage>();
        builder.Services.AddTransient<FavoritesPage>();
        builder.Services.AddTransient<AddRecipePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
