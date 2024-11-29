# Voxel4D Script

## Overview
The `Voxel4D` script defines a four-dimensional voxel object, which extends the concept of a traditional voxel by adding a time dimension. This script is part of a larger codebase that likely deals with voxel-based graphics or simulations in a four-dimensional space. The `Voxel4D` class encapsulates properties such as position, color, density, and material type, allowing for rich representation and manipulation of these four-dimensional entities.

## Variables
- **int x**: The x-coordinate of the voxel in 4D space.
- **int y**: The y-coordinate of the voxel in 4D space.
- **int z**: The z-coordinate of the voxel in 4D space.
- **int w**: The w-coordinate of the voxel, which can represent time.
- **Color color**: The color of the voxel, represented as a `Color` object.
- **float density**: The density of the voxel, which can be used for physics calculations.
- **Voxel4DMaterialType materialType**: The type of material the voxel is made of, represented by the `Voxel4DMaterialType` enumeration.
- **float time**: Represents time if the w-coordinate is interpreted as such.

## Functions
- **Voxel4D(int x, int y, int z, int w, Color color, float density, Voxel4DMaterialType materialType, float time)**: 
  - Constructor for the `Voxel4D` class. It initializes a new instance of the voxel with specified coordinates, color, density, material type, and time. This function sets the values of the instance variables to the parameters provided when creating a new `Voxel4D` object.