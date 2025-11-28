using CommunityToolkit.Mvvm.ComponentModel;

namespace RezeptePlaner.Maui.Models;

/// <summary>
/// FAQ item model for the help section
/// </summary>
public partial class FaqItem : ObservableObject
{
    [ObservableProperty]
    private string question = string.Empty;

    [ObservableProperty]
    private string answer = string.Empty;

    [ObservableProperty]
    private bool isExpanded;

    [ObservableProperty]
    private string category = string.Empty;
}
