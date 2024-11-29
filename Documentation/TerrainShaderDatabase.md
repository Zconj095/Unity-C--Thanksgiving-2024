# TerrainShaderDatabase

## Overview
The `TerrainShaderDatabase` script is a Unity `ScriptableObject` that serves as a centralized repository for managing terrain shaders in a game or application. It allows developers to add, remove, and search for terrain shaders based on their metadata. This script is essential for organizing and efficiently accessing shader data, which can be utilized throughout the codebase to enhance the visual aspects of terrain rendering.

## Variables
- `shaders`: A `List<TerrainShaderMetadata>` that holds the collection of terrain shaders. Each entry in this list represents a unique shader along with its associated metadata.

## Functions
- `AddShader(TerrainShaderMetadata shaderData)`: This method takes a `TerrainShaderMetadata` object as an argument and adds it to the `shaders` list if it is not already present. This ensures that there are no duplicate shaders in the database.

- `RemoveShader(TerrainShaderMetadata shaderData)`: This method removes a specified `TerrainShaderMetadata` object from the `shaders` list if it exists. This allows for the dynamic management of shader data.

- `FindShaderByName(string name)`: This method searches for a terrain shader in the `shaders` list based on the provided shader name. It returns the matching `TerrainShaderMetadata` object if found, or `null` if no match exists.

- `FindShadersByTag(string tag)`: This method searches for all terrain shaders that contain a specified tag. It returns a list of `TerrainShaderMetadata` objects that match the given tag, allowing for bulk retrieval of shaders based on their associated tags.