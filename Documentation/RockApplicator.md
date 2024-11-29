# RockApplicator

## Overview
The `RockApplicator` class is designed to manage the application of rock textures or sprites to target objects in a Unity game. It interacts with a `RockDatabase` to retrieve rock data based on either a specific rock name or a tag associated with multiple rocks. This class is particularly useful in scenarios where different rock visuals need to be applied dynamically to game objects, enhancing the visual variety in a game environment.

## Variables
- `public RockDatabase rockDatabase`: A reference to the `RockDatabase` that contains information about available rocks, including their textures and metadata.
- `public SpriteRenderer targetSprite`: A reference to a `SpriteRenderer` component used to render 2D sprites. This is where the rock sprite will be applied if a valid rock is found.
- `public GameObject targetObject`: A reference to a `GameObject` that utilizes material textures. This object will have its material's main texture updated with the rock texture if a valid rock is found.

## Functions
### `public void ApplyRock(string rockName)`
This function takes the name of a rock as a parameter and attempts to find the corresponding rock data in the `rockDatabase`. If the rock is found and has an associated texture file, it creates a sprite from the texture and applies it to the `targetSprite`. If the `targetObject` is provided, it updates the object's material with the rock texture. If the rock is not found, a warning message is logged.

### `public void ApplyRandomRockByTag(string tag)`
This function accepts a tag as a parameter and retrieves a list of rocks associated with that tag from the `rockDatabase`. If any rocks are found, it randomly selects one and applies it using the `ApplyRock` function. If no rocks are found with the specified tag, a warning message is logged.