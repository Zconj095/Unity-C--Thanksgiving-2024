# ARPlacement

## Overview
The `ARPlacement` script is responsible for enabling the placement of a specified prefab (in this case, a panel) in an augmented reality (AR) environment. It interacts with the AR system to detect touch inputs and raycast to find suitable surfaces where the prefab can be instantiated. This functionality is essential for creating interactive AR experiences, allowing users to place objects in their real-world environment through their device's camera.

## Variables

- `public GameObject panelPrefab`: This is a public variable that holds the reference to the prefab that will be instantiated in the AR environment. It can be set in the Unity Inspector.
  
- `private ARRaycastManager raycastManager`: This is a private variable that stores a reference to the `ARRaycastManager`, which is used to perform raycasting in the AR scene to detect surfaces where objects can be placed.

## Functions

- `void Start()`: This function is called when the script instance is being loaded. It initializes the `raycastManager` by finding the first instance of `ARRaycastManager` in the scene. This is crucial for setting up the raycasting functionality.

- `void Update()`: This function is called once per frame. It checks for touch inputs from the user. If a touch is detected and its phase is `TouchPhase.Began`, it performs a raycast using the `raycastManager` at the touch position. If the raycast hits a surface, it retrieves the pose of the hit and instantiates the `panelPrefab` at that position and rotation. This allows the user to place the prefab in the AR scene interactively.