using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using RezeptePlaner.Maui.Models;
using RezeptePlaner.Maui.Services;

namespace RezeptePlaner.Maui.ViewModels;

/// <summary>
/// ViewModel for the Help/FAQ page
/// </summary>
public partial class HelpViewModel : ObservableObject
{
    private readonly FaqService _faqService;

    [ObservableProperty]
    private ObservableCollection<FaqItem> faqItems = new();

    [ObservableProperty]
    private ObservableCollection<FaqItem> filteredFaqItems = new();

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private string? selectedCategory;

    public ObservableCollection<string> Categories { get; } = new()
    {
        "Alle",
        "Rezepte",
        "Favoriten",
        "Navigation",
        "Allgemein"
    };

    public HelpViewModel(FaqService faqService)
    {
        _faqService = faqService;
        LoadFaqItems();
    }

    /// <summary>
    /// Load FAQ items
    /// </summary>
    [RelayCommand]
    private void LoadFaqItems()
    {
        FaqItems = _faqService.GetFaqItems();
        FilterFaqItems();
    }

    /// <summary>
    /// Filter FAQ items
    /// </summary>
    [RelayCommand]
    private void FilterFaqItems()
    {
        var filtered = FaqItems.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var searchLower = SearchText.ToLowerInvariant();
            filtered = filtered.Where(f =>
                f.Question.ToLowerInvariant().Contains(searchLower) ||
                f.Answer.ToLowerInvariant().Contains(searchLower));
        }

        if (!string.IsNullOrWhiteSpace(SelectedCategory) && SelectedCategory != "Alle")
        {
            filtered = filtered.Where(f => f.Category == SelectedCategory);
        }

        FilteredFaqItems = new ObservableCollection<FaqItem>(filtered);
    }

    /// <summary>
    /// Toggle FAQ item expansion
    /// </summary>
    [RelayCommand]
    private void ToggleFaqItem(FaqItem item)
    {
        item.IsExpanded = !item.IsExpanded;
    }

    /// <summary>
    /// Navigate to contact/support
    /// </summary>
    [RelayCommand]
    private async Task ContactSupport()
    {
        try
        {
            var message = new EmailMessage
            {
                Subject = "CookMate Support-Anfrage",
                To = new List<string> { "support@cookmate.de" }
            };
            await Email.ComposeAsync(message);
        }
        catch
        {
            await Shell.Current.DisplayAlert("Kontakt",
                "E-Mail: support@cookmate.de\n\nSchreiben Sie uns bei Fragen!", "OK");
        }
    }

    partial void OnSearchTextChanged(string value) => FilterFaqItems();
    partial void OnSelectedCategoryChanged(string? value) => FilterFaqItems();
}
