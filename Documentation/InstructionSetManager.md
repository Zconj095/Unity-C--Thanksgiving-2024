# InstructionSetManager

## Overview
The `InstructionSetManager` class is responsible for managing a default set of instructions within a Unity game environment. It allows for the setting and retrieval of a specific `InstructionSet`, which can be used throughout the game to dictate behaviors, actions, or rules. This class serves as a centralized manager for instruction sets, ensuring that the default instructions are easily accessible and modifiable.

## Variables

- `private InstructionSet defaultInstructionSet`: This variable holds the default set of instructions that can be set and retrieved by the methods provided in the class. It is kept private to enforce encapsulation, ensuring that it can only be modified through the provided methods.

## Functions

- `public void SetDefaultInstructionSet(InstructionSet instructionSet)`: This method allows the user to set the `defaultInstructionSet` variable to a new `InstructionSet`. It takes one parameter, `instructionSet`, which is the new set of instructions that will become the default.

- `public InstructionSet GetDefaultInstructionSet()`: This method returns the current value of the `defaultInstructionSet`. It does not take any parameters and is used to retrieve the existing default instruction set for use within the game.