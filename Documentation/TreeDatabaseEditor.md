# TreeDatabaseEditor

## Overview
The `TreeDatabaseEditor` script is a custom editor for the `TreeDatabase` class in Unity. It provides a user interface in the Unity Editor that allows users to manage a collection of tree metadata. The main function of this script is to enable searching for trees by name, adding new tree metadata, saving the database, and displaying stored trees with options to select or remove them. This custom editor enhances the usability of the `TreeDatabase` class by providing an intuitive interface for users to interact with tree data.

## Variables
- `searchFilter`: A string variable that holds the current input for searching trees by name. It is updated as the user types in the search field.
- `searchTag`: A string variable that is declared but not used in this script. It may be intended for future functionality related to tagging trees.

## Functions
- `OnInspectorGUI()`: This is an overridden method from the `Editor` class that defines how the custom editor interface is rendered in the Unity Inspector. It includes:
  - A label for the editor.
  - A search field for filtering trees by name.
  - A button to execute the search and log found trees to the console.
  - A button to add new tree metadata, which creates a new instance of `TreeMetadata`, saves it as an asset, and adds it to the `TreeDatabase`.
  - A button to save the current state of the `TreeDatabase`.
  - A section that displays all stored trees, including their names and associated files, with options to select or remove each tree from the database.