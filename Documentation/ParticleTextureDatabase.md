# ParticleTextureDatabase

## Overview
The `ParticleTextureDatabase` script is a Unity scriptable object that manages a collection of particle textures, allowing for the addition, removal, and retrieval of textures based on specific criteria. This script is essential for organizing and accessing particle textures efficiently within the Unity game development environment. It serves as a centralized repository for metadata about particle textures, making it easier for developers to manage visual effects in their projects.

## Variables
- `textures`: This is a list that holds instances of `ParticleTextureMetadata`. It stores all the particle textures that have been added to the database.

## Functions
- `AddTexture(ParticleTextureMetadata texture)`: This function adds a new texture to the `textures` list if it is not already present. It prevents duplicates in the database.

- `RemoveTexture(ParticleTextureMetadata texture)`: This function removes a specified texture from the `textures` list if it exists. This allows for dynamic management of the texture collection.

- `FindTextureByName(string name)`: This function searches for a texture in the `textures` list by its name. It returns the corresponding `ParticleTextureMetadata` object if found, or null if not.

- `FindTexturesByTag(string tag)`: This function retrieves a list of textures that have a specific tag associated with them. It returns all matching `ParticleTextureMetadata` objects from the `textures` list, facilitating the organization of textures by tags.