using CommunityToolkit.Mvvm.ComponentModel;

namespace RezeptePlaner.Maui.ViewModels;

/// <summary>
/// ViewModel for the Design/Theme settings page
/// </summary>
public partial class DesignViewModel : ObservableObject
{
    [ObservableProperty]
    private string pageTitle = "Design";
}
