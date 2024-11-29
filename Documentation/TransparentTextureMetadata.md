# TransparentTextureMetadata

## Overview
The `TransparentTextureMetadata` script is a Unity ScriptableObject designed to store metadata related to transparent textures. This script allows developers to create and manage a database of textures that can be referenced throughout the game. Each instance of `TransparentTextureMetadata` contains information about a specific texture, including its name, file reference, resolution, associated tags, and a description. This structured approach facilitates the organization and retrieval of texture assets within the Unity environment.

## Variables
- **textureName**: A `string` that holds the name of the texture. This is used for identification and display purposes.
- **textureFile**: A `Texture2D` reference that points to the actual transparent texture image file. This is the visual asset that will be used in the game.
- **resolution**: A `Vector2Int` that specifies the resolution of the texture, typically in pixels (e.g., 512x512). This information can be useful for performance optimization and asset management.
- **tags**: An array of `string` values that allows developers to assign tags to the texture for easier filtering and categorization (e.g., "UI", "Overlay", "Glass").
- **description**: An optional `string` that provides a textual description of the texture. This can be used to give additional context or details about the texture's intended use.

## Functions
- **None Defined**: This script does not define any custom functions. It primarily serves as a data container for storing texture metadata within the Unity editor. The functionality is inherently tied to the Unity engine's ScriptableObject system, allowing for easy creation and management of texture metadata assets.