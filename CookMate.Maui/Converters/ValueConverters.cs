using CookMate.Maui.Models;
using System.Globalization;

namespace CookMate.Maui.Converters;

/// <summary>
/// Converts RecipeCategory enum to German display string.
/// </summary>
public class CategoryToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is RecipeCategory category)
        {
            return category switch
            {
                RecipeCategory.Breakfast => "Frühstück",
                RecipeCategory.Lunch => "Mittagessen",
                RecipeCategory.Dinner => "Abendessen",
                RecipeCategory.Snack => "Snack",
                _ => category.ToString()
            };
        }
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts RecipeDifficulty enum to German display string.
/// </summary>
public class DifficultyToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is RecipeDifficulty difficulty)
        {
            return difficulty switch
            {
                RecipeDifficulty.Easy => "Einfach",
                RecipeDifficulty.Medium => "Mittel",
                RecipeDifficulty.Hard => "Schwer",
                _ => difficulty.ToString()
            };
        }
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts MealType enum to German display string.
/// </summary>
public class MealTypeToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is MealType mealType)
        {
            return mealType switch
            {
                MealType.Breakfast => "Frühstück",
                MealType.Lunch => "Mittagessen",
                MealType.Dinner => "Abendessen",
                _ => mealType.ToString()
            };
        }
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts boolean favorite status to icon character.
/// Uses filled heart for favorites, outline for non-favorites.
/// </summary>
public class FavoriteToIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isFavorite)
        {
            // Using FontAwesome or Material icons - the filled vs outline heart
            return isFavorite ? "\uf004" : "\uf08a"; // FontAwesome heart icons
        }
        return "\uf08a";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts boolean favorite status to color.
/// Red for favorites, gray for non-favorites.
/// </summary>
public class FavoriteToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isFavorite)
        {
            return isFavorite ? Colors.Red : Colors.Gray;
        }
        return Colors.Gray;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts integer index to step number display (1, 2, 3...).
/// </summary>
public class IndexToStepNumberConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int index)
        {
            return (index + 1).ToString();
        }
        return "1";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Inverts a boolean value for visibility binding.
/// </summary>
public class InverseBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }
        return true;
    }
}

/// <summary>
/// Converts count to visibility (visible if count > 0).
/// </summary>
public class CountToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int count)
        {
            return count > 0;
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
