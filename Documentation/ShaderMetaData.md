# ShaderMetadata Script

## Overview
The `ShaderMetadata` script is a Unity ScriptableObject that serves as a data container for storing metadata related to shaders. This script allows developers to create and manage shader information within the Unity Editor. It provides an organized way to categorize shaders with associated names, descriptions, and tags, which can be particularly useful for managing a large number of shaders in a project.

## Variables

- **shaderName**: A `string` representing the name of the shader. This helps in identifying the shader within the project.

- **description**: A `string` that provides a brief description of the shader's purpose or functionality. This is useful for documentation and understanding the shader's role.

- **shader**: An instance of the `Shader` class. This variable holds a reference to the actual shader asset in Unity, allowing the script to link metadata directly to the shader.

- **tags**: An array of `string` that contains optional tags for categorization. Tags can be used to group shaders by specific properties or functionalities, making it easier to filter and find shaders in the project.

## Functions

- **N/A**: The `ShaderMetadata` script does not contain any functions. It is primarily a data structure designed to hold metadata about shaders. The functionality is derived from its use as a ScriptableObject within the Unity Editor, allowing for easy creation and management of shader metadata assets.