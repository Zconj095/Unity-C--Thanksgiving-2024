# SceneAwareness

## Overview
The `SceneAwareness` script is designed to help identify and describe nearby objects within a defined radius in a 3D space, typically within a Unity game environment. It provides functionality to gather objects in proximity to a specified position and generate a descriptive string of those objects. This script can be useful for gameplay mechanics that require awareness of surrounding items, such as interactions, environmental awareness, or gameplay feedback.

## Variables
- **None**: This script does not define any class-level variables.

## Functions

### `GetNearbyObjects(Vector3 position, float radius)`
This function takes a position (as a `Vector3`) and a radius (as a `float`) as parameters. It uses Unity's physics system to detect all colliders within a spherical area defined by the position and radius. The function returns a list of `GameObject`s that are located within this area.

**Parameters:**
- `position`: The center point from which to search for nearby objects.
- `radius`: The radius of the sphere within which to search for objects.

**Returns:**
- A `List<GameObject>` containing all nearby objects detected within the specified radius.

### `DescribeSceneObjects(List<GameObject> objects)`
This function takes a list of `GameObject`s as input and constructs a descriptive string listing the names of these objects. It concatenates the names of the objects into a single string, providing a simple overview of the nearby objects.

**Parameters:**
- `objects`: A list of `GameObject`s to be described.

**Returns:**
- A `string` that describes the nearby objects, formatted as "Nearby objects: object1, object2, object3, ...". The trailing comma and space are removed for neatness.