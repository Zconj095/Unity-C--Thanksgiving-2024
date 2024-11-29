# TreeMetadata

## Overview
The `TreeMetadata` script is a Unity ScriptableObject that serves as a data structure for storing information about different types of trees within a tree database. This script allows developers to create and manage tree assets efficiently, providing essential metadata such as the tree's name, type, and associated visual representation. By utilizing this script, developers can easily organize and filter tree assets in their projects, enhancing the overall management of tree-related resources in the codebase.

## Variables
- `treeName`: A string that holds the name of the tree. This is used for identification purposes within the database.
- `treeFile`: A `Texture2D` reference that points to the 2D visual representation of the tree. This asset is used to display the tree in the game or application.
- `resolution`: A `Vector2Int` that specifies the resolution of the tree asset. This provides information on the dimensions of the tree's visual representation.
- `treeType`: A string that indicates the type of the tree, such as "Oak", "Pine", or "Palm". This helps categorize the tree assets.
- `tags`: An array of strings that contains tags for filtering the trees, such as "Winter", "Tropical", or "Evergreen". This allows for easier searching and organization of tree assets based on their characteristics.
- `description`: A string that offers an optional description of the tree. This can provide additional context or information about the tree for developers or users.

## Functions
The `TreeMetadata` class does not contain any specific functions beyond the default behavior of a ScriptableObject. Its primary purpose is to define and store the metadata for tree assets in a structured way, enabling easy access and management of tree-related information within the Unity environment.