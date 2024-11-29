# TerrainNormalMapDatabaseEditor

## Overview
The `TerrainNormalMapDatabaseEditor` script is a custom editor for the `TerrainNormalMapDatabase` class in Unity. It provides a user interface in the Unity Editor that allows users to manage a collection of terrain normal maps. This script facilitates searching for normal maps by name, adding new normal map metadata, saving the database, and displaying the stored normal maps with options to select or remove them. It enhances the workflow for developers working with terrain normal maps by providing easy access to the functionalities of the `TerrainNormalMapDatabase`.

## Variables
- `searchFilter`: A string used to hold the current input from the search field, allowing users to filter normal maps by name during the search operation.
- `searchTag`: A string intended for tagging purposes (though not used in the current implementation).

## Functions
- `OnInspectorGUI()`: This is an overridden method from the `Editor` class that defines the custom inspector GUI for the `TerrainNormalMapDatabase`. It performs the following tasks:
  - Displays a label for the database.
  - Provides a search field for users to input a name to filter normal maps.
  - Implements a search button that, when clicked, searches through the `normalMaps` list in the `TerrainNormalMapDatabase` and logs any matches found.
  - Offers a button to add new normal map metadata, creating a new instance of `TerrainNormalMapMetadata` and adding it to the database.
  - Includes a button to save changes made to the database.
  - Displays a list of stored normal maps, allowing users to select or remove them with corresponding buttons.