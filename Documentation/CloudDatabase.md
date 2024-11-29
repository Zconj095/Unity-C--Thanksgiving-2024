# CloudDatabase Script

## Overview
The `CloudDatabase` script is a Unity ScriptableObject that serves as a container for managing a collection of `CloudMetadata` objects. It provides functionality for adding, removing, and searching for clouds based on various criteria such as name, tags, and types. This script is essential for organizing cloud-related data in a structured manner, making it easier for other parts of the codebase to access and manipulate cloud information.

## Variables
- `public List<CloudMetadata> clouds`: A list that stores instances of `CloudMetadata`. This list represents the collection of clouds managed by the `CloudDatabase`.

## Functions
- `public void AddCloud(CloudMetadata cloud)`: Adds a `CloudMetadata` instance to the `clouds` list if it is not already present. This function ensures that duplicate entries are not added to the database.

- `public void RemoveCloud(CloudMetadata cloud)`: Removes a specified `CloudMetadata` instance from the `clouds` list if it exists. This function allows for the removal of clouds from the database.

- `public CloudMetadata FindCloudByName(string name)`: Searches for a `CloudMetadata` instance in the `clouds` list by its name. It returns the first matching cloud or `null` if no match is found.

- `public List<CloudMetadata> FindCloudsByTag(string tag)`: Retrieves a list of `CloudMetadata` instances from the `clouds` list that contain a specified tag. It returns all matching clouds.

- `public List<CloudMetadata> FindCloudsByType(string type)`: Retrieves a list of `CloudMetadata` instances from the `clouds` list that match a specified cloud type. It returns all matching clouds.