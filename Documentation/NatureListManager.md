# NatureListManager

## Overview
The `NatureListManager` script is designed to manage and display lists of nature-related items, specifically flowers, clouds, and berries. It initializes these lists with example data and provides functionality to display the contents, search for items, and add or remove items from the lists. This script fits within a larger Unity codebase by helping to organize and manage nature-themed data, which can be utilized in various gameplay or simulation scenarios.

## Variables

- `listOfFlowers`: A private list that stores the names of different flowers.
- `listOfClouds`: A private list that stores the names of different types of clouds.
- `listOfBerries`: A private list that stores the names of various berries.

## Functions

- `void Start()`: This is the Unity lifecycle method that initializes the lists with example data and displays their contents when the script starts.

- `private void InitializeFlowers()`: Populates the `listOfFlowers` with predefined flower names such as "Rose", "Tulip", "Daisy", "Sunflower", and "Orchid".

- `private void InitializeClouds()`: Populates the `listOfClouds` with predefined cloud types such as "Cumulus", "Stratus", "Cirrus", "Nimbus", and "Altostratus".

- `private void InitializeBerries()`: Populates the `listOfBerries` with predefined berry names including "Strawberry", "Blueberry", "Raspberry", "Blackberry", and "Goji Berry".

- `private void DisplayList(string category, List<string> items)`: Displays the contents of a specified list in the Unity console, preceded by the category name.

- `public bool SearchInList(string category, string item)`: Searches for a specified item in the list corresponding to the given category. It returns `true` if the item is found and logs a message; otherwise, it returns `false`.

- `private List<string> GetListByCategory(string category)`: Returns the appropriate list based on the provided category name. If the category is not recognized, it logs an error message and returns `null`.

- `public void AddToList(string category, string item)`: Adds a specified item to the relevant list if it does not already exist. It logs a message indicating whether the addition was successful or if the item already exists.

- `public void RemoveFromList(string category, string item)`: Removes a specified item from the relevant list if it exists. It logs a message indicating whether the removal was successful or if the item was not found.