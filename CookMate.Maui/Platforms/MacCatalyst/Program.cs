using UIKit;

namespace CookMate.Maui;

/// <summary>
/// MacCatalyst Program entry point.
/// Standard MAUI macOS startup via Catalyst.
/// </summary>
public class Program
{
    static void Main(string[] args)
    {
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
