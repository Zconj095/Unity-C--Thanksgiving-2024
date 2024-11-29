# TextureApplicator

## Overview
The `TextureApplicator` class is responsible for applying textures to a specified renderer in a Unity game environment. It interacts with a `SeamlessTextureDatabase` to retrieve textures either by name or by tag. This script is useful for dynamically changing the appearance of game objects based on user input or game events, enhancing the visual diversity of the game.

## Variables
- `public SeamlessTextureDatabase textureDatabase`: A reference to the texture database that contains a collection of textures. This allows the `TextureApplicator` to search for and retrieve textures.
- `public Renderer targetRenderer`: A reference to the renderer component of the game object that will have its texture changed. This is where the selected texture will be applied.

## Functions
- `public void ApplyTexture(string textureName)`: This function takes a string parameter `textureName`, searches the `textureDatabase` for a texture with that name, and applies it to the `targetRenderer`. If the texture is not found, it logs a warning message.

- `public void ApplyRandomTextureByTag(string tag)`: This function takes a string parameter `tag`, retrieves a list of textures from the `textureDatabase` that are associated with the specified tag, and randomly selects one to apply to the `targetRenderer`. If no textures are found for the given tag, it logs a warning message.