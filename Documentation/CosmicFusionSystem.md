# CosmicFusionSystem

## Overview
The `CosmicFusionSystem` script is designed to manage and visualize a cosmic fusion process within a Unity game environment. It calculates a "fused centroid" by blending the influences of two fictional elements—Sapphire and Lapis Lazuli—while constraining the result within a specified radius defined by Rose Quartz. This script plays a crucial role in the codebase by integrating visual elements and physics to create an engaging cosmic experience.

## Variables
- `cosmicCentroid`: A `Vector3` variable that represents the initial coordinates of the cosmic centroid, initialized to the origin (0, 0, 0).
- `roseQuartzRadius`: A `float` variable that defines the radius within which the fused centroid is constrained. It is set to a default value of 3.0 units.
- `sapphireInfluence`: A `Vector3` variable that holds the influence vector of the Sapphire element, initialized to (1.5, -0.5, 0.7).
- `lapisLazuliInfluence`: A `Vector3` variable that holds the influence vector of the Lapis Lazuli element, initialized to (-0.8, 0.6, 1.0).
- `fusedCentroid`: A `Vector3` variable that stores the resulting coordinates of the fused centroid after calculations.

## Functions
- `Start()`: This function is called when the script instance is being loaded. It initializes the cosmic centroid, Sapphire and Lapis Lazuli influences, and the fused centroid.
  
- `Update()`: This function is called once per frame. It triggers the computation of the fused centroid by calling the `ComputeFusedCentroid()` method.

- `ComputeFusedCentroid()`: A private method that calculates the fused centroid by blending the influences of Sapphire and Lapis Lazuli with the cosmic centroid. It also ensures that the fused centroid does not exceed the specified Rose Quartz sealing radius.

- `OnDrawGizmos()`: This method is used to visualize the cosmic centroid, fused centroid, and Rose Quartz sealing radius in the Unity editor. It draws spheres and wire spheres to represent these elements when the application is playing.

- `OnGUI()`: This function is called for rendering and handling GUI events. It displays the current values of the fused centroid and sealing radius on the screen.