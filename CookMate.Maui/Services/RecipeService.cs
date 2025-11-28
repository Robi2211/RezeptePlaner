using CookMate.Maui.Models;

namespace CookMate.Maui.Services;

/// <summary>
/// Service for managing recipes with async operations.
/// Implements repository pattern for better testability and maintainability.
/// </summary>
public interface IRecipeService
{
    /// <summary>
    /// Gets all recipes asynchronously.
    /// </summary>
    Task<IEnumerable<Recipe>> GetRecipesAsync();
    
    /// <summary>
    /// Gets a recipe by its ID.
    /// </summary>
    Task<Recipe?> GetRecipeByIdAsync(string id);
    
    /// <summary>
    /// Adds a new recipe.
    /// </summary>
    Task AddRecipeAsync(Recipe recipe);
    
    /// <summary>
    /// Updates an existing recipe.
    /// </summary>
    Task UpdateRecipeAsync(Recipe recipe);
    
    /// <summary>
    /// Deletes a recipe by ID.
    /// </summary>
    Task DeleteRecipeAsync(string id);
    
    /// <summary>
    /// Toggles the favorite status of a recipe.
    /// </summary>
    Task ToggleFavoriteAsync(string id);
    
    /// <summary>
    /// Gets all favorite recipes.
    /// </summary>
    Task<IEnumerable<Recipe>> GetFavoriteRecipesAsync();
    
    /// <summary>
    /// Searches recipes by query string (title, tags, ingredients).
    /// </summary>
    Task<IEnumerable<Recipe>> SearchRecipesAsync(string query);
    
    /// <summary>
    /// Filters recipes by category.
    /// </summary>
    Task<IEnumerable<Recipe>> FilterByCategoryAsync(RecipeCategory? category);
}

/// <summary>
/// In-memory implementation of the recipe service.
/// In production, this could be replaced with a database or API implementation.
/// </summary>
public class RecipeService : IRecipeService
{
    private readonly List<Recipe> _recipes;
    
    public RecipeService()
    {
        // Initialize with sample data matching the original React app
        _recipes = GetSampleRecipes();
    }
    
    public Task<IEnumerable<Recipe>> GetRecipesAsync()
    {
        return Task.FromResult<IEnumerable<Recipe>>(_recipes);
    }
    
    public Task<Recipe?> GetRecipeByIdAsync(string id)
    {
        var recipe = _recipes.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(recipe);
    }
    
    public Task AddRecipeAsync(Recipe recipe)
    {
        // Generate unique ID if not provided
        if (string.IsNullOrEmpty(recipe.Id))
        {
            recipe.Id = DateTime.Now.Ticks.ToString();
        }
        
        _recipes.Insert(0, recipe);
        return Task.CompletedTask;
    }
    
    public Task UpdateRecipeAsync(Recipe recipe)
    {
        var index = _recipes.FindIndex(r => r.Id == recipe.Id);
        if (index >= 0)
        {
            _recipes[index] = recipe;
        }
        return Task.CompletedTask;
    }
    
    public Task DeleteRecipeAsync(string id)
    {
        _recipes.RemoveAll(r => r.Id == id);
        return Task.CompletedTask;
    }
    
    public Task ToggleFavoriteAsync(string id)
    {
        var recipe = _recipes.FirstOrDefault(r => r.Id == id);
        if (recipe != null)
        {
            recipe.IsFavorite = !recipe.IsFavorite;
        }
        return Task.CompletedTask;
    }
    
    public Task<IEnumerable<Recipe>> GetFavoriteRecipesAsync()
    {
        return Task.FromResult<IEnumerable<Recipe>>(_recipes.Where(r => r.IsFavorite));
    }
    
    public Task<IEnumerable<Recipe>> SearchRecipesAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return GetRecipesAsync();
        }
        
        var lowerQuery = query.ToLowerInvariant();
        var results = _recipes.Where(r =>
            r.Title.ToLowerInvariant().Contains(lowerQuery) ||
            r.Tags.Any(t => t.ToLowerInvariant().Contains(lowerQuery)) ||
            r.Ingredients.Any(i => i.ToLowerInvariant().Contains(lowerQuery)));
        
        return Task.FromResult(results);
    }
    
    public Task<IEnumerable<Recipe>> FilterByCategoryAsync(RecipeCategory? category)
    {
        if (category == null)
        {
            return GetRecipesAsync();
        }
        
        return Task.FromResult<IEnumerable<Recipe>>(_recipes.Where(r => r.Category == category));
    }
    
    /// <summary>
    /// Returns the sample recipes matching the original React app data.
    /// These are German recipes as specified in the original implementation.
    /// </summary>
    private static List<Recipe> GetSampleRecipes()
    {
        return new List<Recipe>
        {
            new Recipe
            {
                Id = "1",
                Title = "Avocado-Toast mit pochiertem Ei",
                Category = RecipeCategory.Breakfast,
                PrepTime = 5,
                CookTime = 10,
                Servings = 2,
                Difficulty = RecipeDifficulty.Easy,
                Ingredients = new List<string>
                {
                    "2 Scheiben Vollkornbrot",
                    "1 reife Avocado",
                    "2 Eier",
                    "Salz und Pfeffer",
                    "Chiliflocken",
                    "Zitronensaft"
                },
                Instructions = new List<string>
                {
                    "Brot toasten bis es goldbraun ist",
                    "Avocado zerdrücken und mit Zitronensaft, Salz und Pfeffer würzen",
                    "Wasser zum Kochen bringen und Eier pochieren (ca. 3-4 Minuten)",
                    "Avocado auf Toast verteilen",
                    "Pochiertes Ei darauf legen und mit Chiliflocken garnieren"
                },
                Image = "https://images.unsplash.com/photo-1541519227354-08fa5d50c44d?w=800",
                IsFavorite = true,
                Tags = new List<string> { "gesund", "schnell", "vegetarisch" }
            },
            new Recipe
            {
                Id = "2",
                Title = "Spaghetti Carbonara",
                Category = RecipeCategory.Dinner,
                PrepTime = 10,
                CookTime = 20,
                Servings = 4,
                Difficulty = RecipeDifficulty.Medium,
                Ingredients = new List<string>
                {
                    "400g Spaghetti",
                    "200g Guanciale oder Pancetta",
                    "4 Eigelb",
                    "100g Pecorino Romano",
                    "Schwarzer Pfeffer",
                    "Salz"
                },
                Instructions = new List<string>
                {
                    "Spaghetti in reichlich Salzwasser al dente kochen",
                    "Guanciale in kleine Würfel schneiden und knusprig braten",
                    "Eigelb mit geriebenem Pecorino und viel schwarzem Pfeffer vermischen",
                    "Pasta abgießen, etwas Nudelwasser auffangen",
                    "Heiße Pasta mit Guanciale mischen, dann Ei-Käse-Mischung unterrühren",
                    "Mit Nudelwasser die gewünschte Konsistenz erreichen"
                },
                Image = "https://images.unsplash.com/photo-1612874742237-6526221588e3?w=800",
                IsFavorite = true,
                Tags = new List<string> { "italienisch", "klassiker", "pasta" }
            },
            new Recipe
            {
                Id = "3",
                Title = "Caesar Salad",
                Category = RecipeCategory.Lunch,
                PrepTime = 15,
                CookTime = 10,
                Servings = 2,
                Difficulty = RecipeDifficulty.Easy,
                Ingredients = new List<string>
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
                Instructions = new List<string>
                {
                    "Hähnchenbrust würzen und in der Pfanne braten (ca. 6-8 Min pro Seite)",
                    "Salat waschen und in mundgerechte Stücke schneiden",
                    "Für das Dressing: Mayo, Senf, gehackten Knoblauch, Zitronensaft und Worcestershire-Sauce vermischen",
                    "Salat mit Dressing vermengen",
                    "Hähnchen in Streifen schneiden",
                    "Salat mit Hähnchen, Croutons und gehobeltem Parmesan servieren"
                },
                Image = "https://images.unsplash.com/photo-1546793665-c74683f339c1?w=800",
                IsFavorite = false,
                Tags = new List<string> { "salat", "proteinreich", "amerikanisch" }
            },
            new Recipe
            {
                Id = "4",
                Title = "Overnight Oats mit Beeren",
                Category = RecipeCategory.Breakfast,
                PrepTime = 5,
                CookTime = 0,
                Servings = 1,
                Difficulty = RecipeDifficulty.Easy,
                Ingredients = new List<string>
                {
                    "50g Haferflocken",
                    "150ml Milch oder Pflanzendrink",
                    "1 EL Chiasamen",
                    "1 TL Honig",
                    "Gemischte Beeren",
                    "Nüsse nach Wahl"
                },
                Instructions = new List<string>
                {
                    "Haferflocken, Milch, Chiasamen und Honig in einem Glas vermischen",
                    "Über Nacht (mindestens 4 Stunden) im Kühlschrank ziehen lassen",
                    "Am nächsten Morgen mit frischen Beeren und Nüssen toppen",
                    "Optional: zusätzlich mit Honig oder Ahornsirup süßen"
                },
                Image = "https://images.unsplash.com/photo-1517673132405-a56a62b18caf?w=800",
                IsFavorite = true,
                Tags = new List<string> { "gesund", "vorbereitung", "vegetarisch" }
            },
            new Recipe
            {
                Id = "5",
                Title = "Thailändisches Curry",
                Category = RecipeCategory.Dinner,
                PrepTime = 15,
                CookTime = 25,
                Servings = 4,
                Difficulty = RecipeDifficulty.Medium,
                Ingredients = new List<string>
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
                Instructions = new List<string>
                {
                    "Reis nach Packungsanweisung kochen",
                    "Hähnchen in Würfel schneiden, Gemüse in Streifen schneiden",
                    "Currypaste in etwas Kokosmilch anbraten",
                    "Hähnchen hinzufügen und anbraten",
                    "Restliche Kokosmilch, Gemüse hinzufügen und 15 Min köcheln lassen",
                    "Mit Fischsauce und Limettensaft abschmecken",
                    "Mit Basilikum garnieren und mit Reis servieren"
                },
                Image = "https://images.unsplash.com/photo-1455619452474-d2be8b1e70cd?w=800",
                IsFavorite = false,
                Tags = new List<string> { "asiatisch", "würzig", "curry" }
            },
            new Recipe
            {
                Id = "6",
                Title = "Griechischer Joghurt Bowl",
                Category = RecipeCategory.Breakfast,
                PrepTime = 5,
                CookTime = 0,
                Servings = 1,
                Difficulty = RecipeDifficulty.Easy,
                Ingredients = new List<string>
                {
                    "200g griechischer Joghurt",
                    "2 EL Honig",
                    "50g Granola",
                    "Frische Früchte (Beeren, Banane)",
                    "1 EL Mandeln",
                    "Minze zur Deko"
                },
                Instructions = new List<string>
                {
                    "Griechischen Joghurt in eine Bowl geben",
                    "Mit Honig beträufeln",
                    "Granola, frische Früchte und Mandeln darauf verteilen",
                    "Mit Minzblättern dekorieren"
                },
                Image = "https://images.unsplash.com/photo-1511376777868-611b54f68947?w=800",
                IsFavorite = false,
                Tags = new List<string> { "gesund", "schnell", "vegetarisch" }
            }
        };
    }
}
