# ScriptGenerator

## Overview
The `ScriptGenerator` class is designed to create new C# script files within a Unity project. It provides a method to generate a script file with a specified class name and content. This functionality can be particularly useful for developers who want to automate the creation of script templates or manage their scripts programmatically. By integrating this class into the Unity codebase, developers can streamline their workflow and enhance productivity.

## Variables
- **path**: A string that stores the full file path where the new script will be saved. It combines the application's data path with the "Scripts" directory and the specified class name.

## Functions
- **GenerateScript(string className, string content)**: This method takes two parameters: `className`, which specifies the name of the script to be created, and `content`, which contains the code that will be written into the new script file. It constructs the file path, writes the content to the file, and logs a message indicating that the script has been successfully generated.