# CloudMetadata Script

## Overview
The `CloudMetadata` script is a Unity `ScriptableObject` designed to store metadata related to cloud assets in a cloud database. This script allows developers to define various properties of cloud assets, such as their name, type, resolution, and associated tags. By using this script, developers can easily manage and categorize cloud assets within the Unity environment, making it simpler to reference and utilize these assets in their projects.

## Variables

- `cloudName` (string): This variable holds the name of the cloud. It is a descriptive identifier for the cloud asset.
  
- `cloudFile` (Texture2D): This variable is a reference to the 2D texture representing the cloud asset. It is used for displaying the visual representation of the cloud.

- `resolution` (Vector2Int): This variable defines the resolution of the cloud asset as a two-dimensional integer vector. It specifies the width and height of the cloud texture.

- `cloudType` (string): This variable indicates the type of cloud, such as "Cumulus", "Stratus", or "Cirrus". It helps categorize the cloud based on its characteristics.

- `tags` (string[]): This variable is an array of strings that contains tags for filtering the cloud assets. Examples of tags include "Stormy", "Fluffy", and "Transparent", which can be used for better organization and searchability.

- `description` (string): This variable provides an optional description of the cloud. It allows developers to add additional context or details about the cloud asset.

## Functions
(Note: The `CloudMetadata` script does not define any functions; it only contains variables. Therefore, there are no functions to describe in this section.)