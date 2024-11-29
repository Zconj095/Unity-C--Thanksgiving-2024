# AnimationMetadata Script

## Overview
The `AnimationMetadata` script defines a data structure used to store metadata about animations within a Unity project. It serves as a ScriptableObject, allowing developers to create and manage animation data assets directly within the Unity Editor. This metadata can be utilized for organizing and filtering animations based on various attributes, making it easier to manage animations for different creature types and actions in the game.

## Variables

- `animationName`: A `string` that holds the name of the animation. This is used to identify the animation within the project.
  
- `creatureType`: A `string` that specifies the type of creature the animation is associated with (e.g., "Dragon", "Zombie", "Robot"). This helps categorize animations by creature type.

- `animationClip`: An `AnimationClip` object that references the actual animation data. This is the animation that will be played in the game.

- `tags`: An array of `string` values that are used for filtering animations. Tags can include descriptors such as "Attack" or "Idle," allowing for easier searching and categorization.

- `duration`: A `float` that indicates the length of the animation in seconds. This helps in managing timing and transitions between animations.

## Functions
(Note: The provided script does not define any functions. It is primarily a data structure for holding animation metadata.) 

This script is essential for organizing animation data in a way that is easily accessible and manageable, enhancing the workflow of animators and developers working on the project.