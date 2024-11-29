# EventMemoryUpdater

## Overview
The `EventMemoryUpdater` class is designed to manage and store events in a memory cache, allowing for the recording and retrieval of event-related information based on their geographical location. It integrates with the `EnhancedPromptCache` to add and retrieve events efficiently, making it a vital component for managing event memory in applications that require spatial awareness, such as games or simulations. This class enables the user to record events with descriptions and locations, and later recall relevant memories based on proximity to a given location.

## Variables
- **memoryCache**: An instance of `EnhancedPromptCache` that stores event prompts and their associated embeddings. This cache is used to manage the lifecycle and retrieval of event memories.

## Functions
- **RecordEvent(string eventDescription, Vector3 location, float[] embedding)**: 
  - Records an event by creating a unique memory key using the event description and location. It adds this key along with the associated embedding to the `memoryCache`, setting an initial priority and a time-to-live (TTL) duration for the memory. It also logs a message indicating that the event has been recorded.

- **RecallRelevantMemory(Vector3 queryLocation, float proximityThreshold = 10f)**: 
  - Retrieves the most relevant memory from the cache based on the proximity to a given location. It checks each memory prompt's location against the query location and returns the first memory that falls within the specified proximity threshold. If no relevant memories are found, it returns a default message indicating that.

- **ParseLocation(string location)**: 
  - Converts a string representation of a location (formatted as "(x,y,z)") into a `Vector3` object. It splits the string to extract the coordinates, parses them into floats, and constructs a `Vector3` instance to represent the location in three-dimensional space.