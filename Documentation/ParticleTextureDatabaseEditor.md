# ParticleTextureDatabaseEditor

## Overview
The `ParticleTextureDatabaseEditor` script is a custom editor for the `ParticleTextureDatabase` class in Unity. Its primary function is to provide a user-friendly interface within the Unity Editor that allows developers to manage a database of particle textures. This script enables users to search for textures by name, add new texture metadata, save the database, and display the stored textures along with options to select or remove them. This enhances the usability of the `ParticleTextureDatabase` by making it easier to interact with and modify the texture data.

## Variables
- `searchFilter` (string): A string used to filter the textures displayed in the editor based on the user's input. It represents the search term for texture names.
- `searchTag` (string): Although declared, this variable is not utilized in the provided script. It may be intended for future use related to tagging textures.

## Functions
- `OnInspectorGUI()`: This method overrides the default inspector GUI for the `ParticleTextureDatabase`. It is responsible for rendering the custom editor interface, which includes:
  - A label for the database.
  - A text field for users to input a search term to filter textures by name.
  - A button that, when clicked, searches through the database for textures matching the search term and logs the found textures to the console.
  - A button that allows users to add new texture metadata, creating a new instance of `ParticleTextureMetadata`, saving it as an asset, and adding it to the database.
  - A button to save the current state of the database, marking it as dirty to ensure changes are saved.
  - A section that displays all stored textures, including their names and associated files, along with buttons to select or remove each texture from the database.