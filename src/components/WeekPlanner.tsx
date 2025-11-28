import { useState } from 'react';
import { Plus, Trash2, Calendar as CalendarIcon, ChevronLeft, ChevronRight, Clock } from 'lucide-react';
import type { Recipe } from '../App';

type WeekPlannerProps = {
  recipes: Recipe[];
  onViewRecipe: (id: string) => void;
};

type PlannedMeal = {
  recipeId: string;
  mealType: 'breakfast' | 'lunch' | 'dinner';
};

export function WeekPlanner({ recipes, onViewRecipe }: WeekPlannerProps) {
  const [weekPlan, setWeekPlan] = useState<{ [key: string]: PlannedMeal[] }>({
    'Montag': [
      { recipeId: '1', mealType: 'breakfast' },
      { recipeId: '3', mealType: 'lunch' }
    ],
    'Dienstag': [
      { recipeId: '4', mealType: 'breakfast' },
      { recipeId: '2', mealType: 'dinner' }
    ],
    'Mittwoch': [
      { recipeId: '1', mealType: 'breakfast' },
      { recipeId: '5', mealType: 'dinner' }
    ],
    'Donnerstag': [
      { recipeId: '6', mealType: 'breakfast' },
      { recipeId: '3', mealType: 'lunch' }
    ],
    'Freitag': [
      { recipeId: '4', mealType: 'breakfast' },
      { recipeId: '2', mealType: 'dinner' }
    ],
    'Samstag': [],
    'Sonntag': []
  });

  const weekDays = ['Montag', 'Dienstag', 'Mittwoch', 'Donnerstag', 'Freitag', 'Samstag', 'Sonntag'];
  const dates = ['18. Nov', '19. Nov', '20. Nov', '21. Nov', '22. Nov', '23. Nov', '24. Nov'];

  const getRecipeById = (id: string) => recipes.find(r => r.id === id);

  const getMealTypeLabel = (mealType: string) => {
    const labels: { [key: string]: string } = {
      breakfast: 'Frühstück',
      lunch: 'Mittagessen',
      dinner: 'Abendessen'
    };
    return labels[mealType] || mealType;
  };

  const removeMeal = (day: string, recipeId: string) => {
    setWeekPlan(prev => ({
      ...prev,
      [day]: prev[day].filter(meal => meal.recipeId !== recipeId)
    }));
  };

  const totalPlannedMeals = Object.values(weekPlan).flat().length;

  const addMeal = (day: string) => {
    if (recipes.length === 0) return;

    const recipeTitles = recipes
      .map((r, index) => `${index + 1}. ${r.title}`)
      .join('\n');

    const recipeChoice = window.prompt(
      `Für welchen Tag möchtest du eine Mahlzeit hinzufügen?\n\nTag: ${day}\n\nWähle ein Rezept (Zahl eingeben):\n\n${recipeTitles}`
    );

    if (!recipeChoice) return;

    const recipeIndex = parseInt(recipeChoice, 10) - 1;
    if (Number.isNaN(recipeIndex) || recipeIndex < 0 || recipeIndex >= recipes.length) {
      window.alert('Ungültige Auswahl.');
      return;
    }

    const mealTypeChoice = window.prompt(
      'Für welche Mahlzeit möchtest du planen? (Frühstück / Mittagessen / Abendessen)'
    );

    if (!mealTypeChoice) return;

    const normalized = mealTypeChoice.trim().toLowerCase();
    let mealType: PlannedMeal['mealType'];

    if (normalized.startsWith('f')) {
      mealType = 'breakfast';
    } else if (normalized.startsWith('m')) {
      mealType = 'lunch';
    } else if (normalized.startsWith('a')) {
      mealType = 'dinner';
    } else {
      window.alert('Bitte "Frühstück", "Mittagessen" oder "Abendessen" eingeben.');
      return;
    }

    const selectedRecipe = recipes[recipeIndex];

    setWeekPlan(prev => ({
      ...prev,
      [day]: [...(prev[day] || []), { recipeId: selectedRecipe.id, mealType }]
    }));
  };

  return (
    <div className="max-w-[1600px] mx-auto px-6 py-4">
      {/* Week Navigation */}
  <div className="bg-white rounded-2xl border border-gray-200 p-4 mb-4 shadow-sm">
        <div className="flex items-center justify-between">
          <button className="p-3 hover:bg-gray-100 rounded-xl transition-colors">
            <ChevronLeft className="w-6 h-6 text-gray-600" />
          </button>
          
          <div className="text-center">
            <div className="flex items-center gap-4 justify-center mb-2">
              <CalendarIcon className="w-6 h-6 text-amber-600" />
                <h2 className="text-gray-900">18. - 24. November 2025</h2>
            </div>
            <p className="text-gray-600">{totalPlannedMeals} Mahlzeiten geplant</p>
          </div>

          <button className="p-3 hover:bg-gray-100 rounded-xl transition-colors">
            <ChevronRight className="w-6 h-6 text-gray-600" />
          </button>
        </div>
      </div>

      {/* Calendar Grid */}
      <div className="grid grid-cols-7 gap-6">
        {weekDays.map((day, index) => {
          const dayMeals = weekPlan[day] || [];
          const isToday = index === 3;

          return (
            <div
              key={day}
              className={`bg-white rounded-2xl border-2 ${
                isToday ? 'border-amber-400 shadow-lg' : 'border-gray-200 shadow-sm'
              } overflow-hidden flex flex-col min-h-[600px]`}
            >
              {/* Day Header */}
              <div className={`p-6 border-b ${isToday ? 'bg-amber-50 border-amber-200' : 'bg-gray-50 border-gray-200'}`}>
                <div className={`text-sm mb-1 ${isToday ? 'text-amber-600' : 'text-gray-500'}`}>
                  {dates[index]}
                </div>
                <div className={`${isToday ? 'text-amber-900' : 'text-gray-900'}`}>
                  {day}
                </div>
                {isToday && (
                  <div className="text-xs text-amber-600 mt-2 px-2 py-1 bg-amber-100 rounded-full inline-block">
                    Heute
                  </div>
                )}
              </div>

              {/* Meals */}
              <div className="p-4 flex-1 space-y-3">
                {dayMeals.map((meal, mealIndex) => {
                  const recipe = getRecipeById(meal.recipeId);
                  if (!recipe) return null;

                  return (
                    <div
                      key={`${meal.recipeId}-${mealIndex}`}
                      className="group relative"
                    >
                      <div
                        onClick={() => onViewRecipe(recipe.id)}
                        className="bg-gray-50 rounded-xl overflow-hidden cursor-pointer hover:shadow-md transition-all border border-gray-200 hover:border-teal-300"
                      >
                        <div className="h-32 bg-gray-200 relative">
                          <img 
                            src={recipe.image} 
                            alt={recipe.title} 
                            className="w-full h-full object-cover"
                          />
                          <div className="absolute top-3 left-3">
                            <span className="px-3 py-1 bg-white/95 backdrop-blur-sm text-xs rounded-full text-teal-700">
                              {getMealTypeLabel(meal.mealType)}
                            </span>
                          </div>
                        </div>
                        <div className="p-3">
                          <div className="text-sm text-gray-900 mb-2 line-clamp-2 leading-snug">
                            {recipe.title}
                          </div>
                          <div className="flex items-center gap-2 text-xs text-gray-500">
                            <Clock className="w-3 h-3" />
                            {recipe.prepTime + recipe.cookTime} Min
                          </div>
                        </div>
                      </div>
                      <button
                        onClick={() => removeMeal(day, meal.recipeId)}
                        className="absolute -top-2 -right-2 w-7 h-7 bg-red-500 hover:bg-red-600 text-white rounded-full flex items-center justify-center opacity-0 group-hover:opacity-100 transition-all shadow-lg"
                      >
                        <Trash2 className="w-4 h-4" />
                      </button>
                    </div>
                  );
                })}

                {/* Add Meal Button */}
                <button
                  type="button"
                  onClick={() => addMeal(day)}
                  className="w-full py-6 border-2 border-dashed border-gray-300 rounded-xl text-gray-400 hover:border-teal-400 hover:text-teal-600 hover:bg-teal-50/50 transition-all flex flex-col items-center justify-center gap-2"
                >
                  <Plus className="w-6 h-6" />
                  <span className="text-sm">Mahlzeit hinzufügen</span>
                </button>
              </div>
            </div>
          );
        })}
      </div>

    </div>
  );
}
