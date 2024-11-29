# VectorDatabaseManager

## Overview
The `VectorDatabaseManager` script is responsible for managing a collection of `VectorDatabase2` instances within a Unity application. It provides functionality to create new databases and retrieve existing ones based on their index in the list. This script acts as a centralized manager for vector databases, enabling easy access and manipulation of these databases throughout the codebase.

## Variables
- `databases`: A private list that stores instances of `VectorDatabase2`. This list allows for the management of multiple vector databases, enabling the creation and retrieval of databases as needed.

## Functions
- `CreateDatabase(string path)`: This public method takes a string parameter `path`, which specifies the location or identifier for the new database. It creates a new instance of `VectorDatabase2`, adds it to the `databases` list, and returns the newly created database.

- `GetDatabase(int index)`: This public method takes an integer parameter `index`, which specifies the position of the desired database in the `databases` list. It retrieves and returns the `VectorDatabase2` instance located at the specified index.