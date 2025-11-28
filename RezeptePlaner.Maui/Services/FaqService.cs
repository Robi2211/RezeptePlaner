using System.Collections.ObjectModel;
using RezeptePlaner.Maui.Models;

namespace RezeptePlaner.Maui.Services;

/// <summary>
/// Service for FAQ data
/// </summary>
public class FaqService
{
    /// <summary>
    /// Get all FAQ items
    /// </summary>
    public ObservableCollection<FaqItem> GetFaqItems()
    {
        return new ObservableCollection<FaqItem>
        {
            new FaqItem
            {
                Question = "Wie füge ich ein neues Rezept hinzu?",
                Answer = "Navigieren Sie zur Seite 'Neues Rezept' über die Navigation. Füllen Sie alle Pflichtfelder aus (Titel, Kategorie, Zutaten, Anleitung) und klicken Sie auf 'Rezept speichern'. Nach erfolgreicher Validierung wird Ihr Rezept gespeichert.",
                Category = "Rezepte"
            },
            new FaqItem
            {
                Question = "Wie markiere ich ein Rezept als Favorit?",
                Answer = "Auf der Rezeptübersicht oder in der Detailansicht finden Sie ein Herz-Symbol. Klicken Sie darauf, um das Rezept zu Ihren Favoriten hinzuzufügen. Favoriten sind über das Navigationsmenü schnell erreichbar.",
                Category = "Favoriten"
            },
            new FaqItem
            {
                Question = "Welche Kategorien stehen zur Verfügung?",
                Answer = "CookMate bietet vier Kategorien: Frühstück, Mittagessen, Abendessen und Snack. Sie können jedes Rezept einer passenden Kategorie zuordnen.",
                Category = "Rezepte"
            },
            new FaqItem
            {
                Question = "Wie funktioniert die Suche?",
                Answer = "Nutzen Sie das Suchfeld auf der Rezeptübersicht. Sie können nach Rezeptnamen, Zutaten oder Tags suchen. Die Ergebnisse werden in Echtzeit gefiltert.",
                Category = "Navigation"
            },
            new FaqItem
            {
                Question = "Was bedeuten die Schwierigkeitsgrade?",
                Answer = "Einfach: Geeignet für Anfänger, wenige Zutaten, kurze Zubereitungszeit.\nMittel: Erfordert etwas Kocherfahrung und mehr Zutaten.\nSchwer: Für erfahrene Köche, komplexe Techniken oder viele Arbeitsschritte.",
                Category = "Rezepte"
            },
            new FaqItem
            {
                Question = "Kann ich Rezepte bearbeiten?",
                Answer = "Ja, öffnen Sie das gewünschte Rezept und klicken Sie auf 'Bearbeiten'. Ändern Sie die gewünschten Felder und speichern Sie Ihre Änderungen.",
                Category = "Rezepte"
            },
            new FaqItem
            {
                Question = "Wie funktioniert die Desktop-Ansicht?",
                Answer = "CookMate ist für Windows Desktop optimiert. Die Navigation befindet sich im linken Seitenbereich. Das Layout passt sich automatisch an Ihre Fenstergröße an (responsives Design).",
                Category = "Navigation"
            },
            new FaqItem
            {
                Question = "Werden meine Daten gespeichert?",
                Answer = "Alle Rezepte werden lokal auf Ihrem Gerät gespeichert. Bei einem App-Update bleiben Ihre Daten erhalten. Wir empfehlen dennoch regelmäßige Backups.",
                Category = "Allgemein"
            },
            new FaqItem
            {
                Question = "Was sind die diätetischen Filter?",
                Answer = "Sie können Rezepte nach vegetarisch, vegan und glutenfrei filtern. Diese Optionen können beim Erstellen eines Rezepts ausgewählt werden.",
                Category = "Rezepte"
            },
            new FaqItem
            {
                Question = "Wie kontaktiere ich den Support?",
                Answer = "Besuchen Sie unsere Info-Seite für Kontaktinformationen. Sie können uns per E-Mail unter support@cookmate.de erreichen.",
                Category = "Allgemein"
            }
        };
    }
}
