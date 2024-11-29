# RockDatabase

## Overview
The `RockDatabase` script is a Unity ScriptableObject that serves as a repository for managing a collection of rock metadata. It allows for the addition, removal, and retrieval of rocks based on various criteria such as name, tags, and type. This script fits into the larger codebase by providing a structured way to handle rock data, making it easier for other components of the game to access and manipulate rock-related information.

## Variables
- **rocks**: A list of `RockMetadata` objects that holds all the rock entries in the database. This list is initialized as an empty list.

## Functions
- **AddRock(RockMetadata rock)**: Adds a new `RockMetadata` object to the `rocks` list if it is not already present. This prevents duplicate entries in the database.

- **RemoveRock(RockMetadata rock)**: Removes a specified `RockMetadata` object from the `rocks` list if it exists. This allows for dynamic management of the rock collection.

- **FindRockByName(string name)**: Searches for a `RockMetadata` object in the `rocks` list by its name. Returns the first rock that matches the specified name or `null` if no match is found.

- **FindRocksByTag(string tag)**: Retrieves a list of `RockMetadata` objects that contain a specified tag. It returns all rocks that have the tag in their tags array.

- **FindRocksByType(string type)**: Returns a list of `RockMetadata` objects that match a specified rock type. This function helps filter rocks based on their classification.