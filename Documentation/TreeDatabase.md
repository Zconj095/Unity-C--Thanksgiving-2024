# TreeDatabase Script

## Overview
The `TreeDatabase` script is designed to manage a collection of tree metadata within a Unity project. It allows developers to add, remove, and search for trees based on various attributes such as name, tags, and type. This script serves as a centralized database for tree-related information, facilitating easy access and manipulation of tree data throughout the codebase.

## Variables
- `trees`: A `List<TreeMetadata>` that holds instances of `TreeMetadata`. This list represents the collection of trees stored in the database.

## Functions
- `AddTree(TreeMetadata tree)`: This method adds a new `TreeMetadata` object to the `trees` list if it is not already present. It ensures that there are no duplicate entries in the database.

- `RemoveTree(TreeMetadata tree)`: This method removes a specified `TreeMetadata` object from the `trees` list if it exists. It helps in managing the collection by allowing the removal of trees that are no longer needed.

- `FindTreeByName(string name)`: This method searches for a `TreeMetadata` object in the `trees` list by its name. It returns the first matching tree found or `null` if no match exists.

- `FindTreesByTag(string tag)`: This method retrieves a list of `TreeMetadata` objects that contain a specified tag. It returns all trees that match the given tag, allowing for filtering based on tags.

- `FindTreesByType(string type)`: This method returns a list of `TreeMetadata` objects that match a specified tree type. It facilitates searching for trees based on their type classification.