# InstructionManager

## Overview
The `InstructionManager` class is responsible for managing a list of instructions in a Unity game. It allows for the addition of new instructions and provides functionality to retrieve a response based on user input. This class fits into the larger codebase as a mediator between user interactions and predefined responses, facilitating a dynamic interaction experience.

## Variables
- `List<Instruction> instructions`: A private list that stores `Instruction` objects. Each `Instruction` contains a trigger phrase and a corresponding response template. This list is maintained in a sorted order based on the priority of the instructions.

## Functions
- `AddInstruction(Instruction instruction)`: This public method takes an `Instruction` object as a parameter and adds it to the `instructions` list. After adding the instruction, it sorts the list by the priority of each instruction to ensure that higher priority instructions are processed first.

- `string GetResponse(string input)`: This public method takes a string input and checks each instruction in the `instructions` list to see if the input contains the instruction's trigger. If a match is found, it returns the corresponding response template. If no matching instruction is found, it returns a default message: "No matching instruction found."