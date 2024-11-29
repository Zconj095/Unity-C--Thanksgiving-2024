# PromptCache

## Overview
The `PromptCache` class is designed to manage a collection of prompts and their associated vector embeddings within a Unity game environment. It allows for the addition of new prompts and retrieval of their corresponding vectors using unique identifiers (IDs). This functionality is essential for scenarios where prompts need to be stored and accessed efficiently, such as in machine learning applications or AI-driven features in games.

## Variables
- **cache**: A `Dictionary<int, float[]>` that maps each prompt ID (an integer) to its corresponding vector embedding (an array of floats). This serves as the primary storage for prompts.
- **nextId**: An `int` that keeps track of the next available ID to assign to a new prompt. It ensures that each prompt added to the cache has a unique identifier.

## Functions
- **AddPrompt(string prompt, float[] embedding)**: 
  - Takes a string `prompt` and a float array `embedding` as parameters.
  - Adds the provided embedding to the `cache` using the current `nextId` as the key.
  - Increments `nextId` for the next prompt addition.
  - Returns the unique ID assigned to the newly added prompt.
  
- **GetPromptVector(int id)**: 
  - Takes an integer `id` as a parameter.
  - Checks if the `cache` contains the given ID.
  - If the ID exists, it returns the corresponding vector embedding; otherwise, it returns `null`.