# CookMate - .NET MAUI Rezepteplaner

Eine Windows Desktop-Anwendung zur Rezeptverwaltung, entwickelt mit .NET MAUI (C# + XAML).

## 📋 Projektübersicht

CookMate ist eine vollständige Rezepteverwaltungs-Anwendung, die folgende Anforderungen erfüllt:

### Makro-Ebene (4 Punkte)
- ✅ **Infoseite zum Programm** - Über CookMate, Features, Kontakt
- ✅ **Hilfebereich / FAQ-Bereich** - Durchsuchbare FAQs mit Kategorien
- ✅ **Eingabemaske mit Validierung** - Vollständiges Rezeptformular mit Fehlerprüfung

### Detail-Ebene (10 Punkte)
- ✅ **Navigation mit 4+ Seiten** - Rezepte, Neues Rezept, Info, Hilfe (+ Zusammenfassung)
- ✅ **Statische Bilder** - Logo und Icons als SVG
- ✅ **6 Steuerelemente verwendet**:
  - Entry (Texteingabe für Titel, Tags)
  - Switch (Vegetarisch, Vegan, Glutenfrei)
  - Slider (Vorbereitungs- und Kochzeit)
  - Picker (Kategorie, Schwierigkeit)
  - DatePicker (Geplantes Datum)
  - Stepper (Portionen)
- ✅ **CollectionView** - Rezeptliste mit Filterung und Suche

### Genauigkeit (6 Punkte)
- ✅ **Design System** - Farben und Schriften gemäß DesignSystem.md
- ✅ **Responsive Layout** - Grid-basiertes Desktop-Layout
- ✅ **Konsistente Styles** - Einheitliches Erscheinungsbild

### UX-Bewertung (8 Punkte)
- ✅ **Selbsterklärende Navigation** - Flyout-Menü mit Icons
- ✅ **Zweckmäßige Steuerelemente** - Passende Controls für jeden Input-Typ
- ✅ **Hilfreiche Fehlermeldungen** - Validierung mit deutschsprachigen Hinweisen
- ✅ **Zusammenfassungsseite** - Nach erfolgreicher Rezepterstellung

## 🏗️ Projektstruktur

```
RezeptePlaner.Maui/
├── App.xaml(.cs)          # Application Definition
├── AppShell.xaml(.cs)     # Shell Navigation
├── MauiProgram.cs         # DI Configuration
│
├── Models/
│   ├── Recipe.cs          # Recipe Model
│   └── FaqItem.cs         # FAQ Model
│
├── ViewModels/
│   ├── RecipeListViewModel.cs    # Rezeptliste
│   ├── AddRecipeViewModel.cs     # Rezept hinzufügen
│   ├── InfoViewModel.cs          # Info-Seite
│   ├── HelpViewModel.cs          # Hilfe/FAQ
│   └── SummaryViewModel.cs       # Zusammenfassung
│
├── Views/
│   ├── RecipesPage.xaml(.cs)     # Rezeptübersicht
│   ├── AddRecipePage.xaml(.cs)   # Neues Rezept
│   ├── InfoPage.xaml(.cs)        # Info/Über
│   ├── HelpPage.xaml(.cs)        # FAQ
│   └── SummaryPage.xaml(.cs)     # Zusammenfassung
│
├── Services/
│   ├── RecipeService.cs   # Rezept-Datenverwaltung
│   └── FaqService.cs      # FAQ-Daten
│
├── Converters/
│   └── ValueConverters.cs # Bool/String Converters
│
└── Resources/
    ├── Styles/
    │   ├── Colors.xaml    # Design System Farben
    │   └── Styles.xaml    # UI Styles
    └── Images/
        ├── logo.svg       # App Logo
        └── *_icon.svg     # Navigation Icons
```

## 🎨 Design System

### Farben
| Token | Hex | Verwendung |
|-------|-----|------------|
| Primary | #F4C15A | Goldgelb - Akzente, Buttons |
| Background | #0B0B0D | Dunkler Hintergrund |
| Surface | #17171B | Karten, Container |
| Foreground | #F5EADA | Haupttext |
| Success | #4CAF50 | Erfolgsmeldungen |
| Destructive | #E53935 | Fehler |

### Schriften
- **Display**: Playfair Display (Headlines)
- **Body**: Lato / OpenSans (UI-Texte)

## 🔧 Technische Details

- **.NET 9.0** mit MAUI
- **Windows 10** (10.0.19041.0+)
- **MVVM-Architektur** mit CommunityToolkit.Mvvm
- **Dependency Injection** für Services

## 🚀 Build & Run

### Voraussetzungen

1. **.NET 9 SDK** installiert
2. **MAUI Workload** installiert:
   ```bash
   dotnet workload install maui
   ```
3. **Windows 10/11** für das Ausführen der App

### Mit JetBrains Rider

1. Öffnen Sie die Solution-Datei `RezeptePlaner.sln` im Root-Verzeichnis
2. Stellen Sie sicher, dass das .NET MAUI Plugin in Rider aktiviert ist
3. Wählen Sie als Run Configuration: **Windows Machine**
4. Klicken Sie auf Run (F5) oder Debug

> 💡 **Tipp**: In Rider unter **Settings > Build, Execution, Deployment > Toolset and Build** die .NET SDK Version prüfen.

### Via Command Line

```bash
# MAUI Workload installieren (falls nicht vorhanden)
dotnet workload install maui

# Restore packages
dotnet restore

# Build (nur auf Windows möglich)
dotnet build

# Run
dotnet run --project RezeptePlaner.Maui/RezeptePlaner.Maui.csproj
```

> ⚠️ **Hinweis**: Die vollständige Kompilierung und Ausführung ist nur auf Windows möglich, da die App speziell für Windows Desktop entwickelt wurde.

## 📱 Features

1. **Rezepte durchsuchen** - Suche nach Name, Zutaten, Tags
2. **Rezepte erstellen** - Vollständiges Formular mit Validierung
3. **Kategorien** - Frühstück, Mittagessen, Abendessen, Snack
4. **Schwierigkeitsgrade** - Einfach, Mittel, Schwer
5. **Diät-Filter** - Vegetarisch, Vegan, Glutenfrei
6. **Favoriten** - Rezepte markieren
7. **FAQ-Hilfe** - Durchsuchbarer Hilfebereich

## 📄 Lizenz

© 2024 CookMate. Alle Rechte vorbehalten.
