# VoxelGrid4D

## Overview
The `VoxelGrid4D` class represents a four-dimensional grid of voxels, where each voxel is an instance of the `Voxel4D` class. This class allows for the storage and manipulation of voxels in a 4D space defined by its width, height, depth, and time steps. The grid is initialized with the specified dimensions, and it provides methods to retrieve and set the values of individual voxels based on their coordinates in this multi-dimensional space. This functionality is essential for applications that require a representation of data in four dimensions, such as simulations or advanced graphics.

## Variables
- `Voxel4D[] voxels`: An array that holds the voxel data for the 4D grid. Each element in this array corresponds to a voxel at a specific coordinate in the four-dimensional space.
- `int width`: The width of the voxel grid, representing the number of voxels along the first dimension.
- `int height`: The height of the voxel grid, representing the number of voxels along the second dimension.
- `int depth`: The depth of the voxel grid, representing the number of voxels along the third dimension.
- `int timeSteps`: The number of time steps in the voxel grid, representing the fourth dimension.

## Functions
- `public VoxelGrid4D(int width, int height, int depth, int timeSteps)`: Constructor that initializes a new instance of the `VoxelGrid4D` class with the specified dimensions. It calculates the total number of voxels based on these dimensions and creates an array to hold them.

- `public Voxel4D GetVoxel(int x, int y, int z, int w)`: This method retrieves a voxel from the grid at the specified coordinates (x, y, z, w). It calculates the index in the `voxels` array based on the provided coordinates and returns the corresponding `Voxel4D` object.

- `public void SetVoxel(int x, int y, int z, int w, Voxel4D voxel)`: This method sets a voxel in the grid at the specified coordinates (x, y, z, w) to the provided `Voxel4D` object. It calculates the index in the `voxels` array and assigns the new voxel to that position.