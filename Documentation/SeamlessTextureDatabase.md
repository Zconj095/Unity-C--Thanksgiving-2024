# SeamlessTextureDatabase

## Overview
The `SeamlessTextureDatabase` script is a Unity `ScriptableObject` that serves as a centralized repository for managing seamless texture assets within a Unity project. This script allows developers to add, remove, and search for textures based on their names or associated tags. By utilizing this database, developers can efficiently organize and access textures, improving workflow and asset management in game development.

## Variables
- `textures`: A `List<SeamlessTextureMetadata>` that holds all the seamless texture metadata objects. This list is the core of the database, allowing for the storage and retrieval of texture information.

## Functions
- `AddTexture(SeamlessTextureMetadata texture)`: This method adds a new texture to the `textures` list if it is not already present. It ensures that duplicate textures are not added to the database.

- `RemoveTexture(SeamlessTextureMetadata texture)`: This method removes a specified texture from the `textures` list if it exists. It helps in managing the database by allowing the removal of textures that are no longer needed.

- `FindTextureByName(string name)`: This method searches the `textures` list for a texture with a matching name and returns the corresponding `SeamlessTextureMetadata` object. If no texture is found, it returns `null`.

- `FindTexturesByTag(string tag)`: This method searches the `textures` list for all textures that contain a specified tag and returns a list of matching `SeamlessTextureMetadata` objects. This is useful for categorizing and retrieving textures based on tags for easier management.