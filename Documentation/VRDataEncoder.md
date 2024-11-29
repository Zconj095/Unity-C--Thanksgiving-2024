# VRDataEncoder

## Overview
The `VRDataEncoder` class is designed to handle the encoding and decoding of VR input data, specifically the position and rotation of VR objects. This functionality is essential for converting complex 3D data into a format that can be easily transmitted or stored, and then reverted back to its original form. The methods within this class are particularly useful in applications where data integrity and efficient data handling are crucial, such as in multiplayer VR environments or when saving game states.

## Variables
- **None**: This class does not contain any instance variables. It only defines static methods that operate on input parameters.

## Functions

### `EncodeInput(Vector3 position, Quaternion rotation)`
- **Description**: This static method takes in a 3D position and rotation represented by a `Vector3` and a `Quaternion`, respectively. It converts these values into an array of integers by multiplying each component by 1000 and rounding to the nearest integer. This encoding process prepares the data for encryption or storage.
- **Parameters**:
  - `Vector3 position`: The 3D coordinates of the object in space.
  - `Quaternion rotation`: The orientation of the object in space.
- **Returns**: An array of integers representing the encoded position and rotation.

### `DecodePosition(int[] encodedData)`
- **Description**: This static method takes an array of integers that represent encoded position data and converts it back into a `Vector3`. The decoding process involves dividing each integer by 1000 to retrieve the original floating-point values.
- **Parameters**:
  - `int[] encodedData`: An array of integers containing the encoded position data.
- **Returns**: A `Vector3` representing the decoded position in 3D space.