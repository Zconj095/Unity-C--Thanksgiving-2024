# ShaderDatabase

## Overview
The `ShaderDatabase` script is a Unity `ScriptableObject` that manages a collection of shader metadata. This script allows developers to easily add, remove, and find shaders by their names, facilitating better organization and management of shader assets within a Unity project. It serves as a centralized repository for shader information that can be reused across different parts of the codebase.

## Variables
- `shaders`: A `List<ShaderMetadata>` that stores instances of `ShaderMetadata`. This list holds all the shader data that can be managed through the functions provided in this script.

## Functions
- `AddShader(ShaderMetadata shaderData)`: This function adds a new `ShaderMetadata` instance to the `shaders` list if it is not already present. This ensures that duplicate entries are not created in the database.

- `RemoveShader(ShaderMetadata shaderData)`: This function removes a specified `ShaderMetadata` instance from the `shaders` list if it exists. This allows for dynamic management of shader data by removing unnecessary or outdated shaders.

- `FindShaderByName(string name)`: This function searches the `shaders` list for a `ShaderMetadata` instance that matches the given shader name. It returns the corresponding `ShaderMetadata` if found, or `null` if no match exists. This is useful for quickly retrieving shader data based on user-defined names.