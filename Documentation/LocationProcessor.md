# LocationProcessor

## Overview
The `LocationProcessor` script is designed to process Unity GameObjects that are tagged with a specific location identifier. It searches for all GameObjects in the scene with the specified tag, retrieves their components, and logs details about their properties and methods. This script is useful for managing and analyzing location-related objects within a Unity scene, ensuring that developers can easily inspect the attributes and behaviors of these objects.

## Variables

- `locationTag` (string): This public variable allows the user to specify the tag used to filter location-related GameObjects. The default value is "Location". 

## Functions

- `Start()`: This method is called before the first frame update. It checks if the `locationTag` is specified. If not, it logs an error message. If the tag is valid, it logs a message indicating the tag being processed and calls the `ProcessLocations` method.

- `ProcessLocations(string tag)`: This method takes a string parameter representing the tag to search for. It finds all GameObjects tagged with the specified tag. If no objects are found, it logs a warning. For each found object, it logs the object's name and calls the `ProcessObject` method.

- `ProcessObject(GameObject obj)`: This method processes a given GameObject. It retrieves all components attached to the GameObject and logs the type of each component. For each component, it calls the `ProcessComponent` method.

- `ProcessComponent(Component component)`: This method analyzes a given component. It first checks if the component is of type `Transform` and logs its position and rotation if it is. For other components, it uses reflection to retrieve public properties and methods. It logs the values of readable properties and calls parameterless methods, logging their results or any errors encountered during the process.