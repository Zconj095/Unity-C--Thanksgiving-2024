# TransparentTextureDatabaseEditor

## Overview
The `TransparentTextureDatabaseEditor` script is a custom editor for the `TransparentTextureDatabase` class in Unity. It provides a user interface within the Unity Editor to manage and manipulate a collection of transparent textures. This script allows users to search for textures by name, add new texture metadata, save the database, and display stored textures with options to select or remove them. It enhances the usability of the `TransparentTextureDatabase` by providing a visual interface for developers and artists to manage textures more effectively.

## Variables
- `searchFilter`: A string that holds the current search term used to filter textures by their names.
- `searchTag`: A string that is declared but not used in the current implementation. It may be intended for future use to filter textures by tags.

## Functions
- `OnInspectorGUI()`: This method overrides the default inspector GUI for the `TransparentTextureDatabase`. It constructs the custom editor interface that includes:
  - A label for the database.
  - A text field for searching textures by name.
  - A button to execute the search, which logs found textures to the console.
  - A button to add new texture metadata, creating an instance of `TransparentTextureMetadata`, saving it as an asset, and adding it to the database.
  - A button to save the database, marking it as dirty to ensure changes are saved.
  - A display of stored textures, showing each texture's name and file, with options to select or remove the texture from the database.