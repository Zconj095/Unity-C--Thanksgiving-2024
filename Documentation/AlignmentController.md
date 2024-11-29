# AlignmentController

## Overview
The `AlignmentController` script is responsible for aligning cortical activity patterns in a grid-based system within a Unity environment. It utilizes a target pattern generated through mathematical functions to guide the alignment process. The script integrates with the `CorticalActivity` component, adjusting its activity based on the difference between the current activity and the target pattern using a gradient descent approach. This alignment process is executed during each frame update, ensuring that the cortical activity gradually conforms to the desired pattern.

## Variables
- **corticalActivity**: An instance of the `CorticalActivity` class. This variable is used to access the current activity data and update it based on the alignment process.
- **learningRate**: A float value that determines the rate at which the alignment occurs. It affects how quickly the current activity adjusts towards the target pattern.
- **targetPattern**: A 2D array of floats that holds the generated target pattern for alignment. This pattern is computed in the `GenerateTargetPattern` method and serves as the reference for the alignment process.

## Functions
- **Start()**: This Unity lifecycle method is called before the first frame update. It triggers the generation of the target pattern by calling the `GenerateTargetPattern` method.

- **GenerateTargetPattern()**: This method creates a 2D target pattern using sine and cosine functions based on the grid size defined in the `CorticalActivity` instance. The pattern is stored in the `targetPattern` variable and serves as the reference for aligning cortical activity.

- **Update()**: Another Unity lifecycle method that is called once per frame. It invokes the `AlignCorticalActivity` method to continually adjust the cortical activity based on the target pattern.

- **AlignCorticalActivity()**: This method performs the core functionality of the script. It retrieves the current cortical activity, calculates the error between the current activity and the target pattern, and updates the activity using a gradient descent approach. The updated activity is then sent back to the `CorticalActivity` instance.

- **OnDrawGizmos()**: This method is used to visualize the target pattern in the Unity editor. It draws cubes representing the heights of the target pattern at each grid position, allowing for a visual representation of the alignment target. The visualization is only executed if the `targetPattern` has been generated.