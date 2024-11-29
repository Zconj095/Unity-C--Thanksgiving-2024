# BushApplicator

## Overview
The `BushApplicator` script is responsible for applying bush visuals to game objects in a Unity environment. It interfaces with a `BushDatabase` to retrieve bush data based on a provided name or tag. This allows for dynamic visual changes to either 2D sprites or 3D objects with material textures. The script is designed to enhance the visual representation of bushes in the game, making it easy to apply different bush graphics based on game logic.

## Variables
- **bushDatabase**: An instance of `BushDatabase` that contains information about various bushes available in the game. This is used to look up bush data based on names or tags.
  
- **targetSprite**: A reference to a `SpriteRenderer` component for 2D sprites. This is where the bush sprite will be applied if the target is a 2D object.

- **targetObject**: A reference to a `GameObject` that uses material textures. This is where the bush texture will be applied if the target is a 3D object.

## Functions
- **ApplyBush(string bushName)**: 
  - This function takes the name of a bush as a parameter, searches for it in the `bushDatabase`, and applies the corresponding bush sprite or texture to the specified target (either a 2D sprite or a 3D object). If the bush is not found, it logs a warning message.

- **ApplyRandomBushByTag(string tag)**: 
  - This function searches for bushes in the `bushDatabase` that match a specified tag. If any bushes are found, it selects one at random and applies it using the `ApplyBush` function. If no bushes are found with the given tag, it logs a warning message.