# Voxel Script

## Overview
The `Voxel` script defines a 3D voxel object, which is a fundamental unit used in 3D space to represent a volume. Each voxel has specific properties such as its position in 3D space, color, density, and material type. This script is an essential part of a larger codebase that likely deals with 3D rendering, terrain generation, or voxel-based environments in Unity. By encapsulating the properties of a voxel, this script allows for easy manipulation and management of voxel data within the game or application.

## Variables
- `int x`: The x-coordinate of the voxel in 3D space.
- `int y`: The y-coordinate of the voxel in 3D space.
- `int z`: The z-coordinate of the voxel in 3D space.
- `Color color`: The color of the voxel, represented as a `Color` object.
- `float density`: The density of the voxel, which may affect its interactions or physics within the environment.
- `MaterialType materialType`: The type of material the voxel represents, defined by the `MaterialType` enumeration.

## Functions
- `Voxel(int x, int y, int z, Color color, float density, MaterialType materialType)`: This is the constructor for the `Voxel` class. It initializes a new instance of a voxel with specified coordinates, color, density, and material type. This function sets the values of the voxel's properties when a new voxel object is created.

## Enum
- `MaterialType`: This enumeration defines the types of materials that a voxel can represent. It currently includes:
  - `Air`: Represents empty space.
  - `Stone`: Represents a solid stone material.
  - `Water`: Represents a fluid water material.
  - Additional material types can be added as needed.