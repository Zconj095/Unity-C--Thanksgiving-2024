# ARObjectSync

## Overview
The `ARObjectSync` script is designed to handle the synchronization of AR (Augmented Reality) objects' positions and rotations across a network. It provides functionality to package the object's current state into a JSON message format, which can then be sent over the network for real-time updates. This script is likely part of a larger codebase that deals with AR interactions, ensuring that multiple users can see and interact with the same virtual objects in a synchronized manner.

## Variables
- **None**: This script does not define any class-level variables. All data is handled within the `SendObjectUpdate` method.

## Functions
### `SendObjectUpdate(Vector3 position, Quaternion rotation)`
This method takes in two parameters, `position` and `rotation`, which represent the current position and orientation of an AR object. It converts these parameters into a JSON string that contains the following data:
- `posX`, `posY`, `posZ`: The X, Y, and Z coordinates of the object's position.
- `rotX`, `rotY`, `rotZ`, `rotW`: The X, Y, Z, and W components of the object's rotation.

The resulting JSON string is intended to be sent over the network, although the actual sending mechanism is not implemented in this snippet.