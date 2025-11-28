import React from 'react';

export function DesignSystemPage() {
  const colors = [
    { name: 'Primary', token: '--color-primary', hex: '#F4C15A', usage: 'Buttons, Links, Icons, aktive States' },
    { name: 'Secondary', token: '--color-primary-secondary', hex: '#D7A94B', usage: 'Alternative Buttons, Badges, Hover' },
    { name: 'Background', token: '--color-background', hex: '#0B0B0D', usage: 'Hintergrund (dunkle Sektionen)' },
    { name: 'Surface', token: '--color-surface', hex: '#17171B', usage: 'Cards, Container' },
    { name: 'Muted', token: '--color-muted', hex: '#2A2A30', usage: 'Gedämpfte Bereiche, Inputs' },
    { name: 'Border', token: '--color-border', hex: '#2F2F36', usage: 'Umrandungen, Divider' },
    { name: 'Foreground', token: '--color-foreground', hex: '#F5EADA', usage: 'Haupttext auf Dunkel' },
    { name: 'Foreground Muted', token: '--color-foreground-muted', hex: '#C9BBA4', usage: 'Sekundärtext, Labels' },
    { name: 'Destructive', token: '--color-destructive', hex: '#E53935', usage: 'Fehler, destruktive Aktionen' },
    { name: 'Teal Accent', token: '--color-chart-2', hex: '#00bcd4', usage: 'Akzentfarbe für Links, Badges, Weekplanner-Details' },
  ];

  const fonts = [
    {
      name: 'Playfair Display',
      role: 'Display Font (Überschriften)',
      className: 'font-[var(--font-display)]',
      sample: 'Unvergessliche Events erleben',
    },
    {
      name: 'Lato',
      role: 'Body Font (Fließtext, UI)',
      className: 'font-[var(--font-body)]',
      sample: 'Entdecke exklusive Rezepte, plane deine Woche und speichere deine Favoriten.',
    },
  ];

  return (
    <div className="min-h-screen bg-[var(--color-background)] text-[var(--color-foreground)]">
      <div className="max-w-6xl mx-auto px-6 py-10">
        <h1 className="text-3xl font-semibold mb-6" style={{ fontFamily: 'var(--font-display)' }}>
          Designsystem
        </h1>
        <p className="text-sm text-[var(--color-foreground-muted)] mb-8 max-w-2xl">
          Auf dieser Seite kannst du alle zentralen Farben und Schriften des Projekts live sehen. Die Werte
          entsprechen den Tokens im Dokument <code>guidelines/DesignSystem.md</code>.
        </p>

        {/* Farben */}
        <section className="mb-10">
          <h2 className="text-xl font-semibold mb-4" style={{ fontFamily: 'var(--font-display)' }}>
            Farben
          </h2>
          <div className="overflow-hidden rounded-2xl border border-[var(--color-border)] bg-[var(--color-surface)]">
            <div className="grid grid-cols-1 md:grid-cols-3 text-xs font-medium text-[var(--color-foreground-muted)] bg-black/40 px-4 py-2">
              <div>Farbe</div>
              <div>Verwendung</div>
              <div>Token / Hex</div>
            </div>
            {colors.map((c) => (
              <div
                key={c.token}
                className="grid grid-cols-1 md:grid-cols-3 items-center px-4 py-3 border-t border-[var(--color-border)] text-sm"
              >
                <div className="flex items-center gap-3 mb-2 md:mb-0">
                  <div
                    className="h-8 w-8 rounded-md border border-black/40 shadow-sm"
                    style={{ backgroundColor: `var(${c.token})` }}
                  />
                  <div>
                    <div className="font-medium text-[var(--color-foreground)]">{c.name}</div>
                  </div>
                </div>
                <div className="text-[var(--color-foreground-muted)] mb-2 md:mb-0">{c.usage}</div>
                <div className="font-mono text-[11px] text-[var(--color-foreground-muted)]">
                  {c.token} · {c.hex}
                </div>
              </div>
            ))}
          </div>
        </section>

        {/* Typografie */}
        <section>
          <h2 className="text-xl font-semibold mb-4" style={{ fontFamily: 'var(--font-display)' }}>
            Typografie
          </h2>
          <div className="grid gap-4 md:grid-cols-2">
            {fonts.map((font) => (
              <div
                key={font.name}
                className="rounded-2xl border border-[var(--color-border)] bg-[var(--color-surface)] p-5"
              >
                <div className="text-xs text-[var(--color-foreground-muted)] mb-1">{font.role}</div>
                <div className="text-lg font-semibold mb-2" style={{ fontFamily: font.name }}>
                  {font.name}
                </div>
                <p className="text-sm" style={{ fontFamily: font.name }}>
                  {font.sample}
                </p>
              </div>
            ))}
          </div>
        </section>
      </div>
    </div>
  );
}
