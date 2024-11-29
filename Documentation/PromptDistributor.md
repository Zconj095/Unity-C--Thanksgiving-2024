# PromptDistributor

## Overview
The `PromptDistributor` class is designed to manage the distribution of prompts to a set of agents within a Unity game environment. Its main function is to randomly select one of the available agents and send a prompt along with its associated embeddings to that agent for processing. This class plays a crucial role in ensuring that prompts are effectively shared among agents, which can be part of a larger system for handling user inputs or AI interactions.

## Variables
- `agents`: A private list of `PromptCache` objects that represents the agents capable of receiving prompts. This list is initialized as an empty list and is intended to store all the agents that can process the prompts.

## Functions
- `DistributePrompt(string prompt, float[] embedding)`: This public method is responsible for distributing a given prompt to one of the agents. It takes two parameters:
  - `prompt`: A string that represents the prompt to be distributed.
  - `embedding`: An array of floats that represents the embeddings associated with the prompt.
  
  The method randomly selects an agent from the `agents` list using `UnityEngine.Random.Range`, then calls the `AddPrompt` method on the selected agent, passing the prompt and its embeddings. After successfully distributing the prompt, it logs a message indicating which agent received the prompt.