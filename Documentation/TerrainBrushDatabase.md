# TerrainBrushDatabase

## Overview
The `TerrainBrushDatabase` script is a Unity `ScriptableObject` designed to manage a collection of terrain brushes used in a game development environment. It serves as a centralized repository for storing, adding, removing, and retrieving terrain brush metadata. This functionality is essential for organizing and manipulating brushes that define how terrain is shaped and textured, thereby enhancing the efficiency and flexibility of the development process.

## Variables
- `brushes`: A list that stores instances of `TerrainBrushMetadata`. This collection holds all the terrain brushes available in the database.

## Functions
- `AddBrush(TerrainBrushMetadata brush)`: This method adds a new terrain brush to the `brushes` list if it is not already present. It ensures that each brush in the database is unique, preventing duplicates.

- `RemoveBrush(TerrainBrushMetadata brush)`: This method removes a specified terrain brush from the `brushes` list if it exists. It helps in managing the brushes by allowing developers to clean up unused or outdated brushes.

- `FindBrushByName(string name)`: This method searches for and returns a terrain brush from the `brushes` list that matches the provided name. It facilitates quick access to specific brushes based on their identifiers.

- `FindBrushesByTag(string tag)`: This method retrieves a list of terrain brushes that contain a specific tag. It allows for filtering brushes based on tags, making it easier to categorize and find brushes that share common attributes.