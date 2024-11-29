# FlowerDatabaseEditor

## Overview
The `FlowerDatabaseEditor` script is a custom Unity editor for managing a flower database within the Unity Editor. It allows users to search for flowers by name, add new flower metadata, save the database, and view existing flowers. This script enhances the usability of the `FlowerDatabase` class by providing a user-friendly interface for managing flower data.

## Variables
- `searchFilter`: A string that stores the current search term used to filter flowers by their names in the database.
- `searchTag`: A string that appears to be unused in the current implementation but may be intended for future functionality related to tagging flowers.

## Functions
- `OnInspectorGUI()`: This method is overridden to create the custom inspector GUI. It handles the following functionalities:
  - Displays a label for the flower database.
  - Provides a text field for searching flowers by name using the `searchFilter`.
  - Implements a button that, when clicked, searches through the `flowers` collection in the `FlowerDatabase` for names that contain the `searchFilter` and logs the found flowers to the console.
  - Offers a button to add new flower metadata, which creates a new instance of `FlowerMetadata`, saves it as an asset, and adds it to the `FlowerDatabase`.
  - Includes a button to save changes made to the `FlowerDatabase`.
  - Displays a list of stored flowers, showing each flower's name and image (if available), along with buttons to select or remove the flower from the database.