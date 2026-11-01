# Space Invader

I built a fixed-screen arcade shooter in C# with MonoGame. The player moves beneath a formation of aliens, fires upward, and uses shields that change as they take damage.

## Gameplay

https://github.com/user-attachments/assets/669a397a-6fac-4c1a-a594-69f4d598e7b2

This is the original gameplay clip already attached to the repository.

## Controls

| Key | Action |
| --- | --- |
| Left / Right arrows | Move the ship |
| Space | Fire, with one player bullet active at a time |
| R | Restart after the game ends |
| Escape | Exit |

## Run locally

This repository uses a legacy **MonoGame Windows / .NET Framework 4.5** project. It references MonoGame’s MSBuild imports and locally installed framework assemblies, rather than a modern DesktopGL NuGet setup.

On a Windows machine with compatible Visual Studio, .NET Framework targeting assemblies, and the legacy MonoGame build/content tools:

1. Open `Space Invader.sln`.
2. Check that MonoGame’s imports and assembly references resolve.
3. Build the included `Content/Content.mgcb` assets with the matching content tools.
4. Run the `Space Invader` project using the solution’s x86 configuration.

The checked-in project does not currently build with the macOS .NET SDK: its legacy `MonoGame.Content.Builder.targets` import is missing. I have not migrated it or rerun the Windows application during this documentation pass. The existing clip shows historical gameplay, not a freshly verified cross-platform release.

## Implementation

`Game1.cs` manages the alien formation, game state, and collisions. `Ship.cs` and `Alien.cs` manage firing and movement, while `Shield.cs` handles damage and `Sprite.cs` supplies shared drawing behavior.

The source and content files are in `Space Invader/`. This is an early game project, without automated gameplay tests or a packaged release. A cross-platform port would be a separate change from this documentation update.
