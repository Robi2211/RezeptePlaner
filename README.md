# CookMate - Rezepteplaner

Eine .NET MAUI Desktop-Anwendung für die Rezeptverwaltung und Wochenplanung.

## Über das Projekt

CookMate ist ein Rezepteplaner basierend auf dem CookMate UI Design von Figma. Die Anwendung wurde als .NET MAUI Projekt für Windows Desktop entwickelt.

Das Original-Design ist verfügbar unter: https://www.figma.com/design/9fXyoZpFW2L8p7K038euxJ/CookMate-UI-Design

## Funktionen

- **Dashboard**: Übersicht über heute geplante Mahlzeiten, Wochenübersicht und beliebte Rezepte
- **Rezeptbibliothek**: Durchsuchen und Filtern aller Rezepte nach Kategorie, Schwierigkeit und Zeit
- **Rezeptdetails**: Vollständige Ansicht mit Zutaten und Zubereitungsschritten
- **Wochenplan**: Mahlzeiten für die ganze Woche planen
- **Favoriten**: Lieblingsrezepte speichern und schnell wiederfinden
- **Neues Rezept**: Eigene Rezepte hinzufügen mit Validierung
- **Info & Hilfe**: Informationen über die App und FAQ

## Voraussetzungen

- .NET 9.0 SDK oder neuer
- Visual Studio 2022 (Version 17.8 oder neuer) mit MAUI-Workload
- Windows 10/11 für die Ausführung

## Installation

1. .NET MAUI Workload installieren:
   ```bash
   dotnet workload install maui
   ```

2. Projekt bauen:
   ```bash
   dotnet build RezeptePlaner.Maui/RezeptePlaner.Maui.csproj
   ```

3. Projekt ausführen:
   ```bash
   dotnet run --project RezeptePlaner.Maui/RezeptePlaner.Maui.csproj
   ```

## Projektstruktur

```
RezeptePlaner.Maui/
├── Views/              # XAML-Seiten und Code-Behind
├── ViewModels/         # MVVM ViewModels
├── Models/             # Datenmodelle
├── Services/           # Business Logic Services
├── Converters/         # Value Converters
├── Resources/          # Bilder, Fonts, Styles
└── Platforms/          # Plattform-spezifischer Code
```

## Design System

Die Anwendung verwendet ein dunkles Farbschema mit Gold/Amber als Primärfarbe:

- **Primary**: #F4C15A (Gold)
- **Background**: #0B0B0D (Dunkel)
- **Surface**: #17171B (Karten)
- **Foreground**: #F5EADA (Text)

## Lizenz

MIT License