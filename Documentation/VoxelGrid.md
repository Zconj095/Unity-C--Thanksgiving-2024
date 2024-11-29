# VoxelGrid

## Overview
The `VoxelGrid` class is designed to represent a three-dimensional grid of voxels (volumetric pixels). It provides methods to access and modify individual voxels within the grid. This class is essential for applications that require 3D spatial representation, such as simulations, games, or visualizations. It fits within a larger codebase that manipulates 3D data, allowing for efficient storage and retrieval of voxel information.

## Variables
- `Voxel[] voxels`: An array that holds the voxel objects in the grid. Each voxel represents a unit of space in the 3D grid.
- `int width`: The width of the voxel grid, defining how many voxels exist along the x-axis.
- `int height`: The height of the voxel grid, defining how many voxels exist along the y-axis.
- `int depth`: The depth of the voxel grid, defining how many voxels exist along the z-axis.

## Functions
- `public VoxelGrid(int width, int height, int depth)`: 
  - Constructor method for the `VoxelGrid` class. It initializes the grid with the specified dimensions (width, height, and depth) and allocates memory for the voxel array.

- `public Voxel GetVoxel(int x, int y, int z)`:
  - This method retrieves the voxel located at the specified coordinates (x, y, z) in the grid. It calculates the index in the `voxels` array based on the provided coordinates and returns the corresponding `Voxel` object.

- `public void SetVoxel(int x, int y, int z, Voxel voxel)`:
  - This method sets the voxel at the specified coordinates (x, y, z) to a new `Voxel` object. It calculates the index in the `voxels` array and updates the voxel at that position with the provided `Voxel` instance.