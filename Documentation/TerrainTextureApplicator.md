# TerrainTextureApplicator

## Overview
The `TerrainTextureApplicator` script is designed to manage and apply textures to a terrain in Unity. It interacts with a `SeamlessTerrainTextureDatabase` to retrieve texture data based on specific criteria, such as texture name or biome type. This functionality allows for dynamic texture application, enhancing the visual quality and diversity of terrains within the game environment.

## Variables

- `public SeamlessTerrainTextureDatabase textureDatabase`: 
  This variable holds a reference to the texture database that contains various textures available for application to the terrain.

- `public Terrain terrain`: 
  This variable references the Unity `Terrain` object to which the textures will be applied.

## Functions

- `public void ApplyTexture(string textureName)`: 
  This function takes a string parameter `textureName`, searches for the corresponding texture in the `textureDatabase`, and applies it to the terrain if found. If the texture is not found or does not have a valid file, a warning is logged to the console.

- `public void ApplyRandomTextureByBiome(string biomeType)`: 
  This function accepts a string parameter `biomeType`, retrieves a list of textures associated with that biome from the `textureDatabase`, and randomly selects one to apply to the terrain. If no textures are found for the specified biome, a warning is logged to the console.