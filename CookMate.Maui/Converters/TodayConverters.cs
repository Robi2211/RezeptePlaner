using System.Globalization;

namespace CookMate.Maui.Converters;

/// <summary>
/// Converts IsToday boolean to border color for week planner.
/// Today gets amber/orange border, other days get gray.
/// </summary>
public class TodayToBorderColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isToday && isToday)
        {
            return Color.FromArgb("#FBBF24"); // Amber-400
        }
        return Color.FromArgb("#E5E7EB"); // Gray-200
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts IsToday boolean to background color for day header.
/// </summary>
public class TodayToBackgroundColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isToday && isToday)
        {
            return Color.FromArgb("#FFFBEB"); // Amber-50
        }
        return Color.FromArgb("#F9FAFB"); // Gray-50
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts IsToday boolean to text color.
/// </summary>
public class TodayToTextColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isToday && isToday)
        {
            return Color.FromArgb("#78350F"); // Amber-900
        }
        return Color.FromArgb("#6B7280"); // Gray-500
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts IsToday boolean to shadow for emphasis.
/// </summary>
public class TodayToShadowConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isToday && isToday)
        {
            return new Shadow
            {
                Brush = new SolidColorBrush(Color.FromArgb("#0000001A")),
                Offset = new Point(0, 10),
                Radius = 15,
                Opacity = 0.25f
            };
        }
        return new Shadow
        {
            Brush = new SolidColorBrush(Color.FromArgb("#0000001A")),
            Offset = new Point(0, 1),
            Radius = 3,
            Opacity = 0.1f
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
