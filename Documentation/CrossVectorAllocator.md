# CrossVectorAllocator

## Overview
The `CrossVectorAllocator` class is part of a Unity project and is designed to manage a collection of `VectorState` objects, referred to as layers. Its primary function is to allow the addition of these layers and to compute a merged `VectorState` by applying a specified merging function across all layers. This class is useful in scenarios where multiple vector states need to be combined, such as in graphics rendering or physics simulations.

## Variables
- **layers**: A private list of `VectorState` objects that stores the layers added to the `CrossVectorAllocator`. This list allows for the dynamic management of multiple vector states.

## Functions
- **CrossVectorAllocator()**: Constructor that initializes the `layers` list to an empty state. This sets up the object for use by ensuring that it starts with no layers.

- **void AddLayer(VectorState state)**: This method accepts a `VectorState` object as a parameter and adds it to the `layers` list. It enables the user to build up the collection of layers.

- **VectorState GetLayer(int index)**: This method retrieves a `VectorState` from the `layers` list at the specified index. It allows access to individual layers, enabling users to interact with specific states as needed.

- **VectorState AllocateCrossVectors(Func<VectorState, VectorState, VectorState> mergeFunc)**: This method takes a function (`mergeFunc`) as a parameter, which defines how to merge two `VectorState` objects. It iteratively applies this function to combine the first layer with each subsequent layer in the `layers` list, ultimately returning a single `VectorState` that represents the result of all merges. This is useful for obtaining a composite vector state from multiple inputs.