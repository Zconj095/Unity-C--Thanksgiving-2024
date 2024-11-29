# AnimationDatabaseEditor

## Overview
The `AnimationDatabaseEditor` script is a custom Unity Editor that provides a user interface for managing an `AnimationDatabase`. It allows users to search for animations by name, add new animation metadata, save the database, and display the stored animations. This editor script enhances the usability of the `AnimationDatabase` by providing a visual representation and interaction capabilities within the Unity Editor.

## Variables
- `searchFilter`: A string that stores the current input for searching animations by name.
- `searchByTag`: A string that can be used to filter animations by tags (currently not utilized in the script).

## Functions
- `OnInspectorGUI()`: This is the main function that draws the custom inspector GUI for the `AnimationDatabase`. It performs the following tasks:
  - Displays a label for the Animation Database.
  - Provides a text field for users to input a search term to find animations by name.
  - Contains a button that, when clicked, searches through the `animations` collection in the `AnimationDatabase` and logs the names of animations that match the search term.
  - Includes a button to add new animation metadata, which creates a new instance of `AnimationMetadata`, saves it as an asset, and adds it to the `AnimationDatabase`.
  - Features a button to save any changes made to the `AnimationDatabase`.
  - Displays a list of stored animations with options to select or remove each animation. When the "Select" button is clicked, the corresponding animation is highlighted in the Unity Editor, and when the "Remove" button is clicked, the animation is removed from the database and marked as dirty for saving changes.