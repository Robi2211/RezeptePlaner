using System.Collections.ObjectModel;
using RezeptePlaner.Maui.Models;

namespace RezeptePlaner.Maui.Services;

/// <summary>
/// Service for managing recipes
/// </summary>
public class RecipeService
{
    private ObservableCollection<Recipe> _recipes;

    public RecipeService()
    {
        _recipes = new ObservableCollection<Recipe>(GetSampleRecipes());
    }

    /// <summary>
    /// Get all recipes
    /// </summary>
    public ObservableCollection<Recipe> GetRecipes() => _recipes;

    /// <summary>
    /// Add a new recipe
    /// </summary>
    public void AddRecipe(Recipe recipe)
    {
        recipe.Id = Guid.NewGuid().ToString();
        recipe.CreatedAt = DateTime.Now;
        _recipes.Insert(0, recipe);
    }

    /// <summary>
    /// Toggle favorite status
    /// </summary>
    public void ToggleFavorite(string recipeId)
    {
        var recipe = _recipes.FirstOrDefault(r => r.Id == recipeId);
        if (recipe != null)
        {
            recipe.IsFavorite = !recipe.IsFavorite;
        }
    }

    /// <summary>
    /// Get recipe by ID
    /// </summary>
    public Recipe? GetRecipeById(string id) => _recipes.FirstOrDefault(r => r.Id == id);

    /// <summary>
    /// Get favorite recipes
    /// </summary>
    public ObservableCollection<Recipe> GetFavorites() => 
        new ObservableCollection<Recipe>(_recipes.Where(r => r.IsFavorite));

    /// <summary>
    /// Get sample recipes
    /// </summary>
    private static List<Recipe> GetSampleRecipes()
    {
        return new List<Recipe>
        {
            new Recipe
            {
                Id = "1",
                Title = "Avocado-Toast mit pochiertem Ei",
                Category = RecipeCategory.Frühstück,
                PrepTime = 5,
                CookTime = 10,
                Servings = 2,
                Difficulty = DifficultyLevel.Einfach,
                Ingredients = new ObservableCollection<string>
                {
                    "2 Scheiben Vollkornbrot",
                    "1 reife Avocado",
                    "2 Eier",
                    "Salz und Pfeffer",
                    "Chiliflocken",
                    "Zitronensaft"
                },
                Instructions = new ObservableCollection<string>
                {
                    "Brot toasten bis es goldbraun ist",
                    "Avocado zerdrücken und mit Zitronensaft, Salz und Pfeffer würzen",
                    "Wasser zum Kochen bringen und Eier pochieren (ca. 3-4 Minuten)",
                    "Avocado auf Toast verteilen",
                    "Pochiertes Ei darauf legen und mit Chiliflocken garnieren"
                },
                ImageUrl = "avocado_toast.svg",
                IsFavorite = true,
                IsVegetarian = true,
                Tags = new ObservableCollection<string> { "gesund", "schnell", "vegetarisch" }
            },
            new Recipe
            {
                Id = "2",
                Title = "Spaghetti Carbonara",
                Category = RecipeCategory.Abendessen,
                PrepTime = 10,
                CookTime = 20,
                Servings = 4,
                Difficulty = DifficultyLevel.Mittel,
                Ingredients = new ObservableCollection<string>
                {
                    "400g Spaghetti",
                    "200g Guanciale oder Pancetta",
                    "4 Eigelb",
                    "100g Pecorino Romano",
                    "Schwarzer Pfeffer",
                    "Salz"
                },
                Instructions = new ObservableCollection<string>
                {
                    "Spaghetti in reichlich Salzwasser al dente kochen",
                    "Guanciale in kleine Würfel schneiden und knusprig braten",
                    "Eigelb mit geriebenem Pecorino und viel schwarzem Pfeffer vermischen",
                    "Pasta abgießen, etwas Nudelwasser auffangen",
                    "Heiße Pasta mit Guanciale mischen, dann Ei-Käse-Mischung unterrühren",
                    "Mit Nudelwasser die gewünschte Konsistenz erreichen"
                },
                ImageUrl = "carbonara.svg",
                IsFavorite = true,
                Tags = new ObservableCollection<string> { "italienisch", "klassiker", "pasta" }
            },
            new Recipe
            {
                Id = "3",
                Title = "Caesar Salad",
                Category = RecipeCategory.Mittagessen,
                PrepTime = 15,
                CookTime = 10,
                Servings = 2,
                Difficulty = DifficultyLevel.Einfach,
                Ingredients = new ObservableCollection<string>
                {
                    "2 Römersalat-Herzen",
                    "2 Hähnchenbrüste",
                    "100g Parmesan",
                    "Croutons",
                    "3 EL Mayonnaise",
                    "1 EL Dijon-Senf",
                    "2 Knoblauchzehen",
                    "Zitronensaft",
                    "Worcestershire-Sauce"
                },
                Instructions = new ObservableCollection<string>
                {
                    "Hähnchenbrust würzen und in der Pfanne braten (ca. 6-8 Min pro Seite)",
                    "Salat waschen und in mundgerechte Stücke schneiden",
                    "Für das Dressing: Mayo, Senf, gehackten Knoblauch, Zitronensaft und Worcestershire-Sauce vermischen",
                    "Salat mit Dressing vermengen",
                    "Hähnchen in Streifen schneiden",
                    "Salat mit Hähnchen, Croutons und gehobeltem Parmesan servieren"
                },
                ImageUrl = "caesar_salad.svg",
                IsFavorite = false,
                Tags = new ObservableCollection<string> { "salat", "proteinreich", "amerikanisch" }
            },
            new Recipe
            {
                Id = "4",
                Title = "Overnight Oats mit Beeren",
                Category = RecipeCategory.Frühstück,
                PrepTime = 5,
                CookTime = 0,
                Servings = 1,
                Difficulty = DifficultyLevel.Einfach,
                Ingredients = new ObservableCollection<string>
                {
                    "50g Haferflocken",
                    "150ml Milch oder Pflanzendrink",
                    "1 EL Chiasamen",
                    "1 TL Honig",
                    "Gemischte Beeren",
                    "Nüsse nach Wahl"
                },
                Instructions = new ObservableCollection<string>
                {
                    "Haferflocken, Milch, Chiasamen und Honig in einem Glas vermischen",
                    "Über Nacht (mindestens 4 Stunden) im Kühlschrank ziehen lassen",
                    "Am nächsten Morgen mit frischen Beeren und Nüssen toppen",
                    "Optional: zusätzlich mit Honig oder Ahornsirup süßen"
                },
                ImageUrl = "overnight_oats.svg",
                IsFavorite = true,
                IsVegetarian = true,
                Tags = new ObservableCollection<string> { "gesund", "vorbereitung", "vegetarisch" }
            },
            new Recipe
            {
                Id = "5",
                Title = "Thailändisches Curry",
                Category = RecipeCategory.Abendessen,
                PrepTime = 15,
                CookTime = 25,
                Servings = 4,
                Difficulty = DifficultyLevel.Mittel,
                Ingredients = new ObservableCollection<string>
                {
                    "400ml Kokosmilch",
                    "2-3 EL rote Currypaste",
                    "500g Hähnchenfleisch",
                    "1 Paprika",
                    "200g Zucchini",
                    "1 Zwiebel",
                    "Basilikum",
                    "Fischsauce",
                    "Limettensaft",
                    "Jasminreis"
                },
                Instructions = new ObservableCollection<string>
                {
                    "Reis nach Packungsanweisung kochen",
                    "Hähnchen in Würfel schneiden, Gemüse in Streifen schneiden",
                    "Currypaste in etwas Kokosmilch anbraten",
                    "Hähnchen hinzufügen und anbraten",
                    "Restliche Kokosmilch, Gemüse hinzufügen und 15 Min köcheln lassen",
                    "Mit Fischsauce und Limettensaft abschmecken",
                    "Mit Basilikum garnieren und mit Reis servieren"
                },
                ImageUrl = "thai_curry.svg",
                IsFavorite = false,
                IsGlutenFree = true,
                Tags = new ObservableCollection<string> { "asiatisch", "würzig", "curry" }
            },
            new Recipe
            {
                Id = "6",
                Title = "Griechischer Joghurt Bowl",
                Category = RecipeCategory.Frühstück,
                PrepTime = 5,
                CookTime = 0,
                Servings = 1,
                Difficulty = DifficultyLevel.Einfach,
                Ingredients = new ObservableCollection<string>
                {
                    "200g griechischer Joghurt",
                    "2 EL Honig",
                    "50g Granola",
                    "Frische Früchte (Beeren, Banane)",
                    "1 EL Mandeln",
                    "Minze zur Deko"
                },
                Instructions = new ObservableCollection<string>
                {
                    "Griechischen Joghurt in eine Bowl geben",
                    "Mit Honig beträufeln",
                    "Granola, frische Früchte und Mandeln darauf verteilen",
                    "Mit Minzblättern dekorieren"
                },
                ImageUrl = "yogurt_bowl.svg",
                IsFavorite = false,
                IsVegetarian = true,
                Tags = new ObservableCollection<string> { "gesund", "schnell", "vegetarisch" }
            }
        };
    }
}
