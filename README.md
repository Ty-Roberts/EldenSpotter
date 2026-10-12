# EldenSpotter

A portable Windows x64 enemy HUD for Elden Ring. Shows the locked enemy’s health, stance-break progress, active status buildup, base damage negation, and status susceptibility. It hides when unlocked and uses a tray menu with separate settings.

## Run

Extract the entire release ZIP into one folder and launch **EldenSpotter.exe**. Keep `enemy-names.json` beside it. Windows .NET Framework 4.x is required; Windows 10/11 generally includes it. Borderless/windowed game mode works best.

Use an existing **offline Elden Ring setup with EAC already disabled**, then select **Start offline tracking** in Settings. This app reads memory only. It does not change the game, disable EAC, inject code, or edit saves. Anti-cheat detection blocks tracking; read-only access does not make online use safe.

Double-click the flame icon in the notification area to open Settings. Closing Settings leaves tracking running; select **Exit** in the tray to quit.

Choose **Position overlay**, drag the sample HUD, and choose **Done positioning**. Normal display passes clicks through. Bottom-right placement and upward expansion are supported. Preferences persist under `%LOCALAPPDATA%/Lordran/EnemyObserver/settings.json` for compatibility with previous builds.

## What the numbers mean

Stance progress fills toward a stance break and falls with recovery. Damage negation uses the exact loaded NPC parameter row: negative values increase damage, positive values reduce it. Those are base values; flat defense, equipment, shields and phase modifiers are not fully modeled.

Status susceptibility displays Yes, Immune or Unknown based on validated resistance data and active effect flags. High thresholds alone do not prove immunity. Unknown means the data could not be confirmed. Sleep duration and special status variants are not predicted.

## Build and check

Run `./build.ps1` in Windows PowerShell. It uses the Windows .NET Framework C# compiler and requires no NuGet packages or external SDK.

```powershell
.\build.ps1
.\src\EldenSpotter.exe --self-test
.\src\EldenSpotter.exe --preview
```

The reader supports explicitly listed modern executable file versions through 2.7.1. Unsupported layouts are rejected. Mock-memory regressions cover target identity, the Fort Faroth bat fallback, stance, damage rows and status flags. Off-screen HUD checks cover hiding, positioning, click-through and anchors. The development build has been tested by its user during normal play; exhaustive coverage of every enemy, boss, mod and game version is not claimed.

## License

AGPL-3.0; see [LICENSE](LICENSE) and [third-party notices](THIRD_PARTY_NOTICES.md). This unofficial fan tool is not affiliated with FromSoftware or Bandai Namco. No official game images or binary game data are bundled.
