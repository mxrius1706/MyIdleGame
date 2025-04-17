💤 Unity Idle Game – In Entwicklung
Ein 2D Idle Game in Unity – aktuell noch in der Entwicklungsphase. Das Spiel legt den Fokus auf automatisierte Spielmechaniken, passives Fortschreiten und modular erweiterbare Systeme.

🧪 Aktueller Entwicklungsstand
⚠️ Dieses Projekt befindet sich in aktiver Entwicklung. Ziel ist ein solides Fundament für ein modulares, passives Spielsystem, das sich leicht erweitern lässt.

✅ Bereits umgesetzte Features
⏳ Idle-Gameplay: Fortschritt über Zeit, auch ohne aktive Eingaben

🧠 Verhaltensbasierte NPCs: Gegner mit einfacher Logik & automatisierter Bewegung

🎞️ Animator-gesteuerte Zustände über StateMachineBehaviours

🏞️ Parallax-Hintergründe für mehr optische Tiefe

🔍 Ziel- & Kontakt-Erkennung über Raycasts (TouchingDirections)

🎮 Neues Unity Input System für moderne, zukunftssichere Steuerung

💥 Schadenssystem: Sowohl Gegner als auch der Spieler können jetzt Schaden nehmen und sterben

🧩 Geplante Features
 UI & Progress-System für Ressourcen und Spielfortschritt

 Save/Load-Mechanismus zur Speicherung des Spielfortschritts

 Idle-Currency & Upgrades für passiven Spielverlauf

 Erweiterte NPC-Interaktionen und KI für komplexeres Verhalten

🧱 Projektstruktur
bash
Kopieren
Bearbeiten
Assets/
├── Scripts/
│   ├── Player/
│   │   ├── PlayerController.cs         # Steuerung & Eingaben
│   │   ├── TouchingDirections.cs       # Erkennung von Umgebungskontakt
│   │   ├── AnimationStrings.cs         # Konstanten für Animationen
│   │   ├── HealthSystem.cs            # Schadenssystem für Spieler
│   ├── Enemies/
│   │   ├── BOD.cs                      # Gegnerverhalten mit Bewegung & Zielsuche
│   │   ├── EnemyHealth.cs             # Schadenssystem für Gegner
│   │   └── DetectionZone.cs           # Kollisionsbasierte Zielerkennung
│   ├── Visuals/
│   │   └── ParallaxEffect.cs          # Parallaxeffekte für Hintergrundebenen
│   └── Animator/
│       └── SetBooleanBehaviour.cs     # Animation Bool-Steuerung per StateMachine

📌 Hinweise
Dieses Projekt ist aktuell ein Solo-Projekt für Lern- und Übungszwecke.

Kommentare & Struktur sind teilweise auf Lesbarkeit für andere Unity-Dev-Einsteiger ausgelegt.

Kann gerne weiterentwickelt oder geforkt werden!

---

### Erklärung der neuen Features:

- **Schadenssystem**: Spieler und Gegner haben nun Gesundheitswerte, die durch Angriffe oder andere Ereignisse verringert werden. Wenn die Gesundheit auf null sinkt, sterben die Charaktere. Diese Funktion ist in den Skripten `HealthSystem.cs` für den Spieler und `EnemyHealth.cs` für die Gegner implementiert.

---
