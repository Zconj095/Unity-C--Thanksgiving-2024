# DaemonToolsManager

## Overview
The `DaemonToolsManager` class is a Unity script designed to interface with DAEMON Tools Lite, a software used for mounting and unmounting disk image files (such as ISO files). The main functions of this script allow users to mount an ISO file to a specified virtual drive and unmount it when needed. This functionality is particularly useful in scenarios where virtual drives are required for testing or running software that relies on disk images. The script utilizes the `System.Diagnostics` namespace to start external processes and capture their output.

## Variables

- **DaemonToolsPath**: A constant string representing the file path to the DAEMON Tools Lite executable. This path is used to launch the application when mounting or unmounting ISO files.

## Functions

- **MountISO(string isoPath, int driveIndex = 0)**: 
  - **Description**: This static method mounts an ISO file located at `isoPath` to a specified drive index. The default drive index is set to 0, which typically refers to the first virtual drive. The method constructs command-line arguments for the DAEMON Tools executable and starts the process to mount the ISO. It also captures and logs the output from the process.
  
- **UnmountISO(int driveIndex = 0)**: 
  - **Description**: This static method unmounts the ISO file from a specified drive index, with a default value of 0. Similar to the `MountISO` method, it builds the necessary command-line arguments for the DAEMON Tools executable, starts the process, and logs the output from the unmounting operation.