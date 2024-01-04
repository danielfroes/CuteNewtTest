# Project Overview

### Controls

-   **WASD/Arrow keys** ⇒ Movement
-   **E/Spacebar** ⇒ Climb

### Settings

The primary map generation settings reside within the *MapGenerator* component of the *DemoController* GameObject. Here, not only can you adjust the map size, but you can also modify all layer generation settings.

In addition to hierarchy-based changes, you can access individual layer settings by accessing their scriptable objects, located at Assets/MapConfigurations/.

### Map Generation

The map generates each time the "play" button is pressed in the main scene or when clicking the "Regenerate Map" button in the HUD.

Furthermore, you can generate maps during editing time via the Map Generator component inspector by clicking "Generate Map," allowing for faster testing of new configurations.

### Key Scripts

The main script of the project is the Map Generator, which serves as the top-level abstraction for all procedural generation.

Two other noteworthy scripts are the PlayerController, responsible for player movement, and the MapGenerationDemoController, which serves as the project initializer.

### Notes

-   To create more interactive editors without consuming excessive development time, I've added the Naughty Attributes package to the project. 
-   To streamline the development of the project, I've made minor modifications to the tilemap sprites, to be able to treat them all as 24x24-pixel tiles.