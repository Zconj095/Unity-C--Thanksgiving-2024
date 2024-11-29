# ParticleTextureMetadata

## Overview
The `ParticleTextureMetadata` script defines a data structure for storing metadata related to particle textures in a Unity project. This scriptable object allows developers to create and manage texture assets that can be used in particle systems. It includes essential information such as the texture name, file reference, resolution, tags for filtering, and a description. This metadata can be utilized throughout the codebase to facilitate the organization and retrieval of particle textures, enhancing the overall workflow in visual effects development.

## Variables

- `textureName` (string): The name of the texture, which serves as an identifier for the particle texture.
  
- `textureFile` (Texture2D): A reference to the actual texture file used for the particle effect. This is the visual representation that will be rendered in the game.
  
- `resolution` (Vector2Int): The resolution of the texture, represented as a width and height (e.g., 512x512). This information is useful for understanding the quality and performance implications of using the texture.
  
- `tags` (string[]): An array of tags that can be used for filtering and categorizing textures. Examples include "Fire", "Smoke", and "Spark". Tags help in organizing textures and making them easily searchable.
  
- `description` (string): An optional field that provides additional information about the texture. This can be used to give context or notes about how the texture should be used or its intended effects.

## Functions
The `ParticleTextureMetadata` class does not define any functions. It primarily serves as a data container for the specified variables. The functionality of this scriptable object is to hold and manage metadata, which can be accessed and manipulated by other components in the Unity project.