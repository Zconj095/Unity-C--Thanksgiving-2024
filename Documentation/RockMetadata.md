# RockMetadata

## Overview
The `RockMetadata` script defines a data structure used to store information about different types of rocks within a Unity project. It is designed as a ScriptableObject, which allows for easy creation and management of rock data assets in the Unity Editor. This script fits into the larger codebase by providing a way to encapsulate rock-related data that can be referenced and utilized by other components, such as game objects or systems that require rock information.

## Variables
- `rockName`: A string that holds the name of the rock.
- `rockFile`: A reference to a 2D texture asset that visually represents the rock.
- `resolution`: A `Vector2Int` that specifies the resolution of the rock asset, indicating its width and height.
- `rockType`: A string that categorizes the rock, such as "Granite", "Sandstone", or "Basalt".
- `tags`: An array of strings that contains tags for filtering the rocks, such as "Smooth", "Rough", or "Layered".
- `description`: An optional string that provides additional information about the rock.

## Functions
This script does not contain any functions. Its primary purpose is to serve as a data container for rock metadata, allowing other parts of the codebase to create and manipulate instances of rock data through the Unity Editor.