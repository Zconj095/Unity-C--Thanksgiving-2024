# AwarenessModule

## Overview
The `AwarenessModule` script is designed to determine whether a player is within the line of sight of a game object in a Unity environment. This functionality is crucial for AI entities that need to react to the player's presence, allowing them to engage or respond based on the player's visibility. The script implements a simple field of view check, where it calculates the angle between the game object's forward direction and the direction to the player. If the angle is less than 45 degrees, the player is considered to be in sight.

## Variables
- **None**: This script does not define any variables at the class level. It operates solely using parameters passed to its methods.

## Functions
- **IsPlayerInSight(Transform player)**: 
  - **Description**: This method checks if the player is within the visibility range of the game object. It calculates the direction from the game object to the player and computes the angle between this direction and the game object's forward direction. If the angle is less than 45 degrees, it returns `true`, indicating that the player is in sight; otherwise, it returns `false`. This function is essential for AI behavior that depends on player visibility.