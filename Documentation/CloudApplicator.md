# CloudApplicator

## Overview
The `CloudApplicator` script is designed to manage and apply cloud visuals in a Unity game environment. It interacts with a `CloudDatabase` to retrieve cloud data based on name or tags, allowing developers to easily switch cloud sprites for 2D representations or textures for particle systems. This script is essential for creating dynamic and visually appealing cloud effects, enhancing the overall atmosphere of the game.

## Variables

- **cloudDatabase**: An instance of `CloudDatabase` that holds the collection of cloud data, allowing the script to access and retrieve specific cloud information.
- **targetSprite**: A `SpriteRenderer` component that is responsible for rendering 2D sprites in the game. This variable holds the reference to the sprite that will display the cloud.
- **particleRenderer**: A `ParticleSystemRenderer` component that is used for rendering particle systems. This variable holds the reference to the particle system that will utilize the cloud texture.

## Functions

- **ApplyCloud(string cloudName)**: 
  - This function takes a string parameter `cloudName`, which represents the name of the cloud to be applied. It searches the `cloudDatabase` for the specified cloud. If found, it creates a new `Sprite` using the cloud's texture and assigns it to the `targetSprite`. Additionally, if a `particleRenderer` is present, it updates the main texture of the particle system with the cloud's texture. If the cloud is not found, it logs a warning message.

- **ApplyRandomCloudByTag(string tag)**: 
  - This function takes a string parameter `tag`, which is used to filter clouds in the `cloudDatabase`. It retrieves a list of clouds associated with the given tag. If any clouds are found, it randomly selects one and applies it using the `ApplyCloud` function. If no clouds are found with the specified tag, it logs a warning message.