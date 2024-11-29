# SeamlessTerrainTextureDatabase

## Overview
The `SeamlessTerrainTextureDatabase` is a ScriptableObject designed for managing a collection of terrain textures in a Unity project. It allows developers to add, remove, and search for textures based on various criteria such as name, biome type, and tags. This script fits into the broader codebase by providing a centralized repository for texture metadata, which can be utilized by other components of the game to create seamless and diverse terrain visuals.

## Variables
- `textures`: A list of `SeamlessTerrainTextureMetadata` objects that holds all the textures available in the database. This list is initialized as an empty list and is used to manage the terrain textures.

## Functions
- `AddTexture(SeamlessTerrainTextureMetadata texture)`: This function adds a new texture to the `textures` list if it is not already present. It ensures that the same texture is not added multiple times.

- `RemoveTexture(SeamlessTerrainTextureMetadata texture)`: This function removes a specified texture from the `textures` list, if it exists. It helps in managing the collection by allowing textures to be deleted when they are no longer needed.

- `FindTextureByName(string name)`: This function searches for a texture in the `textures` list by its name. It returns the `SeamlessTerrainTextureMetadata` object that matches the given name, or `null` if no match is found.

- `FindTexturesByBiome(string biomeType)`: This function retrieves a list of textures that are associated with a specific biome type. It returns all `SeamlessTerrainTextureMetadata` objects whose `biomeType` matches the provided parameter.

- `FindTexturesByTag(string tag)`: This function searches for textures that contain a specified tag. It returns a list of `SeamlessTerrainTextureMetadata` objects that have the given tag in their `tags` array.