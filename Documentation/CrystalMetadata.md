# CrystalMetadata Script

## Overview
The `CrystalMetadata` script is a Unity ScriptableObject designed to store metadata about different types of crystals in a game or application. This metadata includes attributes such as the crystal's name, visual representation, resolution, type, associated tags for filtering, and an optional description. By using ScriptableObjects, this script allows for easy management and organization of crystal data within the Unity environment, enabling developers to create and modify crystal assets without altering the core codebase.

## Variables
- `crystalName`: A `string` that holds the name of the crystal.
- `crystalFile`: A `Texture2D` that references the 2D visual representation of the crystal asset.
- `resolution`: A `Vector2Int` that specifies the resolution of the crystal asset.
- `crystalType`: A `string` that indicates the type of the crystal, such as "Quartz", "Emerald", or "Ruby".
- `tags`: An array of `string` that contains tags for filtering the crystals, such as "Rare", "Shiny", or "Blue".
- `description`: An optional `string` that provides a description of the crystal.

## Functions
The `CrystalMetadata` class does not contain any functions. Its primary purpose is to serve as a data container for the variables listed above, allowing for the easy creation and management of crystal-related metadata in a Unity project.