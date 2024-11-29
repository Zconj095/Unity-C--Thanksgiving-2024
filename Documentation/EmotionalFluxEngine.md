# EmotionalFluxEngine

## Overview
The `EmotionalFluxEngine` class is designed to manage and analyze a series of emotional states within a Unity application. It tracks recent emotions and determines whether there is a significant change (or "flux") in emotional states over time. This functionality can be useful in various applications, such as games or simulations, where character emotions play a crucial role in the user experience. The class maintains a queue of recent emotions and provides methods to update the current emotion as well as to detect any shifts in emotional state.

## Variables

- `recentEmotions`: A `Queue<string>` that stores the most recent emotional states. It is limited to a maximum of `maxTrackedEmotions` to ensure that only the latest emotions are considered for flux detection.
- `maxTrackedEmotions`: A `const int` that defines the maximum number of emotions to be tracked at any given time. In this case, it is set to 5.

## Functions

- `public void UpdateEmotion(string currentEmotion)`: This method updates the current emotion by adding it to the `recentEmotions` queue. If the queue has reached its maximum capacity, it removes the oldest emotion to make space for the new one.

- `public string DetectFlux()`: This method analyzes the emotions stored in the `recentEmotions` queue. It returns a string indicating the emotional state:
  - Returns "STABLE" if there are fewer than two emotions tracked or if the first and last emotions in the queue are the same.
  - Returns "SHIFTING" if the first and last emotions differ, indicating a change in emotional state.