# ConversationMetadata

## Overview
The `ConversationMetadata` script is a Unity component designed to manage a collection of linked vector IDs associated with conversations. It provides functionality to add and remove vector IDs, ensuring that each ID is unique within the list. This script can be used in a broader codebase that involves conversation management, where vectors might represent different conversation threads or topics.

## Variables

- **LinkedVectorIds**: 
  - Type: `List<string>`
  - Description: A private list that stores unique vector IDs linked to the conversation. This list is initialized in the constructor and can be modified through the provided methods.

## Functions

- **ConversationMetadata()**: 
  - Description: Constructor for the `ConversationMetadata` class. It initializes the `LinkedVectorIds` list to ensure it starts as an empty collection.

- **LinkVector(string vectorId)**: 
  - Description: Adds a vector ID to the `LinkedVectorIds` list if it is not already present. This method ensures that each vector ID is unique within the list, preventing duplicates.

- **UnlinkVector(string vectorId)**: 
  - Description: Removes a specified vector ID from the `LinkedVectorIds` list if it exists. This method allows for the management of linked vector IDs, facilitating the removal of unwanted or outdated IDs.