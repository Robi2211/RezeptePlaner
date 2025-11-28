import { Home, BookOpen, Calendar, Heart, Plus, Palette } from 'lucide-react';
import type { View } from '../App';
import logo from 'figma:asset/e9c6e3d69047f0ff17bc35751a33c1af7ea4718f.png';

type TopNavigationProps = {
  currentView: View;
  onNavigate: (view: View) => void;
};

export function TopNavigation({ currentView, onNavigate }: TopNavigationProps) {
  const navItems = [
    { view: 'dashboard' as View, label: 'Dashboard', icon: Home },
    { view: 'recipes' as View, label: 'Rezepte', icon: BookOpen },
    { view: 'planner' as View, label: 'Wochenplan', icon: Calendar },
    { view: 'favorites' as View, label: 'Favoriten', icon: Heart },
    { view: 'design-system' as View, label: 'Design', icon: Palette },
  ];

  return (
    <header className="fixed top-0 left-0 right-0 bg-white border-b border-gray-200 z-50">
      <div className="max-w-[1600px] mx-auto px-8">
        <div className="flex items-center justify-between h-20">
          {/* Logo */}
          <div className="flex items-center gap-3">
            <img src={logo} alt="CookMate" className="h-14" />
          </div>

          {/* Navigation */}
          <nav className="flex items-center gap-1">
            {navItems.map(item => {
              const Icon = item.icon;
              const isActive = currentView === item.view;
              
              return (
                <button
                  key={item.view}
                  onClick={() => onNavigate(item.view)}
                  className={`flex items-center gap-2 px-6 py-5 transition-all relative rounded-t-lg ${
                    isActive
                      ? 'text-orange-600 bg-orange-50'
                      : 'text-gray-500 hover:text-gray-700 hover:bg-gray-50'
                  }`}
                >
                  <Icon className="w-5 h-5" />
                  <span className="font-semibold">{item.label}</span>
                  {isActive && (
                    <>
                      {/* oberer orangefarbener Strich über dem aktiven Tab */}
                      <div className="absolute top-0 left-0 right-0 h-1 bg-orange-500" />
                      {/* unterer orangefarbener Strich unter dem aktiven Tab */}
                      <div className="absolute bottom-0 left-0 right-0 h-1 bg-orange-500" />
                    </>
                  )}
                </button>
              );
            })}
          </nav>

          {/* Actions */}
          <div className="flex items-center gap-3">
            <button
              onClick={() => onNavigate('add-recipe')}
              className="border border-gray-300 hover:border-gray-400 text-gray-700 hover:bg-gray-50 px-5 py-3 rounded-lg transition-colors flex items-center gap-2"
            >
              <Plus className="w-5 h-5" />
              Neues Rezept
            </button>
          </div>
        </div>
      </div>
    </header>
  );
}
