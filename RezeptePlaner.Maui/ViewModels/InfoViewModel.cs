using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RezeptePlaner.Maui.ViewModels;

/// <summary>
/// ViewModel for the Info/About page
/// </summary>
public partial class InfoViewModel : ObservableObject
{
    [ObservableProperty]
    private string appName = "CookMate";

    [ObservableProperty]
    private string appVersion = "1.0.0";

    [ObservableProperty]
    private string appDescription = 
        "CookMate ist Ihre persönliche Rezepteverwaltung für Windows Desktop. " +
        "Erstellen, organisieren und entdecken Sie Ihre Lieblingsrezepte mit Leichtigkeit.";

    [ObservableProperty]
    private string developerInfo = "Entwickelt mit ❤️ in Deutschland";

    [ObservableProperty]
    private string contactEmail = "support@cookmate.de";

    [ObservableProperty]
    private string copyright = $"© {DateTime.Now.Year} CookMate. Alle Rechte vorbehalten.";

    public List<FeatureItem> Features { get; } = new()
    {
        new FeatureItem 
        { 
            Icon = "🍳", 
            Title = "Rezepte verwalten", 
            Description = "Erstellen, bearbeiten und organisieren Sie Ihre Rezepte" 
        },
        new FeatureItem 
        { 
            Icon = "⭐", 
            Title = "Favoriten", 
            Description = "Markieren Sie Ihre Lieblingsrezepte für schnellen Zugriff" 
        },
        new FeatureItem 
        { 
            Icon = "📋", 
            Title = "Kategorien", 
            Description = "Sortieren Sie Rezepte nach Frühstück, Mittag, Abend oder Snack" 
        },
        new FeatureItem 
        { 
            Icon = "🔍", 
            Title = "Suche", 
            Description = "Finden Sie Rezepte nach Name, Zutaten oder Tags" 
        },
        new FeatureItem 
        { 
            Icon = "🥗", 
            Title = "Diätfilter", 
            Description = "Vegetarisch, Vegan und Glutenfrei Filter" 
        },
        new FeatureItem 
        { 
            Icon = "💻", 
            Title = "Desktop-optimiert", 
            Description = "Responsives Design für Windows Desktop" 
        }
    };

    [RelayCommand]
    private async Task OpenWebsite()
    {
        try
        {
            await Launcher.OpenAsync(new Uri("https://cookmate.de"));
        }
        catch
        {
            // URL konnte nicht geöffnet werden
        }
    }

    [RelayCommand]
    private async Task SendEmail()
    {
        try
        {
            var message = new EmailMessage
            {
                Subject = "CookMate Anfrage",
                To = new List<string> { ContactEmail }
            };
            await Email.ComposeAsync(message);
        }
        catch
        {
            await Shell.Current.DisplayAlert("Hinweis", 
                $"E-Mail konnte nicht geöffnet werden. Schreiben Sie an: {ContactEmail}", "OK");
        }
    }
}

/// <summary>
/// Feature item for the info page
/// </summary>
public class FeatureItem
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
