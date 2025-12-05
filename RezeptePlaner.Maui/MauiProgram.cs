using Microsoft.Extensions.Logging;
using RezeptePlaner.Maui.Services;
using RezeptePlaner.Maui.ViewModels;
using RezeptePlaner.Maui.Views;

namespace RezeptePlaner.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Register Services (Singleton for data persistence)
		builder.Services.AddSingleton<RecipeService>();
		builder.Services.AddSingleton<FaqService>();

		// Register ViewModels
		builder.Services.AddTransient<DashboardViewModel>();
		builder.Services.AddTransient<RecipeListViewModel>();
		builder.Services.AddTransient<RecipeDetailViewModel>();
		builder.Services.AddTransient<FavoritesViewModel>();
		builder.Services.AddTransient<WeekPlannerViewModel>();
		builder.Services.AddTransient<AddRecipeViewModel>();
		builder.Services.AddTransient<InfoViewModel>();
		builder.Services.AddTransient<HelpViewModel>();
		builder.Services.AddTransient<SummaryViewModel>();

		// Register Views
		builder.Services.AddTransient<DashboardPage>();
		builder.Services.AddTransient<RecipesPage>();
		builder.Services.AddTransient<RecipeDetailPage>();
		builder.Services.AddTransient<FavoritesPage>();
		builder.Services.AddTransient<WeekPlannerPage>();
		builder.Services.AddTransient<AddRecipePage>();
		builder.Services.AddTransient<InfoPage>();
		builder.Services.AddTransient<HelpPage>();
		builder.Services.AddTransient<SummaryPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
