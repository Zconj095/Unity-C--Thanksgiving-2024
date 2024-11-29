# SPCell Class Documentation

## Overview
The `SPCell` class represents a region of space within a Space-Partitioning Tree (SPT). It defines an axis-aligned bounding box using a center point and half-dimensions (widths) to establish the boundaries of the spatial cell. This class is essential for managing spatial data efficiently, allowing for operations such as point containment checks, which are crucial in various applications such as game development and simulations.

## Variables
- `corner`: A `double[]` array that holds the coordinates of the starting point (corner) of the spatial cell. The length of this array determines the number of dimensions of the cell.
- `width`: A `double[]` array that represents the half-dimensions (widths) of the spatial cell in each dimension. This defines the extent of the cell from its corner.

## Properties
- `Dimension`: Returns the number of dimensions of the space defined by the spatial cell, which corresponds to the length of the `corner` array.
- `Corner`: A property to get or set the `corner` of the spatial cell. It ensures that the new corner's dimensions match those of the existing cell.
- `Width`: A property to get or set the `width` of the spatial cell. It also checks that the new width's dimensions match those of the existing cell.

## Constructors
- `SPCell(int dimension)`: Initializes a new instance of the `SPCell` class with a specified number of dimensions. It throws an exception if the dimension is less than or equal to zero.
- `SPCell(double[] corner, double[] width)`: Initializes a new instance of the `SPCell` class with specified corner and width arrays. It throws an exception if the arrays are null or if they do not have the same length.

## Methods
- `Contains(double[] point)`: Checks whether a given point lies within the boundaries of the spatial cell. It returns `true` if the point is contained within the cell; otherwise, it returns `false`. This method ensures that the point's dimensions match those of the cell.
- `ToString()`: Provides a string representation of the `SPCell` for debugging purposes. It displays the corner and width of the cell in a readable format.
- `ArrayToString(double[] array)`: A private helper method that converts a `double[]` array into a string format for easier debugging. It returns "null" if the array is null, or formats the array elements as a comma-separated string enclosed in brackets.