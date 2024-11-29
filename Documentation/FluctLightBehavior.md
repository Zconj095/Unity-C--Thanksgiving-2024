# FluctLightBehavior

## Overview
The `FluctLightBehavior` script is designed to control the visual representation of a light source in a Unity game, simulating emotional states through color and intensity changes. It utilizes a `FluctLight` class to define properties such as emotional intensity and colors, and it manages a particle system and a light component to create an engaging visual effect. This script fits within a larger codebase that may involve character emotions or environmental interactions, allowing for dynamic lighting that responds to gameplay events.

## Variables
- **CurrentFluctLight**: An instance of the `FluctLight` class that holds the current light's properties, such as name, emotional intensity, memory capacity, base color, and active color.
- **GlowEffect**: A reference to a `ParticleSystem`, which provides a glowing visual effect that represents the light's state.
- **LightAura**: A reference to a `Light` component that emits light in the scene, changing color and intensity based on the emotional state.
- **pulseSpeed**: A float that determines the speed of the pulsing effect of the light's intensity.
- **isActive**: A boolean that indicates whether the light is currently activated or not.

## Functions
- **Start()**: Initializes the visual representation of the light by setting the glow effect's color and the light aura's color and intensity based on the properties of `CurrentFluctLight`.
  
- **Update()**: Called once per frame, this function invokes `SimulatePulsing()` to create a pulsing effect on the light's intensity.

- **SimulatePulsing()**: Adjusts the intensity of the light aura using a ping-pong function that creates a smooth pulsing effect based on the emotional intensity of the current light.

- **ActivateLight()**: Activates the light, changing its visual representation to the active color and increasing its intensity. It sets `isActive` to true.

- **DeactivateLight()**: Deactivates the light, resetting its visual representation to the base color and reducing its intensity. It sets `isActive` to false.

- **UpdateEmotionalState(float intensity)**: Updates the emotional intensity of the current light and clamps its value between 0 and 1. It also logs the updated emotional intensity to the debug console.