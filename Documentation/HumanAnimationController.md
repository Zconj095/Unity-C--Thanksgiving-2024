# HumanAnimationController

## Overview
The `HumanAnimationController` script is responsible for managing and playing animations for a character in a Unity game. It interfaces with an `HumanAnimationDatabase` to retrieve animation clips based on their names or associated tags. This script is integral to the character's animation system, allowing for dynamic animation playback based on gameplay events or conditions.

## Variables

- `animationDatabase`: An instance of `HumanAnimationDatabase` that contains a collection of animations. This database is used to look up animations by name or tag.
- `animator`: An instance of `Animator`, which is a Unity component that controls the animation state of the character. It is used to play the actual animation clips retrieved from the `animationDatabase`.

## Functions

- `PlayAnimation(string animationName)`: 
  - This function takes a string parameter `animationName` and attempts to find the corresponding animation in the `animationDatabase`. If the animation is found and it has a valid animation clip, it plays the animation using the `animator`. If the animation is not found, it logs a warning message to the console.

- `PlayAnimationByTag(string tag)`:
  - This function takes a string parameter `tag` and retrieves a list of animations associated with that tag from the `animationDatabase`. If there are any animations available, it randomly selects one and plays it using the `animator`. If no animations are found with the specified tag, it logs a warning message to the console.