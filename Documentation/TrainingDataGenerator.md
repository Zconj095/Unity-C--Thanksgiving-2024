# TrainingDataGenerator

## Overview
The `TrainingDataGenerator` class is responsible for generating training pairs from a conversation history. It processes messages exchanged between a user and an AI, creating a list of input-output pairs that can be used for training machine learning models. The input is the concatenated messages from the user, and the output is the corresponding AI response. This functionality is crucial for training conversational agents, as it helps to structure the training data in a way that reflects real interactions.

## Variables
- `trainingPairs`: A `List` of tuples containing pairs of strings, where each tuple represents a training example with the user's context as the input and the AI's response as the output.
- `context`: A `string` that accumulates messages from the user during the conversation until an AI response is encountered.

## Functions
- `GenerateTrainingPairs(ConversationHistory conversation)`: 
  - This function takes a `ConversationHistory` object as input, which contains a list of messages exchanged in a conversation. It iterates through each message, checking the sender. If the sender is the user, it appends the message to the `context`. If the sender is the AI, it creates a training pair from the accumulated `context` and the AI's message, then resets the `context`. The function returns a list of training pairs that can be used for further processing or training purposes.