# HumanAnimationDatabase

## Overview
The `HumanAnimationDatabase` script is a Unity `ScriptableObject` that serves as a centralized repository for managing human animation metadata. This script allows developers to add, remove, and search for animations based on various criteria such as name, type, and tags. It is designed to simplify the management of animations in a game or application, making it easier to organize and access animation data efficiently.

## Variables
- `animations`: A `List<HumanAnimationMetadata>` that stores all the animation metadata entries. Each entry represents a specific animation and its associated properties.

## Functions
- `AddAnimation(HumanAnimationMetadata animation)`: This method adds a new animation to the `animations` list if it is not already present. It ensures that duplicate entries are avoided.

- `RemoveAnimation(HumanAnimationMetadata animation)`: This method removes an existing animation from the `animations` list if it is currently included. It helps in managing the collection of animations by allowing for the removal of unused or unwanted animations.

- `FindAnimationByName(string name)`: This method searches for and returns a `HumanAnimationMetadata` object from the `animations` list that matches the specified animation name. If no match is found, it returns `null`.

- `FindAnimationsByType(string animationType)`: This method retrieves a list of all animations that match the specified animation type. It returns a `List<HumanAnimationMetadata>` containing all animations of the given type.

- `FindAnimationsByTag(string tag)`: This method searches for and returns a list of animations that contain the specified tag. It checks each animation's tags and returns those that match the provided tag.