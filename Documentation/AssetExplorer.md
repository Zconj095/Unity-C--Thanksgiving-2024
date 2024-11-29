# AssetExplorer

## Overview
The `AssetExplorer` script is designed to facilitate the listing of all assets within a Unity project. It utilizes the Unity Editor's `AssetDatabase` to retrieve and log the paths of every asset available in the project. This functionality is particularly useful for developers who need to quickly view or audit the assets they are working with, ensuring better organization and management of project resources.

## Variables
- **assetPaths**: An array of strings that stores the paths of all assets in the project. This variable is populated by calling `AssetDatabase.GetAllAssetPaths()`.

## Functions
- **ListAssets()**: This public method retrieves all asset paths using the `AssetDatabase.GetAllAssetPaths()` method and iterates through each path. For every asset path, it logs the path to the console using `Debug.Log()`. This allows developers to see a complete list of assets in the project, enhancing visibility and organization.