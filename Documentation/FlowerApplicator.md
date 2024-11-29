# FlowerApplicator

## Overview
The `FlowerApplicator` class is responsible for applying flower sprites to UI elements or 2D sprites in a Unity game. It interacts with a `FlowerDatabase` to find and retrieve flower data based on the flower's name or associated tags. This class allows developers to easily change the visual representation of flowers in the game by either specifying a flower directly or selecting one randomly based on a tag. 

## Variables
- `FlowerDatabase flowerDatabase`: A reference to the database that contains information about various flowers, including their names and associated sprite files.
- `Image targetImage`: A Unity UI Image component where the flower sprite can be applied for UI representation.
- `SpriteRenderer targetSprite`: A Unity SpriteRenderer component where the flower sprite can be applied for 2D sprite representation.

## Functions
- `public void ApplyFlower(string flowerName)`: 
  - This function takes a flower's name as a parameter, searches for the corresponding flower data in the `flowerDatabase`, and applies the flower sprite to either the `targetImage` or `targetSprite`. If the flower is not found, a warning message is logged.

- `public void ApplyRandomFlowerByTag(string tag)`:
  - This function takes a tag as a parameter, retrieves a list of flowers associated with that tag from the `flowerDatabase`, and randomly selects one to apply using the `ApplyFlower` method. If no flowers are found for the specified tag, a warning message is logged.