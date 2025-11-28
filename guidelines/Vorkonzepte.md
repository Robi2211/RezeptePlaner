# Bereich 1 – Vorkonzepte für die Koch-App

## A) Farbkonzept (mindestens 5 Farben)

**Primäre Projektfarben (UI-Ebene)**

1. **Primary / Akzent Dunkelblau-Schwarz**  
   Token: `--primary` / Tailwind: `bg-primary`, `text-primary`  
   Hex (Light Theme): ca. `#030213`  
   Verwendung: Hauptakzente in der Navigation, wichtige interaktive Elemente, Primärbuttons.

2. **Sekundär-Violett (Secondary)**  
   Token: `--secondary` / Tailwind: `bg-secondary`  
   Wert: `oklch(0.95 0.0058 264.53)` (helles Violett)  
   Verwendung: Sekundäre Akzente, dezente Hintergründe, alternative Badges.

3. **Hintergrund Weiß**  
   Token: `--background` / Tailwind: `bg-background`  
   Hex: `#ffffff`  
   Verwendung: Haupt-Hintergrund der App, Karten-Layouts, helle Flächen.

4. **Muted Grau**  
  Token: `--muted` / Tailwind: `bg-muted`, `text-muted-foreground`  
  Hex: `#ececf0` (Hintergrund), `#717182` (Schrift)  
  Verwendung: gedämpfte Bereiche wie Input-Backgrounds, Unterzeilen und Labels.

5. **Orange Akzent (Highlight-Farbe)**  
  Token: `--color-orange-500` (Tailwind: `bg-orange-500`, `text-orange-500`)  
  Wert: `oklch(0.705 0.213 47.604)`  
  Verwendung: Hervorhebungen bei Buttons und Badges, z.B. Navigation (aktive Tabs), Schritt-Kreise in der Rezeptdetail-Seite, Schnellinfo-Karten im Dashboard und CTA-Buttons.

6. **Teal Akzent**  
   Token: `--color-chart-2` / indirekt via `--chart-2`  
   Wert: `oklch(0.6 0.118 184.704)` (kräftiges Teal)  
   Verwendung: Akzentfarbe in Charts, Badges, dekorative Elemente auf Dashboard und Wochenplan.

7. **Destructive Rot**  
   Token: `--destructive` / Tailwind: `bg-destructive`, `text-destructive`  
   Hex: `#d4183d`  
   Verwendung: Fehlerzustände, Löschaktionen (z.B. „Mahlzeit entfernen“), Warn-Hinweise.

8. **Grauskala / Neutral-Töne**  
   Tokens: `--color-gray-50` … `--color-gray-900`  
   Verwendung: Text (z.B. `text-gray-900`, `text-gray-600`), dezente Hintergründe (`bg-gray-50`), Kartenrahmen.

---

## A) Begründung Farbkonzept

- **Klarer Kontrast & Lesbarkeit:**  
  Weißer Hintergrund (`--background`) mit sehr dunklen Texten (`--foreground`, `text-gray-900`) sorgt für gute Lesbarkeit bei Rezept-Texten und Formularen.
- **Fokussierte Akzente:**  
  Primärfarbe (`--primary`), Orange (`--color-orange-500`) und Teal (`--chart-2`) werden gezielt für interaktive Elemente und Highlights eingesetzt (z.B. Badges, Status, Schritt-Kreise, aktive Tabs), damit wichtige Aktionen auffallen, ohne das UI zu überladen.
- **Ruhige Grundstimmung:**  
  Helle Grautöne (`--muted`, `--color-gray-*`) bilden die Basis für Karten und Sektionen. Sie lassen Food-Fotos und farbige Tags im Vordergrund wirken.
- **Warnlogik:**  
  Rot (`--destructive`) ist klar reserviert für Fehler, Delete-Buttons und kritische States. Dadurch erkennt der Nutzer sofort, wenn eine Aktion nicht rückgängig zu machen ist.
- **Konsistenz zwischen Screens:**  
  Das gleiche Set aus Weiß, Grau, Dunkelblau-Schwarz, Teal und Rot zieht sich durch Dashboard, Wochenplan, Favoriten, Bibliothek und Rezeptdetail – so wirkt das ganze Projekt wie aus einem Guss.

---

## B) Schriftarten (Alle im Projekt genutzten Typen)

Im generierten CSS (`index.css`) und globalen Styles (`globals.css`) werden vordefinierte Font-Stacks genutzt, kombiniert mit deinen inhaltlich geplanten Fonts:

1. **System Sans-Serif (Tailwind Sans)**  
   Token: `--font-sans`  
   Stack: `ui-sans-serif, system-ui, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", ...`  
   Verwendung: Basis für fast alle UI-Texte (Buttons, Labels, Standard-Text), z.B. in `Dashboard`, `RecipeLibrary`, `WeekPlanner`.

2. **System Mono**  
   Token: `--font-mono`  
   Stack: `ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, ...`  
   Verwendung: Code-ähnliche Stellen, kleine Token-Anzeigen im `DesignSystemPage`, z.B. Anzeige von CSS-Variablen.

3. **Geplante Display-Font: Playfair Display**  
   (in `DesignSystem.md` dokumentiert, optional über Google Fonts einbindbar)  
   Verwendung: große Überschriften, Markenbotschaften, Titel.

4. **Geplante Body-Font: Lato**  
   (ebenfalls in `DesignSystem.md` dokumentiert)  
   Verwendung: Fließtext, Labels, Buttons, Formulare, Rezeptbeschreibungen – sorgt für ruhige, moderne Lesbarkeit.

> Hinweis: Im aktuellen Build werden die System-Fonts (`--font-sans`, `--font-mono`) effektiv genutzt; Playfair Display und Lato sind als konzeptionelle Fonts angelegt und können über die im DesignSystem beschriebenen Snippets eingebunden werden.

---

## B) Begründung Schriftarten

- **Lesbarkeit im UI:**  
  Die systembasierte Sans-Serif-Schrift (`--font-sans`) ist auf allen Plattformen gut lesbar und lädt schnell, was für Formulare, Tabellen und lange Listen in der Koch-App wichtig ist.
- **Technische Klarheit:**  
  Monospace (`--font-mono`) wird gezielt dort eingesetzt, wo technische Informationen (z.B. CSS-Token) gezeigt werden – so erkennt der Nutzer sofort, dass es sich um „Code“ oder Systemwerte handelt.
- **Markenwirkung mit Display-Font:**  
  Playfair Display bringt eine elegante, leicht klassische Note hinein, die zu „Premium“-Rezepten und Event-Charakter passt.
- **Neutraler Fließtext:**  
  Lato verbindet Klarheit mit Freundlichkeit und eignet sich gut für deutschsprachige UI-Texte und längere Beschreibungen.

---

## C) Logo

Im Projekt wird aktuell eine Bilddatei als Logo in der Top-Navigation verwendet:

- Datei: `figma:asset/...` (import in `TopNavigation.tsx` als `logo`)  
- Platzierung: links in der festen Navigationsleiste  
- Umgebung: weißer Header-Hintergrund (`bg-white`), feine graue Border (`border-gray-200`).

Konzeptuell gehen wir von einer Wort-/Bildmarke aus, die zur edlen, aber freundlichen Koch- und Eventwelt passt.

**Logovariante 1 – Hell auf Dunkel**
- Hintergrund: `--background` (oder dunkler Grauton aus der Palette)  
- Schrift/Icon: `--primary` oder Teal-Akzent  
- Einsatz: Splash-Screens, vollflächige Hero-Sektionen, evtl. Login.

**Logovariante 2 – Dunkel auf Hell (aktuell umgesetzt)**
- Hintergrund: weiß (`#ffffff`)  
- Logo: dunkle Wortmarke/Bildmarke  
- Einsatz: Navigation, Header, Dokumente.

---

## C) Begründung Logo

- **Wiedererkennung:**  
  Ein zentrales Logo oben in der Navigation sorgt dafür, dass Nutzer jederzeit wissen, in welcher App sie sich befinden.
- **Neutraler Rahmen:**  
  Der weiße Header-Hintergrund macht das Logo unabhängig von den verschiedenen Inhaltsbereichen (Dashboard, Rezepte, Wochenplan) gut sichtbar.
- **Kompatibilität mit Farbkonzept:**  
  Das Logo harmoniert mit den Primärfarben und Teal-Akzenten; Gold- oder Teal-Töne können in der Wortmarke oder im Icon aufgegriffen werden.
- **Flexibilität:**  
  Durch die definierten hellen und dunklen Varianten kann das Logo sowohl auf weißen Flächen (App-Header) als auch auf dunklen Hintergründen (z.B. Marketingmaterial, Präsentationen) eingesetzt werden.
