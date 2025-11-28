import { useState } from 'react';
import { Search, Filter, Clock, Users, Heart } from 'lucide-react';
import type { Recipe } from '../App';

type RecipeLibraryProps = {
  recipes: Recipe[];
  onViewRecipe: (id: string) => void;
  onToggleFavorite: (id: string) => void;
};

export function RecipeLibrary({ recipes, onViewRecipe, onToggleFavorite }: RecipeLibraryProps) {
  const [searchQuery, setSearchQuery] = useState('');
  const [selectedCategory, setSelectedCategory] = useState<string>('all');
  const [selectedDifficulty, setSelectedDifficulty] = useState<string>('all');
  const [maxTime, setMaxTime] = useState<number>(999);

  const categories = [
    { value: 'all', label: 'Alle Kategorien' },
    { value: 'breakfast', label: 'Frühstück' },
    { value: 'lunch', label: 'Mittagessen' },
    { value: 'dinner', label: 'Abendessen' },
    { value: 'snack', label: 'Snack' },
  ];

  const difficulties = [
    { value: 'all', label: 'Alle Schwierigkeiten' },
    { value: 'easy', label: 'Einfach' },
    { value: 'medium', label: 'Mittel' },
    { value: 'hard', label: 'Schwer' },
  ];

  const filteredRecipes = recipes.filter(recipe => {
    const matchesSearch = recipe.title.toLowerCase().includes(searchQuery.toLowerCase()) ||
                         recipe.tags.some(tag => tag.toLowerCase().includes(searchQuery.toLowerCase()));
    const matchesCategory = selectedCategory === 'all' || recipe.category === selectedCategory;
    const matchesDifficulty = selectedDifficulty === 'all' || recipe.difficulty === selectedDifficulty;
    const matchesTime = (recipe.prepTime + recipe.cookTime) <= maxTime;

    return matchesSearch && matchesCategory && matchesDifficulty && matchesTime;
  });

  const getCategoryLabel = (category: string) => {
    const cat = categories.find(c => c.value === category);
    return cat ? cat.label : category;
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
    <div className="p-8">
      <div className="max-w-7xl mx-auto">
        <div className="mb-8">
          <h1 className="text-neutral-900 mb-2">Rezeptbibliothek</h1>
          <p className="text-neutral-600">Entdecke und verwalte all deine Lieblingsrezepte.</p>
        </div>

        {/* Search and Filters */}
        <div className="bg-white rounded-xl border border-neutral-200 p-6 mb-6">
          <div className="flex flex-col lg:flex-row gap-4 items-stretch">
            {/* Search */}
            <div className="flex-1 relative flex items-center">
              <Search className="absolute left-3 w-5 h-5 text-neutral-400 pointer-events-none" />
              <input
                type="text"
                placeholder="Rezepte oder Zutaten suchen..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="w-full h-12 pl-10 pr-4 border border-neutral-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent"
              />
            </div>

            {/* Category Filter */}
            <div className="relative flex items-center">
              <Filter className="absolute left-3 w-5 h-5 text-neutral-400 pointer-events-none" />
              <select
                value={selectedCategory}
                onChange={(e) => setSelectedCategory(e.target.value)}
                className="h-12 pl-10 pr-8 border border-neutral-200 rounded-lg appearance-none bg-white focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent cursor-pointer min-w-[200px]"
              >
                {categories.map(cat => (
                  <option key={cat.value} value={cat.value}>{cat.label}</option>
                ))}
              </select>
            </div>

            {/* Difficulty Filter */}
            <div className="relative flex items-center">
              <select
                value={selectedDifficulty}
                onChange={(e) => setSelectedDifficulty(e.target.value)}
                className="h-12 pl-4 pr-8 border border-neutral-200 rounded-lg appearance-none bg-white focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent cursor-pointer min-w-[180px]"
              >
                {difficulties.map(diff => (
                  <option key={diff.value} value={diff.value}>{diff.label}</option>
                ))}
              </select>
            </div>

            {/* Time Filter */}
            <div className="flex items-center gap-2 px-4 border border-neutral-200 rounded-lg bg-white min-w-[180px] h-12">
              <Clock className="w-5 h-5 text-neutral-400" />
              <input
                type="range"
                min="15"
                max="120"
                step="15"
                value={maxTime === 999 ? 120 : maxTime}
                onChange={(e) => setMaxTime(Number(e.target.value))}
                className="flex-1 accent-amber-500"
              />
              <span className="text-sm text-neutral-600 w-12">
                {maxTime === 999 ? '120+' : maxTime} Min
              </span>
            </div>
          </div>

          {/* Active Filters Info */}
          <div className="mt-4 pt-4 border-t border-neutral-100">
            <span className="text-sm text-neutral-600">
              {filteredRecipes.length} {filteredRecipes.length === 1 ? 'Rezept gefunden' : 'Rezepte gefunden'}
            </span>
          </div>
        </div>

        {/* Recipe Grid */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {filteredRecipes.map(recipe => (
            <div
              key={recipe.id}
              className="bg-white rounded-xl border border-neutral-200 overflow-hidden hover:shadow-lg transition-all cursor-pointer group"
            >
              <div className="relative h-48 bg-neutral-100 overflow-hidden">
                <img
                  src={recipe.image}
                  alt={recipe.title}
                  className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-300"
                  onClick={() => onViewRecipe(recipe.id)}
                />
                <button
                  onClick={(e) => {
                    e.stopPropagation();
                    onToggleFavorite(recipe.id);
                  }}
                  className="absolute top-3 right-3 w-10 h-10 bg-white/90 backdrop-blur-sm rounded-full flex items-center justify-center hover:bg-white transition-all"
                >
                  <Heart
                    className={`w-5 h-5 ${recipe.isFavorite ? 'fill-red-500 text-red-500' : 'text-neutral-600'}`}
                  />
                </button>
              </div>

              <div className="p-5" onClick={() => onViewRecipe(recipe.id)}>
                <div className="flex items-center gap-2 mb-2">
                  <span className="px-2 py-1 bg-amber-50 text-amber-700 text-xs rounded">
                    {getCategoryLabel(recipe.category)}
                  </span>
                  <span className="px-2 py-1 bg-neutral-100 text-neutral-600 text-xs rounded">
                    {getDifficultyLabel(recipe.difficulty)}
                  </span>
                </div>

                <h3 className="text-neutral-900 mb-3 group-hover:text-amber-600 transition-colors">
                  {recipe.title}
                </h3>

                <div className="flex items-center gap-4 text-sm text-neutral-600">
                  <div className="flex items-center gap-1">
                    <Clock className="w-4 h-4" />
                    {recipe.prepTime + recipe.cookTime} Min
                  </div>
                  <div className="flex items-center gap-1">
                    <Users className="w-4 h-4" />
                    {recipe.servings} Portionen
                  </div>
                </div>

                <div className="mt-3 flex flex-wrap gap-1">
                  {recipe.tags.slice(0, 3).map(tag => (
                    <span key={tag} className="text-xs text-neutral-500 bg-neutral-50 px-2 py-1 rounded">
                      #{tag}
                    </span>
                  ))}
                </div>
              </div>
            </div>
          ))}
        </div>

        {filteredRecipes.length === 0 && (
          <div className="text-center py-16">
            <div className="text-neutral-400 mb-2">Keine Rezepte gefunden</div>
            <p className="text-neutral-500 text-sm">Versuche andere Filter oder Suchbegriffe</p>
          </div>
        )}
      </div>
    </div>
  );
}
