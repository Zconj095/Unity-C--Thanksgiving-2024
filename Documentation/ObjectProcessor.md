# ObjectProcessor

## Overview
The `ObjectProcessor` script is designed to process a specified GameObject within a Unity scene. It retrieves and logs information about the GameObject's components, including their properties and methods. This script is intended to be attached to a GameObject in the Unity Editor, where the user can assign a target GameObject to be processed. It serves as a utility for developers to inspect the structure and functionality of GameObjects in their projects.

## Variables
- `public GameObject targetObject`: This variable holds a reference to the GameObject that will be processed. It must be assigned in the Unity Editor; otherwise, an error will be logged when the script starts.

## Functions
- `private void Start()`: This function is called when the script instance is being loaded. It checks if the `targetObject` is assigned. If it is not, it logs an error message and exits. If it is assigned, it calls the `ProcessObject` function to begin processing.

- `private void ProcessObject(GameObject obj)`: This function takes a GameObject as an argument and logs its name. It retrieves all components attached to the GameObject and iterates through them, calling the `ProcessComponent` function for each component.

- `private void ProcessComponent(Component component)`: This function takes a Component as an argument and logs its type. It uses reflection to get all readable properties of the component and logs their names and values. It also retrieves all public instance methods of the component and logs their names. If an error occurs while reading a property, a warning message is logged.