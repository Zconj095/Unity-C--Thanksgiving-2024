# ReadOnlyInterjection

## Overview
The `ReadOnlyInterjection` script is designed to monitor and log information about game objects within a specified detection radius from a designated observer point in a Unity environment. It allows developers to visualize the monitored area in the Unity Editor, making it easier to debug and understand which objects are being detected. This script fits within the codebase by providing a mechanism for spatial awareness, enabling interactions or behaviors based on the presence of nearby objects.

## Variables
- **observer**: A `Transform` representing the position in space from which the detection will occur. This is typically the point of interest or the player's position.
- **detectionRadius**: A `float` that defines the radius of the spherical area around the observer within which objects will be monitored. The default value is set to 5.0 units.
- **targetLayer**: A `LayerMask` that specifies which layers of objects should be considered for detection. This allows filtering of objects based on their assigned layers.
- **debugDraw**: A `bool` that toggles the visualization of the detection area in the Unity Editor. When set to true, a wireframe representation of the detection sphere will be drawn.

## Functions
- **Update()**: This Unity lifecycle method is called once per frame. It invokes the `ReadLocalSpace()` method to continuously monitor the area around the observer.

- **ReadLocalSpace()**: This method checks for all colliders within the detection radius of the observer. It uses the `Physics.OverlapSphere` method to gather all relevant colliders that match the specified target layer. For each detected object, it logs the object's name and position to the console.

- **OnDrawGizmos()**: This method is called by Unity to allow for custom visualization in the editor. If `debugDraw` is enabled and the `observer` is assigned, it draws a blue wireframe sphere around the observer's position, indicating the detection area. This helps developers visualize the extent of the monitoring space during development.