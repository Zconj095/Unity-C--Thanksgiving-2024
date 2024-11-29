# CrystalApplicator

## Overview
The `CrystalApplicator` class is a Unity script designed to apply crystal textures or sprites to target game objects. It interacts with a `CrystalDatabase` to retrieve crystal data based on the crystal's name or associated tags. This functionality allows for dynamic visual changes in the game, enhancing the appearance of objects by applying crystal-like textures or sprites.

## Variables
- `public CrystalDatabase crystalDatabase`: A reference to the database that contains information about various crystals, including their textures and properties.
- `public SpriteRenderer targetSprite`: A reference to a `SpriteRenderer` component that is used to display 2D sprites on game objects. This variable is used when applying a crystal sprite.
- `public GameObject targetObject`: A reference to a GameObject that may have a material texture. This variable is used when applying a crystal texture to 3D objects.

## Functions
- `public void ApplyCrystal(string crystalName)`: This function takes a string parameter `crystalName`, searches for the corresponding crystal in the `crystalDatabase`, and applies its sprite or texture to the specified target. If the crystal is found and has a valid texture file, it creates a new sprite and assigns it to the `targetSprite`. If `targetObject` is provided, it updates its material's main texture with the crystal's texture. If the crystal is not found, it logs a warning message.

- `public void ApplyRandomCrystalByTag(string tag)`: This function takes a string parameter `tag` and retrieves a list of crystals associated with that tag from the `crystalDatabase`. If there are crystals available, it selects one at random and calls `ApplyCrystal` with the selected crystal's name. If no crystals are found with the specified tag, it logs a warning message.