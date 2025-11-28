import { useState } from 'react';
import { TopNavigation } from './components/TopNavigation';
import { Dashboard } from './components/Dashboard';
import { RecipeLibrary } from './components/RecipeLibrary';
import { RecipeDetail } from './components/RecipeDetail';
import { WeekPlanner } from './components/WeekPlanner';
import { Favorites } from './components/Favorites';
import { AddRecipe } from './components/AddRecipe';
import { DesignSystemPage } from './components/DesignSystemPage';

export type Recipe = {
  id: string;
  title: string;
  category: 'breakfast' | 'lunch' | 'dinner' | 'snack';
  prepTime: number;
  cookTime: number;
  servings: number;
  difficulty: 'easy' | 'medium' | 'hard';
  ingredients: string[];
  instructions: string[];
  image: string;
  isFavorite: boolean;
  tags: string[];
};

export type View = 'dashboard' | 'recipes' | 'recipe-detail' | 'planner' | 'favorites' | 'add-recipe' | 'design-system';

export default function App() {
  const [currentView, setCurrentView] = useState<View>('dashboard');
  const [selectedRecipeId, setSelectedRecipeId] = useState<string | null>(null);
  const [recipes, setRecipes] = useState<Recipe[]>([
    {
      id: '1',
      title: 'Avocado-Toast mit pochiertem Ei',
      category: 'breakfast',
      prepTime: 5,
      cookTime: 10,
      servings: 2,
      difficulty: 'easy',
      ingredients: [
        '2 Scheiben Vollkornbrot',
        '1 reife Avocado',
        '2 Eier',
        'Salz und Pfeffer',
        'Chiliflocken',
        'Zitronensaft'
      ],
      instructions: [
        'Brot toasten bis es goldbraun ist',
        'Avocado zerdrücken und mit Zitronensaft, Salz und Pfeffer würzen',
        'Wasser zum Kochen bringen und Eier pochieren (ca. 3-4 Minuten)',
        'Avocado auf Toast verteilen',
        'Pochiertes Ei darauf legen und mit Chiliflocken garnieren'
      ],
      image: 'https://images.unsplash.com/photo-1541519227354-08fa5d50c44d?w=800',
      isFavorite: true,
      tags: ['gesund', 'schnell', 'vegetarisch']
    },
    {
      id: '2',
      title: 'Spaghetti Carbonara',
      category: 'dinner',
      prepTime: 10,
      cookTime: 20,
      servings: 4,
      difficulty: 'medium',
      ingredients: [
        '400g Spaghetti',
        '200g Guanciale oder Pancetta',
        '4 Eigelb',
        '100g Pecorino Romano',
        'Schwarzer Pfeffer',
        'Salz'
      ],
      instructions: [
        'Spaghetti in reichlich Salzwasser al dente kochen',
        'Guanciale in kleine Würfel schneiden und knusprig braten',
        'Eigelb mit geriebenem Pecorino und viel schwarzem Pfeffer vermischen',
        'Pasta abgießen, etwas Nudelwasser auffangen',
        'Heiße Pasta mit Guanciale mischen, dann Ei-Käse-Mischung unterrühren',
        'Mit Nudelwasser die gewünschte Konsistenz erreichen'
      ],
      image: 'https://images.unsplash.com/photo-1612874742237-6526221588e3?w=800',
      isFavorite: true,
      tags: ['italienisch', 'klassiker', 'pasta']
    },
    {
      id: '3',
      title: 'Caesar Salad',
      category: 'lunch',
      prepTime: 15,
      cookTime: 10,
      servings: 2,
      difficulty: 'easy',
      ingredients: [
        '2 Römersalat-Herzen',
        '2 Hähnchenbrüste',
        '100g Parmesan',
        'Croutons',
        '3 EL Mayonnaise',
        '1 EL Dijon-Senf',
        '2 Knoblauchzehen',
        'Zitronensaft',
        'Worcestershire-Sauce'
      ],
      instructions: [
        'Hähnchenbrust würzen und in der Pfanne braten (ca. 6-8 Min pro Seite)',
        'Salat waschen und in mundgerechte Stücke schneiden',
        'Für das Dressing: Mayo, Senf, gehackten Knoblauch, Zitronensaft und Worcestershire-Sauce vermischen',
        'Salat mit Dressing vermengen',
        'Hähnchen in Streifen schneiden',
        'Salat mit Hähnchen, Croutons und gehobeltem Parmesan servieren'
      ],
      image: 'https://images.unsplash.com/photo-1546793665-c74683f339c1?w=800',
      isFavorite: false,
      tags: ['salat', 'proteinreich', 'amerikanisch']
    },
    {
      id: '4',
      title: 'Overnight Oats mit Beeren',
      category: 'breakfast',
      prepTime: 5,
      cookTime: 0,
      servings: 1,
      difficulty: 'easy',
      ingredients: [
        '50g Haferflocken',
        '150ml Milch oder Pflanzendrink',
        '1 EL Chiasamen',
        '1 TL Honig',
        'Gemischte Beeren',
        'Nüsse nach Wahl'
      ],
      instructions: [
        'Haferflocken, Milch, Chiasamen und Honig in einem Glas vermischen',
        'Über Nacht (mindestens 4 Stunden) im Kühlschrank ziehen lassen',
        'Am nächsten Morgen mit frischen Beeren und Nüssen toppen',
        'Optional: zusätzlich mit Honig oder Ahornsirup süßen'
      ],
      image: 'https://images.unsplash.com/photo-1517673132405-a56a62b18caf?w=800',
      isFavorite: true,
      tags: ['gesund', 'vorbereitung', 'vegetarisch']
    },
    {
      id: '5',
      title: 'Thailändisches Curry',
      category: 'dinner',
      prepTime: 15,
      cookTime: 25,
      servings: 4,
      difficulty: 'medium',
      ingredients: [
        '400ml Kokosmilch',
        '2-3 EL rote Currypaste',
        '500g Hähnchenfleisch',
        '1 Paprika',
        '200g Zucchini',
        '1 Zwiebel',
        'Basilikum',
        'Fischsauce',
        'Limettensaft',
        'Jasminreis'
      ],
      instructions: [
        'Reis nach Packungsanweisung kochen',
        'Hähnchen in Würfel schneiden, Gemüse in Streifen schneiden',
        'Currypaste in etwas Kokosmilch anbraten',
        'Hähnchen hinzufügen und anbraten',
        'Restliche Kokosmilch, Gemüse hinzufügen und 15 Min köcheln lassen',
        'Mit Fischsauce und Limettensaft abschmecken',
        'Mit Basilikum garnieren und mit Reis servieren'
      ],
      image: 'https://images.unsplash.com/photo-1455619452474-d2be8b1e70cd?w=800',
      isFavorite: false,
      tags: ['asiatisch', 'würzig', 'curry']
    },
    {
      id: '6',
      title: 'Griechischer Joghurt Bowl',
      category: 'breakfast',
      prepTime: 5,
      cookTime: 0,
      servings: 1,
      difficulty: 'easy',
      ingredients: [
        '200g griechischer Joghurt',
        '2 EL Honig',
        '50g Granola',
        'Frische Früchte (Beeren, Banane)',
        '1 EL Mandeln',
        'Minze zur Deko'
      ],
      instructions: [
        'Griechischen Joghurt in eine Bowl geben',
        'Mit Honig beträufeln',
        'Granola, frische Früchte und Mandeln darauf verteilen',
        'Mit Minzblättern dekorieren'
      ],
      image: 'https://images.unsplash.com/photo-1511376777868-611b54f68947?w=800',
      isFavorite: false,
      tags: ['gesund', 'schnell', 'vegetarisch']
    }
  ]);

  const toggleFavorite = (recipeId: string) => {
    setRecipes(prev => prev.map(recipe => 
      recipe.id === recipeId ? { ...recipe, isFavorite: !recipe.isFavorite } : recipe
    ));
  };

  const viewRecipeDetail = (recipeId: string) => {
    setSelectedRecipeId(recipeId);
    setCurrentView('recipe-detail');
  };

  const navigateToView = (view: View) => {
    setCurrentView(view);
    if (view !== 'recipe-detail') {
      setSelectedRecipeId(null);
    }
  };

  const addRecipe = (recipe: Recipe) => {
    setRecipes(prev => [recipe, ...prev]);
  };

  const selectedRecipe = selectedRecipeId ? recipes.find(r => r.id === selectedRecipeId) : null;

  return (
    <div className="min-h-screen bg-gray-50">
      <TopNavigation currentView={currentView} onNavigate={navigateToView} />
      
      <main className="pt-20">
        {currentView === 'dashboard' && (
          <Dashboard 
            recipes={recipes} 
            onViewRecipe={viewRecipeDetail}
            onNavigateToPlanner={() => navigateToView('planner')}
          />
        )}
        {currentView === 'recipes' && (
          <RecipeLibrary 
            recipes={recipes} 
            onViewRecipe={viewRecipeDetail}
            onToggleFavorite={toggleFavorite}
          />
        )}
        {currentView === 'recipe-detail' && selectedRecipe && (
          <RecipeDetail 
            recipe={selectedRecipe}
            onBack={() => navigateToView('recipes')}
            onToggleFavorite={toggleFavorite}
          />
        )}
        {currentView === 'planner' && (
          <WeekPlanner 
            recipes={recipes}
            onViewRecipe={viewRecipeDetail}
          />
        )}
        {currentView === 'favorites' && (
          <Favorites 
            recipes={recipes.filter(r => r.isFavorite)}
            onViewRecipe={viewRecipeDetail}
            onToggleFavorite={toggleFavorite}
          />
        )}
        {currentView === 'add-recipe' && (
          <AddRecipe 
            onAddRecipe={addRecipe}
            onBack={() => navigateToView('recipes')}
          />
        )}
        {currentView === 'design-system' && (
          <DesignSystemPage />
        )}
      </main>
    </div>
  );
}
