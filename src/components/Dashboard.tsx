import { Clock, Users, ArrowRight, TrendingUp, Calendar } from 'lucide-react';
import type { Recipe } from '../App';

type DashboardProps = {
  recipes: Recipe[];
  onViewRecipe: (id: string) => void;
  onNavigateToPlanner: () => void;
};

export function Dashboard({ recipes, onViewRecipe, onNavigateToPlanner }: DashboardProps) {
  const weekDays = [
    { day: 'Montag', date: '18. Nov', recipes: ['1', '3'] },
    { day: 'Dienstag', date: '19. Nov', recipes: ['4', '2'] },
    { day: 'Mittwoch', date: '20. Nov', recipes: ['1', '5'] },
    { day: 'Donnerstag', date: '21. Nov', recipes: ['6', '3'] },
    { day: 'Freitag', date: '22. Nov', recipes: ['4', '2'] },
  ];

  const getRecipeById = (id: string) => recipes.find(r => r.id === id);

  const todayRecipes = weekDays[3].recipes.map(id => getRecipeById(id)).filter(Boolean) as Recipe[];

  return (
    <div className="max-w-[1400px] mx-auto px-6 py-8 bg-gradient-to-br from-orange-25 via-white to-emerald-25">
      {/* Kompakter Header */}
      <div className="mb-6">
        <h1 className="text-2xl font-bold text-gray-900 mb-1">Dashboard</h1>
        <p className="text-gray-600 text-sm">Donnerstag, 21. November</p>
      </div>

      {/* Today's Meals - Kompakt, leicht orange eingefärbt */}
      <div className="mb-6">
        <h2 className="text-lg font-semibold text-gray-900 mb-3">Heute geplant</h2>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
          {todayRecipes.map((recipe, index) => {
            // leicht orange eingefärbte Karten
            const colorSet = {
              bg: 'bg-gradient-to-br from-orange-50 via-orange-50 to-orange-100',
              border: 'border-orange-200',
              // Badge: helles Orange + dunkler Text, damit nicht „weiß auf hell“ ist
              tag: 'bg-orange-100 border border-orange-300 text-orange-900',
              hover: 'hover:border-orange-400',
            };

            return (
              <div
                key={recipe.id}
                onClick={() => onViewRecipe(recipe.id)}
                className={`flex gap-3 ${colorSet.bg} p-3 rounded-lg border ${colorSet.border} cursor-pointer ${colorSet.hover} hover:shadow-md transition-all`}
              >
                <div className="w-20 h-20 rounded-lg overflow-hidden bg-white flex-shrink-0 shadow-sm">
                  <img
                    src={recipe.image}
                    alt={recipe.title}
                    className="w-full h-full object-cover"
                  />
                </div>
                <div className="flex-1 min-w-0">
                  <span
                    className={`inline-block px-2 py-0.5 ${colorSet.tag} text-xs rounded mb-1 font-semibold`}
                  >
                    {recipe.category === 'breakfast'
                      ? 'Frühstück'
                      : recipe.category === 'lunch'
                      ? 'Mittagessen'
                      : 'Abendessen'}
                  </span>
                  <h3 className="text-sm font-semibold text-gray-900 mb-1 truncate">
                    {recipe.title}
                  </h3>
                  <div className="flex items-center gap-3 text-xs text-gray-600">
                    <div className="flex items-center gap-1">
                      <Clock className="w-3 h-3" />
                      <span>{recipe.prepTime + recipe.cookTime} Min</span>
                    </div>
                    <div className="flex items-center gap-1">
                      <Users className="w-3 h-3" />
                      <span>{recipe.servings} Port.</span>
                    </div>
                  </div>
                </div>
              </div>
            );
          })}
        </div>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-4">
        {/* Week Overview */}
        <div className="lg:col-span-2 bg-white rounded-lg border border-gray-200 p-5">
          <div className="flex items-center justify-between mb-4">
            <h2 className="text-lg font-semibold text-gray-900">Wochenübersicht</h2>
            <button 
              onClick={onNavigateToPlanner}
              className="text-orange-600 hover:text-orange-700 flex items-center gap-1 text-sm transition-colors"
            >
              Zum Wochenplan
              <ArrowRight className="w-4 h-4" />
            </button>
          </div>

          <div className="space-y-2">
            {weekDays.map((day) => (
              // vorher: bg-gray-50 hover:bg-gray-100
              <div
                key={day.day}
                className="flex items-center gap-4 p-3 rounded-lg transition-colors bg-orange-50/60 hover:bg-orange-100/80"
              >
                <div className="w-24 flex-shrink-0">
                  <div className="text-sm font-semibold text-gray-900">{day.day}</div>
                  <div className="text-xs text-gray-500">{day.date}</div>
                </div>
                
                <div className="flex-1 flex gap-2 overflow-x-auto">
                  {day.recipes.map(recipeId => {
                    const recipe = getRecipeById(recipeId);
                    if (!recipe) return null;
                    
                    return (
                      <div
                        key={recipeId}
                        onClick={() => onViewRecipe(recipeId)}
                        className="flex items-center gap-2 bg-white px-3 py-2 rounded-lg border border-gray-200 cursor-pointer hover:border-orange-400 transition-all flex-shrink-0 min-w-[200px]"
                      >
                        <div className="w-12 h-12 rounded-lg overflow-hidden bg-gray-100 flex-shrink-0">
                          <img src={recipe.image} alt="" className="w-full h-full object-cover" />
                        </div>
                        <div className="flex-1 min-w-0">
                          <div className="text-xs font-semibold text-gray-900 truncate">{recipe.title}</div>
                          <div className="text-xs text-gray-500 flex items-center gap-1">
                            <Clock className="w-3 h-3" />
                            {recipe.prepTime + recipe.cookTime} Min
                          </div>
                        </div>
                      </div>
                    );
                  })}
                </div>
              </div>
            ))}
          </div>
        </div>

        {/* Sidebar */}
        <div className="space-y-4">
          {/* Trending */}
          <div className="bg-gradient-to-br from-green-50 to-emerald-50 rounded-lg border-2 border-green-200 p-5 shadow-sm">
            <div className="flex items-center gap-2 mb-4">
              <div className="w-8 h-8 bg-gradient-to-br from-green-500 to-emerald-600 rounded-lg flex items-center justify-center">
                {/* Icon jetzt satt grün */}
                <TrendingUp className="w-5 h-5 text-green-600" strokeWidth={2.5} />
              </div>
              <h3 className="text-base font-semibold text-gray-900">Beliebt</h3>
            </div>
            <div className="space-y-3">
              {recipes.slice(1, 4).map(recipe => (
                <div
                  key={recipe.id}
                  onClick={() => onViewRecipe(recipe.id)}
                  className="flex gap-3 cursor-pointer hover:bg-white/60 p-2 rounded-lg transition-colors bg-white/30"
                >
                  <div className="w-14 h-14 rounded-lg overflow-hidden bg-white flex-shrink-0 shadow-sm">
                    <img 
                      src={recipe.image} 
                      alt={recipe.title} 
                      className="w-full h-full object-cover" 
                    />
                  </div>
                  <div className="flex-1 min-w-0">
                    <div className="text-sm font-semibold text-gray-900 mb-1 line-clamp-2">
                      {recipe.title}
                    </div>
                    <div className="flex items-center gap-1 text-xs text-gray-600">
                      <Clock className="w-3 h-3" />
                      {recipe.prepTime + recipe.cookTime} Min
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </div>

          {/* Quick Stats */}
          <div className="bg-gradient-to-br from-orange-50 to-orange-100 rounded-lg border-2 border-orange-200 p-5 shadow-sm">
            <h3 className="text-base font-semibold text-gray-900 mb-3">Schnellinfo</h3>
            <div className="space-y-3">
              <div className="flex justify-between items-center p-2 bg-white/80 rounded-lg">
                <span className="text-sm text-gray-600">Rezepte</span>
                <span className="font-bold text-orange-600">{recipes.length}</span>
              </div>
              <div className="flex justify-between items-center p-2 bg-white/80 rounded-lg">
                <span className="text-sm text-gray-600">Favoriten</span>
                <span className="font-bold text-green-600">{recipes.filter(r => r.isFavorite).length}</span>
              </div>
              <button 
                onClick={onNavigateToPlanner}
                className="w-full !bg-orange-500 hover:!bg-orange-600 !text-white !px-4 !py-2.5 !rounded-lg !transition-all !border !border-orange-600 flex items-center justify-center gap-2 mt-3 text-sm font-medium !shadow-md hover:!shadow-lg hover:scale-[1.02] active:scale-[0.99] cursor-pointer"
              >
                <Calendar className="w-4 h-4" />
                <span className="uppercase tracking-wide text-xs font-semibold">
                  Wochenplan öffnen
                </span>
                <ArrowRight className="w-4 h-4" />
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
