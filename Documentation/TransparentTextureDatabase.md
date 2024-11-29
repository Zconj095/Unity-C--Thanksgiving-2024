# TransparentTextureDatabase

## Overview
The `TransparentTextureDatabase` script is a Unity `ScriptableObject` designed to manage a collection of transparent textures within a game or application. It provides functionality to add, remove, and search for textures based on their names or associated tags. This script fits into the codebase as a centralized database for texture management, allowing for easy access and manipulation of transparent textures, which can be used in various visual elements of a project.

## Variables
- **textures**: A `List<TransparentTextureMetadata>` that holds all the transparent texture metadata objects. Each object contains information about a specific transparent texture, allowing for easy management and retrieval.

## Functions
- **AddTexture(TransparentTextureMetadata texture)**: This method adds a new transparent texture to the `textures` list if it is not already present. It ensures that duplicate entries are not added to the database.

- **RemoveTexture(TransparentTextureMetadata texture)**: This method removes a specified transparent texture from the `textures` list if it exists. It helps in maintaining the integrity of the texture database by allowing for the removal of unused or unnecessary textures.

- **FindTextureByName(string name)**: This method searches for a transparent texture in the `textures` list by its name. It returns the `TransparentTextureMetadata` object that matches the specified name, or `null` if no match is found.

- **FindTexturesByTag(string tag)**: This method retrieves a list of transparent textures that are associated with a specific tag. It returns all `TransparentTextureMetadata` objects from the `textures` list whose tags include the specified tag, facilitating the organization and filtering of textures based on their characteristics.