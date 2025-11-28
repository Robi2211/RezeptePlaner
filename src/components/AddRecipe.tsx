import { useState } from 'react';
import { ArrowLeft, Plus, X, Image as ImageIcon } from 'lucide-react';
import type { Recipe } from '../App';
import { ImageWithFallback } from './figma/ImageWithFallback';

type AddRecipeProps = {
  onAddRecipe: (recipe: Recipe) => void;
  onBack: () => void;
};

export function AddRecipe({ onAddRecipe, onBack }: AddRecipeProps) {
  const [title, setTitle] = useState('');
  const [category, setCategory] = useState<'breakfast' | 'lunch' | 'dinner' | 'snack'>('lunch');
  const [prepTime, setPrepTime] = useState(15);
  const [cookTime, setCookTime] = useState(30);
  const [servings, setServings] = useState(4);
  const [difficulty, setDifficulty] = useState<'easy' | 'medium' | 'hard'>('medium');
  const [imageUrl, setImageUrl] = useState('');
  const [ingredients, setIngredients] = useState<string[]>(['']);
  const [instructions, setInstructions] = useState<string[]>(['']);
  const [tags, setTags] = useState<string[]>([]);
  const [currentTag, setCurrentTag] = useState('');

  const addIngredient = () => {
    setIngredients([...ingredients, '']);
  };

  const updateIngredient = (index: number, value: string) => {
    const newIngredients = [...ingredients];
    newIngredients[index] = value;
    setIngredients(newIngredients);
  };

  const removeIngredient = (index: number) => {
    setIngredients(ingredients.filter((_, i) => i !== index));
  };

  const addInstruction = () => {
    setInstructions([...instructions, '']);
  };

  const updateInstruction = (index: number, value: string) => {
    const newInstructions = [...instructions];
    newInstructions[index] = value;
    setInstructions(newInstructions);
  };

  const removeInstruction = (index: number) => {
    setInstructions(instructions.filter((_, i) => i !== index));
  };

  const addTag = () => {
    if (currentTag.trim() && !tags.includes(currentTag.trim())) {
      setTags([...tags, currentTag.trim()]);
      setCurrentTag('');
    }
  };

  const removeTag = (tag: string) => {
    setTags(tags.filter(t => t !== tag));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (!title.trim()) {
      alert('Bitte gib einen Titel ein');
      return;
    }

    const filteredIngredients = ingredients.filter(i => i.trim() !== '');
    const filteredInstructions = instructions.filter(i => i.trim() !== '');

    if (filteredIngredients.length === 0) {
      alert('Bitte füge mindestens eine Zutat hinzu');
      return;
    }

    if (filteredInstructions.length === 0) {
      alert('Bitte füge mindestens einen Zubereitungsschritt hinzu');
      return;
    }

    const newRecipe: Recipe = {
      id: Date.now().toString(),
      title: title.trim(),
      category,
      prepTime,
      cookTime,
      servings,
      difficulty,
      ingredients: filteredIngredients,
      instructions: filteredInstructions,
      image: imageUrl || 'https://images.unsplash.com/photo-1546069901-ba9599a7e63c?w=800',
      isFavorite: false,
      tags
    };

    onAddRecipe(newRecipe);
    onBack();
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-[1400px] mx-auto px-8 py-12">
        {/* Header */}
        <button
          onClick={onBack}
          className="flex items-center gap-2 text-gray-600 hover:text-gray-900 transition-colors mb-8 px-4 py-2 hover:bg-white rounded-xl"
        >
          <ArrowLeft className="w-5 h-5" />
          <span>Zurück</span>
        </button>
        
        <div className="mb-4" />

        <form onSubmit={handleSubmit}>
          <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
            {/* Main Content */}
            <div className="lg:col-span-2 space-y-8">
              {/* Basic Info */}
              <div className="bg-white rounded-2xl border border-gray-200 p-8 shadow-sm">
                <h2 className="text-gray-900 mb-6">Grundinformationen</h2>
                
                <div className="space-y-6">
                  <div>
                    <label className="block text-gray-700 mb-3">
                      Rezeptname *
                    </label>
                    <input
                      type="text"
                      value={title}
                      onChange={(e) => setTitle(e.target.value)}
                      placeholder="z.B. Spaghetti Carbonara"
                      className="w-full px-5 py-4 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent text-lg"
                      required
                    />
                  </div>

                  <div className="grid grid-cols-2 gap-6">
                    <div>
                      <label className="block text-gray-700 mb-3">
                        Kategorie
                      </label>
                      <select
                        value={category}
                        onChange={(e) => setCategory(e.target.value as any)}
                        className="w-full px-5 py-4 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent appearance-none bg-white cursor-pointer text-lg"
                      >
                        <option value="breakfast">Frühstück</option>
                        <option value="lunch">Mittagessen</option>
                        <option value="dinner">Abendessen</option>
                        <option value="snack">Snack</option>
                      </select>
                    </div>

                    <div>
                      <label className="block text-gray-700 mb-3">
                        Schwierigkeit
                      </label>
                      <select
                        value={difficulty}
                        onChange={(e) => setDifficulty(e.target.value as any)}
                        className="w-full px-5 py-4 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent appearance-none bg-white cursor-pointer text-lg"
                      >
                        <option value="easy">Einfach</option>
                        <option value="medium">Mittel</option>
                        <option value="hard">Schwer</option>
                      </select>
                    </div>
                  </div>

                  <div className="grid grid-cols-3 gap-6">
                    <div>
                      <label className="block text-gray-700 mb-3">
                        Vorbereitung (Min)
                      </label>
                      <input
                        type="number"
                        value={prepTime}
                        onChange={(e) => setPrepTime(Number(e.target.value))}
                        min="0"
                        className="w-full px-5 py-4 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent text-lg"
                      />
                    </div>

                    <div>
                      <label className="block text-gray-700 mb-3">
                        Kochzeit (Min)
                      </label>
                      <input
                        type="number"
                        value={cookTime}
                        onChange={(e) => setCookTime(Number(e.target.value))}
                        min="0"
                        className="w-full px-5 py-4 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent text-lg"
                      />
                    </div>

                    <div>
                      <label className="block text-gray-700 mb-3">
                        Portionen
                      </label>
                      <input
                        type="number"
                        value={servings}
                        onChange={(e) => setServings(Number(e.target.value))}
                        min="1"
                        className="w-full px-5 py-4 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent text-lg"
                      />
                    </div>
                  </div>
                </div>
              </div>

              {/* Ingredients */}
              <div className="bg-white rounded-2xl border border-gray-200 p-8 shadow-sm">
                <div className="flex items-center justify-between mb-6">
                  <h2 className="text-gray-900">Zutaten</h2>
                  <button
                    type="button"
                    onClick={addIngredient}
                    className="text-teal-600 hover:text-teal-700 flex items-center gap-2 px-4 py-2 hover:bg-teal-50 rounded-lg transition-colors"
                  >
                    <Plus className="w-5 h-5" />
                    Zutat hinzufügen
                  </button>
                </div>

                <div className="space-y-3">
                  {ingredients.map((ingredient, index) => (
                    <div key={index} className="flex gap-3">
                      <input
                        type="text"
                        value={ingredient}
                        onChange={(e) => updateIngredient(index, e.target.value)}
                        placeholder={`Zutat ${index + 1}`}
                        className="flex-1 px-5 py-4 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent text-lg"
                      />
                      {ingredients.length > 1 && (
                        <button
                          type="button"
                          onClick={() => removeIngredient(index)}
                          className="p-4 text-red-500 hover:bg-red-50 rounded-xl transition-colors"
                        >
                          <X className="w-5 h-5" />
                        </button>
                      )}
                    </div>
                  ))}
                </div>
              </div>

              {/* Instructions */}
              <div className="bg-white rounded-2xl border border-gray-200 p-8 shadow-sm">
                <div className="flex items-center justify-between mb-6">
                  <h2 className="text-gray-900">Zubereitung</h2>
                  <button
                    type="button"
                    onClick={addInstruction}
                    className="text-teal-600 hover:text-teal-700 flex items-center gap-2 px-4 py-2 hover:bg-teal-50 rounded-lg transition-colors"
                  >
                    <Plus className="w-5 h-5" />
                    Schritt hinzufügen
                  </button>
                </div>

                <div className="space-y-4">
                  {instructions.map((instruction, index) => (
                    <div key={index} className="flex gap-4">
                      <div className="flex-shrink-0 w-10 h-10 bg-amber-500 text-white rounded-full flex items-center justify-center mt-2">
                        {index + 1}
                      </div>
                      <textarea
                        value={instruction}
                        onChange={(e) => updateInstruction(index, e.target.value)}
                        placeholder={`Schritt ${index + 1}`}
                        rows={3}
                        className="flex-1 px-5 py-4 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-amber-500 focus:border-transparent resize-none text-lg"
                      />
                      {instructions.length > 1 && (
                        <button
                          type="button"
                          onClick={() => removeInstruction(index)}
                          className="p-4 text-red-500 hover:bg-red-50 rounded-xl transition-colors self-start"
                        >
                          <X className="w-5 h-5" />
                        </button>
                      )}
                    </div>
                  ))}
                </div>
              </div>
            </div>

            {/* Sidebar */}
            <div className="space-y-8">
              {/* Image */}
              <div className="bg-white rounded-2xl border border-gray-200 p-8 shadow-sm">
                <h3 className="text-gray-900 mb-6">Rezeptbild</h3>
                
                {imageUrl ? (
                  <div className="relative">
                    <ImageWithFallback
                      src={imageUrl}
                      alt="Rezeptvorschau"
                      className="w-full h-64 object-cover rounded-xl"
                    />
                    <button
                      type="button"
                      onClick={() => setImageUrl('')}
                      className="absolute top-3 right-3 w-10 h-10 bg-red-500 hover:bg-red-600 text-white rounded-full flex items-center justify-center shadow-lg"
                    >
                      <X className="w-5 h-5" />
                    </button>
                  </div>
                ) : (
                  <div className="border-2 border-dashed border-gray-300 rounded-xl p-10 text-center bg-gray-50">
                    <ImageIcon className="w-16 h-16 text-gray-300 mx-auto mb-4" />
                    <p className="text-gray-600 mb-4">Bild-URL hinzufügen</p>
                    <input
                      type="url"
                      value={imageUrl}
                      onChange={(e) => setImageUrl(e.target.value)}
                      placeholder="https://..."
                      className="w-full px-4 py-3 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-amber-500"
                    />
                  </div>
                )}
              </div>

              {/* Tags */}
              <div className="bg-white rounded-2xl border border-gray-200 p-8 shadow-sm">
                <h3 className="text-gray-900 mb-6">Tags</h3>
                
                <div className="flex gap-3 mb-4">
                  <input
                    type="text"
                    value={currentTag}
                    onChange={(e) => setCurrentTag(e.target.value)}
                    onKeyPress={(e) => e.key === 'Enter' && (e.preventDefault(), addTag())}
                    placeholder="Tag eingeben..."
                    className="flex-1 px-4 py-3 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-amber-500"
                  />
                  <button
                    type="button"
                    onClick={addTag}
                    className="px-5 py-3 bg-teal-500 hover:bg-teal-600 text-white rounded-lg transition-colors"
                  >
                    <Plus className="w-5 h-5" />
                  </button>
                </div>

                <div className="flex flex-wrap gap-2">
                  {tags.map(tag => (
                    <span
                      key={tag}
                      className="px-4 py-2 bg-amber-50 text-amber-700 rounded-full flex items-center gap-2"
                    >
                      #{tag}
                      <button
                        type="button"
                        onClick={() => removeTag(tag)}
                        className="hover:text-amber-900"
                      >
                        <X className="w-4 h-4" />
                      </button>
                    </span>
                  ))}
                </div>
              </div>

              {/* Actions */}
              <button
                type="submit"
                className="w-full bg-amber-500 hover:bg-amber-600 text-white px-8 py-5 rounded-xl transition-colors shadow-sm"
              >
                Rezept speichern
              </button>

              <button
                type="button"
                onClick={onBack}
                className="w-full bg-gray-100 hover:bg-gray-200 text-gray-700 px-8 py-5 rounded-xl transition-colors"
              >
                Abbrechen
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}
