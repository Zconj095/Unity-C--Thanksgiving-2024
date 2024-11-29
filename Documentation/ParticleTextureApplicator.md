# ParticleTextureApplicator

## Overview
The `ParticleTextureApplicator` script is designed to manage and apply textures to particle systems in Unity. It interacts with a `ParticleTextureDatabase` to retrieve texture data based on specified names or tags. This functionality allows developers to easily customize the appearance of particle systems in their games or applications, enhancing visual effects without hardcoding textures directly into the particle system.

## Variables
- **textureDatabase**: An instance of `ParticleTextureDatabase` that holds a collection of textures. This database is used to find textures by their name or tag.
- **particleRenderer**: An instance of `ParticleSystemRenderer` that represents the particle system's renderer. It is responsible for rendering the particle system and allows the script to change its material properties.

## Functions
- **ApplyTexture(string textureName)**: 
  - This function takes a string parameter `textureName`, which is the name of the texture to be applied. It searches the `textureDatabase` for a texture matching the provided name. If found, it sets the main texture of the `particleRenderer`'s material to the corresponding texture file. If the texture is not found, it logs a warning message to the console.

- **ApplyRandomTextureByTag(string tag)**: 
  - This function takes a string parameter `tag`, which is used to find textures associated with that tag in the `textureDatabase`. If textures are found, it selects one at random and applies it to the `particleRenderer`'s material. If no textures are found with the specified tag, it logs a warning message to the console.