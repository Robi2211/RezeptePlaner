import { Heart, Clock, Users, Search, BookMarked } from 'lucide-react';
import { useState } from 'react';
import type { Recipe } from '../App';

type FavoritesProps = {
  recipes: Recipe[];
  onViewRecipe: (id: string) => void;
  onToggleFavorite: (id: string) => void;
};

export function Favorites({ recipes, onViewRecipe, onToggleFavorite }: FavoritesProps) {
  const [searchQuery, setSearchQuery] = useState('');

  const filteredRecipes = recipes.filter(recipe =>
    recipe.title.toLowerCase().includes(searchQuery.toLowerCase()) ||
    recipe.tags.some(tag => tag.toLowerCase().includes(searchQuery.toLowerCase()))
  );

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
    <div className="max-w-[1600px] mx-auto px-8 py-12">
      {/* Header */}
      <div className="mb-12">
        <div className="flex items-center gap-4 mb-3">
          <div className="w-16 h-16 bg-gradient-to-br from-red-500 to-pink-500 rounded-2xl flex items-center justify-center">
            <Heart className="w-8 h-8 text-white fill-white" />
          </div>
          <div>
            <h1 className="text-gray-900">Meine Favoriten</h1>
            <p className="text-gray-600 text-lg">
              {recipes.length} {recipes.length === 1 ? 'Lieblingsrezept' : 'Lieblingsrezepte'} gespeichert
            </p>
          </div>
        </div>
      </div>

      {/* Search */}
      {recipes.length > 0 && (
        <div className="mb-8">
          <div className="relative max-w-xl">
            <Search className="absolute left-5 top-1/2 -translate-y-1/2 w-5 h-5 text-gray-400" />
            <input
              type="text"
              placeholder="Favoriten durchsuchen..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              className="w-full pl-14 pr-6 py-4 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent bg-white shadow-sm text-lg"
            />
          </div>
        </div>
      )}

      {/* Empty State */}
      {recipes.length === 0 ? (
        <div className="bg-white rounded-2xl border border-gray-200 p-16 shadow-sm">
          <div className="text-center max-w-md mx-auto">
            <div className="w-24 h-24 bg-gray-100 rounded-full flex items-center justify-center mx-auto mb-6">
              <Heart className="w-12 h-12 text-gray-300" />
            </div>
            <h2 className="text-gray-900 mb-3">Noch keine Favoriten</h2>
            <p className="text-gray-600 mb-8 text-lg">
              Markiere Rezepte mit einem Herz, um sie hier zu speichern und schnell wiederzufinden.
            </p>
            <button className="bg-amber-500 hover:bg-amber-600 text-white px-8 py-4 rounded-xl transition-colors">
              Rezepte entdecken
            </button>
          </div>
        </div>
      ) : filteredRecipes.length === 0 ? (
        <div className="bg-white rounded-2xl border border-gray-200 p-16 shadow-sm">
          <div className="text-center">
            <p className="text-gray-600 text-lg">Keine Rezepte gefunden</p>
            <p className="text-gray-500 mt-2">Versuche andere Suchbegriffe</p>
          </div>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {filteredRecipes.map(recipe => (
            <div
              key={recipe.id}
              className="bg-white rounded-2xl border border-gray-200 overflow-hidden hover:shadow-xl transition-all group"
            >
              {/* Image */}
              <div className="relative h-72 bg-gray-100 overflow-hidden">
                <img
                  src={recipe.image}
                  alt={recipe.title}
                  className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500 cursor-pointer"
                  onClick={() => onViewRecipe(recipe.id)}
                />
                <button
                  onClick={(e) => {
                    e.stopPropagation();
                    onToggleFavorite(recipe.id);
                  }}
                  className="absolute top-4 right-4 w-12 h-12 bg-white/95 backdrop-blur-sm rounded-full flex items-center justify-center hover:bg-white transition-all shadow-lg"
                >
                  <Heart className="w-6 h-6 fill-red-500 text-red-500" />
                </button>
              </div>

              {/* Content */}
              <div className="p-6 cursor-pointer" onClick={() => onViewRecipe(recipe.id)}>
                {/* Category & Difficulty */}
                <div className="flex items-center gap-2 mb-4">
                  <span className="px-3 py-1 bg-amber-50 text-amber-700 text-sm rounded-lg">
                    {getCategoryLabel(recipe.category)}
                  </span>
                  <span className="px-3 py-1 bg-gray-100 text-gray-600 text-sm rounded-lg">
                    {getDifficultyLabel(recipe.difficulty)}
                  </span>
                </div>

                {/* Title */}
                <h3 className="text-gray-900 mb-4 group-hover:text-teal-600 transition-colors leading-snug">
                  {recipe.title}
                </h3>

                {/* Meta Info */}
                <div className="flex items-center gap-6 text-gray-600 mb-4 pb-4 border-b border-gray-100">
                  <div className="flex items-center gap-2">
                    <Clock className="w-5 h-5" />
                    <span>{recipe.prepTime + recipe.cookTime} Min</span>
                  </div>
                  <div className="flex items-center gap-2">
                    <Users className="w-5 h-5" />
                    <span>{recipe.servings} Portionen</span>
                  </div>
                </div>

                {/* Tags */}
                <div className="flex flex-wrap gap-2 mb-3">
                  {recipe.tags.slice(0, 3).map(tag => (
                    <span key={tag} className="text-sm text-gray-500 bg-gray-50 px-3 py-1 rounded-full">
                      #{tag}
                    </span>
                  ))}
                </div>

                {/* Status Badge */}
                <div className="flex items-center gap-2 text-sm text-teal-600 mt-4">
                  <BookMarked className="w-4 h-4" />
                  <span>Als Favorit gespeichert</span>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

    </div>
  );
}
