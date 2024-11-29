# TrapezoidalFunction

## Overview
The `TrapezoidalFunction` class represents a membership function in the shape of a trapezoid, which is a fundamental concept in fuzzy logic. This class can create full trapezoids or half trapezoids depending on the parameters provided. It extends the `PiecewiseLinearFunction` class, meaning it inherits properties and behaviors from that class, allowing it to be used in a broader context within the UnityFuzzy namespace. This class is particularly useful in scenarios where fuzzy logic is applied, such as decision-making systems or control systems.

## Variables
- **EdgeType**: An enumeration to define the type of edge for the trapezoidal function. It can take one of two values:
  - **Left**: Represents a left edge trapezoid.
  - **Right**: Represents a right edge trapezoid.

- **points**: An array of `Point` objects that represent the vertices of the trapezoidal shape. The number of points depends on the constructor used.

## Functions
- **TrapezoidalFunction(int size)**: A private constructor that initializes the `TrapezoidalFunction` with a specified number of points. It calls the base constructor of `PiecewiseLinearFunction` with a new array of `Point` objects of the given size.

- **TrapezoidalFunction(float m1, float m2, float m3, float m4, float max = 1.0f, float min = 0.0f)**: A public constructor that creates a full trapezoid with four points. It initializes the points based on the provided parameters, where:
  - `m1`: x-coordinate of the left bottom point.
  - `m2`: x-coordinate of the left top point.
  - `m3`: x-coordinate of the right top point.
  - `m4`: x-coordinate of the right bottom point.
  - `max`: the maximum value of the trapezoid (default is 1.0).
  - `min`: the minimum value of the trapezoid (default is 0.0).

- **TrapezoidalFunction(float m1, float m2, float m3, float max = 1.0f, float min = 0.0f)**: A public constructor that creates a half trapezoid with three points. It initializes the points based on the provided parameters, where:
  - `m1`: x-coordinate of the left bottom point.
  - `m2`: x-coordinate of the top point.
  - `m3`: x-coordinate of the right bottom point.
  - `max`: the maximum value of the trapezoid (default is 1.0).
  - `min`: the minimum value of the trapezoid (default is 0.0).

- **TrapezoidalFunction(float m1, float m2, float max, float min, EdgeType edge)**: A public constructor that creates a half trapezoid with two points. The points' configuration depends on the specified edge type:
  - If `edge` is `Left`, it creates a left edge trapezoid.
  - If `edge` is `Right`, it creates a right edge trapezoid.

- **TrapezoidalFunction(float m1, float m2, EdgeType edge)**: A public constructor that creates a half trapezoid with two points, defaulting the maximum value to 1.0 and the minimum value to 0.0. The trapezoid's configuration is determined by the specified edge type.