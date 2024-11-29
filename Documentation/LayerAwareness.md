# LayerAwareness

## Overview
The `LayerAwareness` class is a Unity script designed to interact with game objects based on their layers. It provides functionality to gather and describe objects within a specified layer, allowing developers to filter and manage game objects effectively in a 3D environment. This script is particularly useful in scenarios where layer-based collision detection or object management is necessary, enhancing the modularity and organization of the game’s architecture.

## Variables
- **None**: This class does not define any instance variables. All data is handled within the methods.

## Functions

### GetObjectsByLayer
```csharp
public List<GameObject> GetObjectsByLayer(int layer, float radius, Vector3 position)
```
- **Description**: This function retrieves a list of game objects that belong to a specified layer within a given radius from a defined position. It uses a physics overlap sphere to detect colliders and filters them based on the specified layer.
- **Parameters**:
  - `int layer`: The layer number to filter objects by.
  - `float radius`: The radius within which to search for objects.
  - `Vector3 position`: The center point from which to search for objects.
- **Returns**: A list of `GameObject` instances that are within the specified layer and radius.

### DescribeLayerObjects
```csharp
public string DescribeLayerObjects(int layer, List<GameObject> objects)
```
- **Description**: This function generates a description string that lists the names of game objects belonging to a specified layer. It provides a human-readable format of the objects found in that layer.
- **Parameters**:
  - `int layer`: The layer number for which the objects are being described.
  - `List<GameObject> objects`: A list of `GameObject` instances that belong to the specified layer.
- **Returns**: A string that describes the objects in the specified layer, formatted as "Objects in layer [LayerName]: [Object1], [Object2], ...".