# TerrainBrushMetadata

## Overview
The `TerrainBrushMetadata` script is a Unity ScriptableObject that serves as a container for metadata related to terrain brushes. This metadata includes properties such as the brush's name, texture, resolution, tags for filtering, and an optional description. The purpose of this script is to enable the organization and management of various terrain brushes within a Unity project, allowing developers to easily create and customize brushes used for terrain manipulation.

## Variables

- **brushName**: A `string` that holds the name of the brush. This is used to identify the brush in the project.
  
- **brushTexture**: A `Texture2D` that references the visual representation of the brush. This texture is displayed in the user interface to provide a visual cue for users selecting the brush.

- **resolution**: A `Vector2Int` that specifies the resolution of the brush texture, typically represented in pixels (e.g., 512x512). This determines the detail level of the brush.

- **tags**: An array of `string` that contains tags for filtering brushes. Tags can include descriptors like "Smooth", "Rough", or "Cliff", which help users categorize and find brushes based on specific characteristics.

- **description**: A `string` that provides an optional description of the brush. This can be used to give additional context or details about the brush's intended use or characteristics.

## Functions
This script does not contain any explicit functions, as it primarily defines a data structure (ScriptableObject) to hold metadata for terrain brushes. The functionality of this script is mainly utilized by other parts of the codebase that interact with terrain brushes, allowing them to access and manipulate the brush metadata as needed.