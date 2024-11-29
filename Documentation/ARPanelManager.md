# ARPanelManager

## Overview
The `ARPanelManager` script is responsible for managing the instantiation of an augmented reality (AR) panel within a Unity scene. It creates an instance of the `adminPanelPrefab` at a specified position and attaches it to the main camera, ensuring that the panel remains in view as the camera moves. This functionality is essential for AR applications where user interfaces need to be anchored in the real world.

## Variables

- **adminPanelPrefab**: 
  - Type: `GameObject`
  - Description: A reference to the prefab of the admin panel that will be instantiated in the AR scene. This prefab contains the visual and interactive elements of the panel.

## Functions

- **Start()**: 
  - Description: This function is called when the script is first executed. It instantiates the `adminPanelPrefab` at the position (0, 0, 1) in the scene and sets its parent to the main camera. This ensures that the panel is correctly positioned in front of the camera and will move with it, providing an immersive AR experience.