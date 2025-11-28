# CookMate - .NET MAUI Recipe Planner

A cross-platform recipe management and meal planning application built with .NET MAUI. This is a complete rewrite of the original React/TypeScript CookMate UI Design, preserving the same UI layout and functionality while modernizing the implementation with MAUI best practices.

## 🍳 Features

- **Dashboard** - Overview of today's meals, weekly summary, trending recipes, and quick stats
- **Recipe Library** - Browse, search, and filter recipes by category, difficulty, and cooking time
- **Recipe Details** - View full recipe information including ingredients and step-by-step instructions
- **Week Planner** - Plan meals for the entire week with drag-and-drop support
- **Favorites** - Save and manage your favorite recipes
- **Add Recipe** - Create new recipes with ingredients, instructions, and tags

## 🏗️ Architecture

### MVVM Pattern
The application follows the Model-View-ViewModel pattern for clean separation of concerns:

- **Models** - Data classes for Recipe, PlannedMeal, DayPlan
- **ViewModels** - Business logic with ObservableObject and RelayCommand from CommunityToolkit.Mvvm
- **Views** - XAML pages with proper data binding
- **Services** - Repository pattern for data access

### Dependency Injection
All services and ViewModels are registered in `MauiProgram.cs` using Microsoft.Extensions.DependencyInjection:

```csharp
// Services (Singleton)
builder.Services.AddSingleton<IRecipeService, RecipeService>();
builder.Services.AddSingleton<IWeekPlannerService, WeekPlannerService>();
builder.Services.AddSingleton<INavigationService, NavigationService>();

// ViewModels (Transient)
builder.Services.AddTransient<DashboardViewModel>();
// ...
```

### Navigation
Shell-based navigation with TabBar for main screens and push navigation for detail pages:

- Dashboard (Tab)
- Recipes (Tab)
- Week Planner (Tab)
- Favorites (Tab)
- Recipe Detail (Push)
- Add Recipe (Push)

## 📁 Project Structure

```
CookMate.Maui/
├── Models/
│   ├── Recipe.cs              # Recipe data model
│   └── PlannedMeal.cs         # Meal planning models
├── ViewModels/
│   ├── DashboardViewModel.cs
│   ├── RecipeLibraryViewModel.cs
│   ├── RecipeDetailViewModel.cs
│   ├── WeekPlannerViewModel.cs
│   ├── FavoritesViewModel.cs
│   └── AddRecipeViewModel.cs
├── Views/
│   ├── DashboardPage.xaml(.cs)
│   ├── RecipeLibraryPage.xaml(.cs)
│   ├── RecipeDetailPage.xaml(.cs)
│   ├── WeekPlannerPage.xaml(.cs)
│   ├── FavoritesPage.xaml(.cs)
│   └── AddRecipePage.xaml(.cs)
├── Services/
│   ├── RecipeService.cs       # Recipe data management
│   ├── WeekPlannerService.cs  # Weekly meal planning
│   └── NavigationService.cs   # Type-safe navigation
├── Converters/
│   ├── ValueConverters.cs     # Category, Difficulty, etc.
│   └── TodayConverters.cs     # Week planner highlighting
├── Resources/
│   ├── Styles/
│   │   ├── Colors.xaml        # Color palette
│   │   └── Styles.xaml        # Global styles
│   ├── AppIcon/
│   ├── Splash/
│   └── Fonts/
├── Platforms/
│   ├── Android/
│   ├── iOS/
│   ├── MacCatalyst/
│   └── Windows/
├── App.xaml(.cs)              # Application entry
├── AppShell.xaml(.cs)         # Navigation structure
└── MauiProgram.cs             # DI and startup config
```

## 🎨 Design System

### Colors
The color palette is based on Tailwind CSS colors from the original React implementation:

- **Primary**: Orange (#F97316) - Brand color for buttons and accents
- **Secondary**: Teal (#14B8A6) - Accent for links and badges
- **Success**: Green (#22C55E) - Positive states
- **Danger**: Red (#EF4444) - Errors and favorites
- **Neutrals**: Gray scale from #F9FAFB to #111827

### Typography
- **Title1**: 24px, Bold - Page titles
- **Title2**: 18px, Bold - Section headers
- **Subtitle**: 16px, Bold - Card titles
- **Body**: 14px, Regular - Content text
- **Caption**: 12px, Regular - Small text

### Animations & Polish
- Smooth transitions on button press
- Hover effects on cards (desktop)
- Loading indicators with ActivityIndicator
- Shadow hierarchy for depth

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022 or VS Code with .NET MAUI extension
- For specific platforms:
  - **Windows**: Windows 10 SDK
  - **macOS**: Xcode 15+
  - **Android**: Android SDK 21+
  - **iOS**: macOS with Xcode

### Build and Run

```bash
# Restore dependencies
dotnet restore

# Build for specific platform
dotnet build -f net9.0-android
dotnet build -f net9.0-ios
dotnet build -f net9.0-maccatalyst
dotnet build -f net9.0-windows10.0.19041.0

# Run on Android emulator
dotnet run -f net9.0-android

# Run on iOS simulator
dotnet run -f net9.0-ios

# Run on Windows
dotnet run -f net9.0-windows10.0.19041.0
```

## 📦 Dependencies

- **CommunityToolkit.Mvvm** (8.4.0) - MVVM helpers, ObservableObject, RelayCommand
- **CommunityToolkit.Maui** (9.1.1) - Enhanced MAUI controls and animations
- **Newtonsoft.Json** (13.0.3) - JSON serialization for data persistence

## 🔄 Migration from React

### Key Changes
| React/TypeScript | .NET MAUI |
|-----------------|-----------|
| useState/useReducer | ObservableProperty |
| onClick events | Command binding |
| Tailwind CSS | ResourceDictionary styles |
| React Router | Shell navigation |
| Context API | Dependency Injection |
| map() for lists | CollectionView + DataTemplate |

### Preserved Features
- ✅ All recipe data (German language)
- ✅ Category/difficulty filtering
- ✅ Time-based search
- ✅ Favorites management
- ✅ Weekly meal planning
- ✅ Responsive layouts
- ✅ Premium feel with gradients and shadows

## 📱 Platform Support

| Platform | Status | Min Version |
|----------|--------|-------------|
| Android | ✅ | API 21 (Lollipop) |
| iOS | ✅ | iOS 15.0 |
| macOS | ✅ | macOS 15.0 (Catalyst) |
| Windows | ✅ | Windows 10 (17763) |

## 📄 License

This project is part of the CookMate UI Design bundle.

---

**CookMate** - Your personal recipe planner 🍳
