# EnhancedPromptCache

## Overview
The `EnhancedPromptCache` script is designed to manage a collection of prompts, each associated with an embedding and a priority level. This script allows for the addition of new prompts, retrieves the prompt with the highest priority, and automatically updates the priorities and lifespans of the prompts over time. It fits into the codebase by providing a mechanism for caching prompts, which can be useful in scenarios where prompt selection is based on their relevance and recency.

## Variables

- **PromptData**: A private class that holds the details of each prompt.
  - `Prompt`: A string representing the text of the prompt.
  - `Embedding`: An array of floats representing the embedding associated with the prompt.
  - `Priority`: A float indicating the importance of the prompt, which can decay over time.
  - `TimeToLive`: A float representing the lifespan of the prompt in seconds before it is considered expired.

- **cache**: A private list of `PromptData` objects that stores all the prompts currently in the cache.

- **decayRate**: A private float that determines how quickly the priority of each prompt decreases over time, set to 0.1f.

## Functions

- **AddPrompt(string prompt, float[] embedding, float initialPriority = 1.0f, float ttl = 300f)**: 
  - This public method adds a new prompt to the cache. It takes the prompt text, its associated embedding, an optional initial priority (default is 1.0), and an optional time-to-live (TTL) value in seconds (default is 300 seconds).

- **GetHighestPriorityPrompt()**:
  - This public method retrieves the prompt with the highest priority from the cache. It first calls the `UpdatePriorities` method to ensure that the priorities are current before returning the highest priority prompt.

- **UpdatePriorities()**:
  - This private method updates the priorities and TTL of all prompts in the cache. It reduces each prompt's priority based on the decay rate and the time elapsed since the last frame. It also removes any prompts that have expired (TTL <= 0) or have a priority that has fallen to zero or below.