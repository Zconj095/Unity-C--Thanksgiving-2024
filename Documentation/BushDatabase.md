# BushDatabase

## Overview
The `BushDatabase` script is a Unity `ScriptableObject` that serves as a centralized repository for managing `BushMetadata` objects. This database allows for the addition, removal, and retrieval of bush metadata, making it easier to manage various types of bushes and their associated properties within a game or application. It fits within the broader codebase by providing a structured way to handle bush-related data, promoting organization and efficiency in accessing this information.

## Variables
- `bushes`: A `List<BushMetadata>` that stores all the bush metadata objects. This list is used to keep track of the different bushes available in the database.

## Functions
- `AddBush(BushMetadata bush)`: This function takes a `BushMetadata` object as a parameter and adds it to the `bushes` list if it is not already present. This helps prevent duplicates in the database.

- `RemoveBush(BushMetadata bush)`: This function removes a specified `BushMetadata` object from the `bushes` list if it exists. This allows for the dynamic management of the bush database.

- `FindBushByName(string name)`: This function searches for a bush in the `bushes` list by its name. It returns the corresponding `BushMetadata` object if found, or `null` if there is no match.

- `FindBushesByTag(string tag)`: This function retrieves a list of `BushMetadata` objects that contain a specific tag. It returns a list of all matching bushes, allowing for filtering based on tags.

- `FindBushesByType(string type)`: This function returns a list of `BushMetadata` objects that match a specified bush type. It allows for categorization and retrieval based on the type of bush.