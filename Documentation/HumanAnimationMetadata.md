# HumanAnimationMetadata

## Overview
The `HumanAnimationMetadata` script is a Unity ScriptableObject that serves as a container for metadata related to human animations. It is used to store essential information about various animation clips, making it easier to manage and reference animations throughout the codebase. This script allows developers to categorize animations by type, duration, and tags, facilitating a more organized approach to animation management in Unity projects.

## Variables

- `animationName`: A string that represents the name of the animation. This is used to identify the animation in the database.
  
- `animationClip`: An `AnimationClip` reference that links to the actual animation file. This is the visual representation of the animation that will be played.

- `animationType`: A string that specifies the type of animation, such as "Idle," "Run," or "Attack." This helps in categorizing animations based on their function.

- `duration`: A float that indicates the length of the animation in seconds. This information is crucial for timing and synchronization with other animations or game events.

- `tags`: An array of strings that contains tags for filtering animations. Tags like "Combat" or "Casual" can be used to quickly find and categorize animations based on specific gameplay contexts.

- `description`: A string that provides an optional description of the animation. This can be used to give additional context or details about the animation's purpose or usage.

## Functions
(Note: The current script does not define any functions, as it primarily consists of variable declarations within a ScriptableObject. However, in a broader context, this script may be utilized in conjunction with other scripts that handle animation playback or management.)