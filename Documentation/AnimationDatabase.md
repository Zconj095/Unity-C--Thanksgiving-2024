# AnimationDatabase

## Overview
The `AnimationDatabase` script is a Unity `ScriptableObject` that serves as a centralized repository for managing animation metadata within a game. This script allows developers to easily add, remove, and search for animations based on different criteria, such as animation name, creature type, and associated tags. By using this database, developers can maintain a structured and organized way of handling animations, making it easier to manage animations across various game objects.

## Variables
- `animations`: A `List<AnimationMetadata>` that holds all the animation metadata objects. Each `AnimationMetadata` object contains information about a specific animation, such as its name, creature type, and tags.

## Functions
- `AddAnimation(AnimationMetadata animationData)`: This method adds a new animation to the `animations` list if it does not already exist in the list. It ensures that there are no duplicate animations stored.

- `RemoveAnimation(AnimationMetadata animationData)`: This method removes an existing animation from the `animations` list if it is currently present. It helps in managing the animations by allowing developers to delete unwanted or outdated animations.

- `FindAnimationByName(string name)`: This method searches for an animation in the `animations` list by its name. It returns the `AnimationMetadata` object that matches the provided name, or `null` if no match is found.

- `FindAnimationsByCreatureType(string creatureType)`: This method retrieves a list of animations that are associated with a specific creature type. It returns a `List<AnimationMetadata>` containing all animations that match the provided creature type.

- `FindAnimationsByTag(string tag)`: This method finds all animations that contain a specific tag. It returns a `List<AnimationMetadata>` with all animations that have the specified tag in their tags array.