# Designsystem Kochseite

Dieses Dokument beschreibt das Farbkonzept, die Typografie und die Logo-Guidelines für das gesamte Projekt. Es ist als Referenz gedacht, damit Designentscheidungen nachvollziehbar und im Code konsistent umgesetzt werden können.

---

## 1. Farbkonzept

### 1.1 Primärfarben

| Token        | Hex       | Verwendung                                                |
|-------------|-----------|-----------------------------------------------------------|
| `primary`   | `#F4C15A` | Hauptakzente, interaktive Elemente (Buttons, Links, Icons, aktive States) |
| `secondary` | `#D7A94B` | Sekundäre Akzente, Badges, Hover-States                  |

**Begründung:**
- Ein warmes Goldgelb vermittelt **Premium**, **Wärme** und **Genuss** – ideal für eine Koch- und Eventplattform.
- Die sekundäre Goldnuance gibt Spielraum für Abstufungen (z.B. Hover, alternative Buttons), ohne den Stil zu brechen.
- Beide Töne funktionieren auf hellem und dunklem Hintergrund und wirken wertig statt verspielt.

### 1.2 Neutrale Farben

| Token        | Hex       | Verwendung                                   |
|-------------|-----------|----------------------------------------------|
| `background`| `#0B0B0D` | Haupt-Hintergrund bei dunklen Sektionen      |
| `surface`   | `#17171B` | Karten, Container, Modals                    |
| `muted`     | `#2A2A30` | Gedämpfte Bereiche, Input-Backgrounds        |
| `border`    | `#2F2F36` | Umrandungen, Divider                         |
| `foreground`| `#F5EADA` | Haupttext auf dunklem Hintergrund           |
| `foregroundMuted` | `#C9BBA4` | Sekundärtext, Labels                 |

**Begründung:**
- Das dunkle Grundthema schafft eine **Bühne für Food-Fotografie** und goldene Akzente.
- Helle, cremefarbene Schrift sorgt für hohe Lesbarkeit und wirkt weniger hart als reines Weiß.
- Mehrere Graustufen ermöglichen Hierarchie (Hintergrund, Card, Muted) ohne Ablenkung.

### 1.3 Status- und Feedbackfarben

| Token          | Hex       | Verwendung                               |
|---------------|-----------|------------------------------------------|
| `success`     | `#4CAF50` | Positive Zustände (z.B. gespeichert)     |
| `warning`     | `#FBC02D` | Hinweise, leichte Warnungen              |
| `destructive` | `#E53935` | Fehler, destruktive Aktionen (Löschen)   |

**Begründung:**
- Klassische Ampelfarben, leicht entsättigt, damit sie in das warme Gesamtkonzept passen.
- Rot wird bewusst sparsam eingesetzt (Delete, Fehler), damit es als starkes Signal wahrgenommen wird.

### 1.4 Beispiel-Kombinationen

- **Gold auf Schwarz**: `primary` auf `background` → Headlines, Call-to-Action.
- **Creme auf Schwarz**: `foreground` auf `background` → Fließtexte.
- **Creme auf Dunkelgrau**: `foreground` auf `surface` → Karteninhalte.
- **Weiß auf Rot**: `#FFFFFF` auf `destructive` → Warn-Buttons, Fehlermeldungen.

---

## 2. Typografie

### 2.1 Schriften

- **Display Font:** `Playfair Display`
  - Einsatz: Große Überschriften (H1–H2), markante Seitentitel, wichtige Hero-Claims.
  - Wirkung: Elegant, hochwertig, leicht klassisch – verstärkt den Premium-Charakter.
- **Body Font:** `Lato`
  - Einsatz: Fließtext, Labels, Buttons, kleinere Überschriften (H3–H6).
  - Wirkung: Klare, gut lesbare Sans-Serif-Schrift, neutral und modern.

### 2.2 Hierarchie & Einsatz

| Ebene           | Font             | Beispiel-Größen | Verwendung                               |
|-----------------|------------------|-----------------|------------------------------------------|
| H1              | Playfair Display | 32–40 px        | Seitentitel, große Hero-Überschriften   |
| H2              | Playfair Display | 24–28 px        | Bereiche wie „Zutaten“, „Zubereitung“   |
| H3/H4           | Lato (Bold)      | 18–22 px        | Karten-Titel, Abschnittsüberschriften   |
| Body / Paragraph| Lato (Regular)   | 14–16 px        | Standardtexte, Beschreibungen           |
| Small / Label   | Lato (Medium)    | 12–13 px        | Labels, Badges, Meta-Informationen      |

**Begründung:**
- Kombination aus Serif (für Emotion & Marke) und Sans-Serif (für Lesbarkeit & UI) unterstützt sowohl **Storytelling** als auch **klare Interfaces**.
- Wiederkehrende Größen und Gewichtungen sorgen für konsistente Hierarchie auf allen Seiten.

### 2.3 Einbindung im Projekt

In `index.html` (oder einem globalen Stylesheet) können die Fonts z.B. über Google Fonts eingebunden werden:

```html
<link rel="preconnect" href="https://fonts.googleapis.com" />
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
<link
  href="https://fonts.googleapis.com/css2?family=Lato:wght@300;400;500;700&family=Playfair+Display:wght@400;500;600;700;800&display=swap"
  rel="stylesheet"
/>
```

In `styles/globals.css` können die Basis-Fonts gesetzt werden:

```css
:root {
  --font-display: 'Playfair Display', system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
  --font-body: 'Lato', system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
}

body {
  font-family: var(--font-body);
  color: #f5eada; /* foreground */
  background-color: #0b0b0d; /* background */
}

.h-display {
  font-family: var(--font-display);
}
```

Auf Komponentenebene können Überschriften mit einer Utility-Klasse (z.B. `className="h-display text-3xl"`) an die Display-Font gebunden werden.

---

## 3. Logo-Guidelines

> Hinweis: Das konkrete Logo-Design (Form, Icon) wird hier nicht als Grafik definiert, sondern über Rahmenbedingungen beschrieben, sodass es konsistent angewendet werden kann.

### 3.1 Grundidee

- Wortmarke **„Lumina Events“** / **„Lumina Kitchen“** (je nach finalem Namen).
- Schrift: `Playfair Display` in einer stärkeren Gewichtung (z.B. 600 oder 700).
- Farbschema: `primary` Gold auf `background` Schwarz.

### 3.2 Varianten

1. **Primäre Logovariante (Hell auf Dunkel)**
   - Text: Gold (`#F4C15A`) auf Hintergrund `#0B0B0D`.
   - Einsatz: Navigation, Header, Footer, Splash-Screen.

2. **Sekundäre Variante (Dunkel auf Hell)**
   - Text: Dunkelgrau (`#17171B`) auf hellem Hintergrund (`#F5EADA` oder Weiß).
   - Einsatz: Dokumente, hellere Sektionen, Druck.

### 3.3 Schutzzone & Mindestgrößen

- Rund um das Logo sollte mindestens die Höhe des Buchstabens „L“ als freier Raum bleiben.
- Mindestbreite für digitale Anwendungen: ca. 120 px, damit `Playfair Display` klar lesbar bleibt.

---

## 4. Nutzung der Tokens im Code

Um das Designsystem im Code verfügbar zu machen, können in `styles/globals.css` CSS-Variablen definiert werden:

```css
:root {
  /* Farben */
  --color-primary: #f4c15a;
  --color-primary-secondary: #d7a94b;
  --color-background: #0b0b0d;
  --color-surface: #17171b;
  --color-muted: #2a2a30;
  --color-border: #2f2f36;
  --color-foreground: #f5eada;
  --color-foreground-muted: #c9bba4;
  --color-success: #4caf50;
  --color-warning: #fbc02d;
  --color-destructive: #e53935;

  /* Typografie */
  --font-display: 'Playfair Display', system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
  --font-body: 'Lato', system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
}

body {
  background-color: var(--color-background);
  color: var(--color-foreground);
  font-family: var(--font-body);
}

.h-display {
  font-family: var(--font-display);
}

.card {
  background-color: var(--color-surface);
  border: 1px solid var(--color-border);
  border-radius: 1rem;
}

.button-primary {
  background-color: var(--color-primary);
  color: #000;
}

.button-primary:hover {
  box-shadow: 0 0 0 2px rgba(244, 193, 90, 0.4);
}
```

Diese Klassen und Variablen kannst du direkt in deinen React-Komponenten verwenden, um die in diesem Dokument definierten Farben und Schriften konsistent auf der Website einzusetzen.
