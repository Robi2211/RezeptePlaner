import { ArrowLeft, Clock, Users, ChefHat, Heart, Calendar, Flame, Share2, Bookmark } from 'lucide-react';
import type { Recipe } from '../App';

type RecipeDetailProps = {
  recipe: Recipe;
  onBack: () => void;
  onToggleFavorite: (id: string) => void;
};

export function RecipeDetail({ recipe, onBack, onToggleFavorite }: RecipeDetailProps) {
  const getCategoryLabel = (category: string) => {
    const labels: { [key: string]: string } = {
      breakfast: 'Frühstück',
      lunch: 'Mittagessen',
      dinner: 'Abendessen',
      snack: 'Snack'
    };
    return labels[category] || category;
  };

  const getDifficultyLabel = (difficulty: string) => {
    const labels: { [key: string]: string } = {
      easy: 'Einfach',
      medium: 'Mittel',
      hard: 'Schwer'
    };
    return labels[difficulty] || difficulty;
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-[1400px] mx-auto px-6 py-8">
        {/* Back Button */}
        <button
          onClick={onBack}
          className="flex items-center gap-2 text-gray-600 hover:text-gray-900 transition-colors mb-6 px-3 py-2 hover:bg-white rounded-lg"
        >
          <ArrowLeft className="w-4 h-4" />
          <span className="text-sm">Zurück</span>
        </button>

        {/* Header Card */}
  <div className="bg-white rounded-2xl overflow-hidden mb-6 border border-gray-200 shadow-sm">
          <div className="grid grid-cols-1 lg:grid-cols-2">
            {/* Image */}
            <div className="h-[360px] bg-gray-100 relative">
              <img
                src={recipe.image}
                alt={recipe.title}
                className="w-full h-full object-cover"
              />
            </div>

            {/* Info */}
            <div className="p-8 flex flex-col h-full">
              {/* Tags */}
              <div className="flex items-center gap-2 mb-4">
                <span className="px-3 py-1 bg-orange-100 text-orange-700 rounded-lg text-xs font-medium">
                  {getCategoryLabel(recipe.category)}
                </span>
                <span className="px-3 py-1 bg-gray-100 text-gray-700 rounded-lg text-xs font-medium">
                  {getDifficultyLabel(recipe.difficulty)}
                </span>
              </div>

              {/* Title */}
              <h1 className="text-3xl font-semibold text-gray-900 mb-4 leading-tight">{recipe.title}</h1>

              {/* Meta Info */}
              <div className="grid grid-cols-3 gap-4 mb-6">
                <div className="flex items-center gap-3">
                  <div className="w-10 h-10 bg-gradient-to-br from-orange-400 to-red-500 rounded-lg flex items-center justify-center flex-shrink-0 shadow-sm">
                    <Clock className="w-5 h-5 text-white" strokeWidth={3} />
                  </div>
                  <div>
                    <div className="flex items-center gap-1.5">
                      <span className="text-xs text-gray-500">Zeit</span>
                    </div>
                    <div className="mt-0.5 flex items-baseline gap-1">
                      <span className="text-sm font-semibold text-gray-900">
                        {recipe.prepTime + recipe.cookTime}
                      </span>
                      <span className="text-[11px] text-gray-500">Minuten</span>
                    </div>
                  </div>
                </div>

                <div className="flex items-center gap-3">
                  <div className="w-10 h-10 bg-gradient-to-br from-blue-400 to-cyan-500 rounded-lg flex items-center justify-center flex-shrink-0 shadow-sm">
                    <Users className="w-5 h-5 text-white" strokeWidth={3} />
                  </div>
                  <div>
                    <div className="flex items-center gap-1.5">
                      <span className="text-xs text-gray-500">Portionen</span>
                    </div>
                    <div className="mt-0.5 flex items-baseline gap-1">
                      <span className="text-sm font-semibold text-gray-900">{recipe.servings}</span>
                      <span className="text-[11px] text-gray-500">Personen</span>
                    </div>
                  </div>
                </div>

                <div className="flex items-center gap-3">
                  <div className="w-10 h-10 bg-gradient-to-br from-purple-400 to-pink-500 rounded-lg flex items-center justify-center flex-shrink-0 shadow-sm">
                    <ChefHat className="w-5 h-5 text-white" strokeWidth={3} />
                  </div>
                  <div>
                    <div className="flex items-center gap-1.5">
                      <span className="text-xs text-gray-500">Level</span>
                    </div>
                    <div className="mt-0.5 text-sm font-semibold text-gray-900">
                      {getDifficultyLabel(recipe.difficulty)}
                    </div>
                  </div>
                </div>
              </div>

              {/* Tags */}
              <div className="flex flex-wrap gap-2 mt-auto pt-4 border-t border-gray-100">
                {recipe.tags.map(tag => (
                  <span key={tag} className="px-3 py-1 bg-gray-100 text-gray-600 rounded-full text-xs">
                    #{tag}
                  </span>
                ))}
              </div>
            </div>
          </div>
        </div>

        {/* Content */}
    <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        {/* Zutaten */}
        <div className="bg-white rounded-2xl p-6 border border-gray-200 shadow-sm">
          <div className="flex items-center justify-between mb-4">
            <h2 className="text-lg font-bold text-gray-900">Zutaten</h2>
            <span className="text-xs font-medium text-emerald-600 bg-emerald-50 px-2.5 py-1 rounded-full">
              {recipe.ingredients.length} Zutaten
            </span>
          </div>
          <ul className="space-y-1.5">
            {recipe.ingredients.map((ingredient, index) => (
              <li
                key={index}
                className="flex items-start gap-2 px-1.5 py-0.5"
              >
                <div className="mt-2 h-1.5 w-1.5 flex-shrink-0 rounded-full bg-orange-400" />
                <span className="text-sm text-gray-800 leading-relaxed flex-1">
                  {ingredient}
                </span>
              </li>
            ))}
          </ul>
        </div>

          {/* Zubereitung */}
          <div className="lg:col-span-2">
            <div className="bg-white rounded-2xl p-6 border border-gray-200 shadow-sm">
              <div className="mb-4">
                <h2 className="text-lg font-bold text-gray-900">Zubereitung</h2>
              </div>
              <div className="space-y-3">
                {recipe.instructions.map((instruction, index) => (
                  <div key={index} className="flex gap-3 items-start">
                    {/* Fester, orangefarbener Kreis per Inline-Style, unabhängig von Tailwind-Farben */}
                    <div
                      className="flex-shrink-0"
                      style={{
                        width: 28,
                        height: 28,
                        borderRadius: '9999px',
                        backgroundColor: '#ff9800',
                        color: '#ffffff',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        fontSize: '0.875rem',
                        fontWeight: 700,
                        boxShadow: '0 0 0 2px rgba(255, 152, 0, 0.25)',
                      }}
                    >
                      {index + 1}
                    </div>
                    <p className="flex-1 text-sm text-gray-800 leading-relaxed">
                      {instruction}
                    </p>
                  </div>
                ))}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}