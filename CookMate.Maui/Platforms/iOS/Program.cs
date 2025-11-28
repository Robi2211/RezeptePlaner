using UIKit;

namespace CookMate.Maui;

/// <summary>
/// iOS Program entry point.
/// Standard MAUI iOS startup.
/// </summary>
public class Program
{
    static void Main(string[] args)
    {
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
