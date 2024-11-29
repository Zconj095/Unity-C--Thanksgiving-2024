# DynamicCognitionController

## Overview
The `DynamicCognitionController` class is responsible for managing and updating the cognitive state of an entity based on both spatial and temporal influences. It integrates information from spatial cognition and temporal feedback to compute a new cognitive state that reflects the combined effects of these influences. This class is designed to work within a Unity game engine environment, where it interacts with other components like `SpatialCognition`, `TemporalFeedback`, and `CognitiveState`. The `Update` method is called once per frame, ensuring that the cognitive state is continuously adjusted based on real-time input.

## Variables
- **public SpatialCognition spatialCognition**: A reference to the `SpatialCognition` component, which provides spatial influence data.
- **public TemporalFeedback temporalFeedback**: A reference to the `TemporalFeedback` component, which calculates feedback based on previous states.
- **public CognitiveState cognitiveState**: A reference to the `CognitiveState` component, which holds the current cognitive state of the entity.
- **public float spatialWeight**: A float value (default 0.6) that determines the weight of spatial influence in the new cognitive state calculation.
- **public float temporalWeight**: A float value (default 0.4) that determines the weight of temporal feedback in the new cognitive state calculation.

## Functions
- **void Update()**: This method is called once per frame. It checks if the required components (`spatialCognition`, `temporalFeedback`, and `cognitiveState`) are not null. If they are valid, it retrieves the current cognitive state and computes the spatial influence and temporal feedback. It then combines these influences using their respective weights to compute a new cognitive state, which is subsequently updated in the `cognitiveState` component. Finally, the current state is stored in the `temporalFeedback` for future reference.

- **float[] ComputeSpatialInfluence()**: This method calculates the spatial influence based on the current grid size of the `SpatialCognition` component. It creates an array of influence values by iterating through each element of the cognitive state vector, determining the corresponding spatial influence for each grid position (x, y) using the `GetSpatialInfluence` method of the `SpatialCognition` component. The resulting influence array is returned.