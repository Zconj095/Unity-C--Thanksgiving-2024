# ShaderDatabaseEditor

## Overview
The `ShaderDatabaseEditor` script is a custom editor for the `ShaderDatabase` class in Unity. Its primary function is to provide a user-friendly interface in the Unity Inspector for managing shaders within a shader database. This script allows users to add new shaders, save the database, and manage existing shaders by selecting or removing them. It enhances the workflow for developers working with shaders by making the management process straightforward and intuitive.

## Variables
- `database`: An instance of the `ShaderDatabase` class, which serves as the target object for the custom editor. This variable allows access to the shader database's properties and methods.

## Functions
- `OnInspectorGUI()`: This method overrides the default inspector GUI for the `ShaderDatabase` class. It handles the rendering of the custom editor's interface, including:
  - Displaying a bold label for the shader database.
  - Providing a button to add a new shader, which creates a new instance of `ShaderMetadata`, saves it as an asset, and adds it to the shader database.
  - Providing a button to save the current state of the shader database.
  - Listing all shaders in the database with options to select or remove each shader. Selecting a shader highlights it in the Unity Editor, while removing a shader deletes it from the database and marks the database as dirty for saving changes. 

This script plays a crucial role in the shader management system, streamlining the process of working with shaders in Unity projects.