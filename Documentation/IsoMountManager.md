# IsoMountManager

## Overview
The `IsoMountManager` class is designed to handle the mounting and unmounting of ISO files within a Unity application using PowerShell commands. This script provides two primary static methods: `MountISO` and `UnmountISO`, which allow developers to easily integrate ISO file management into their Unity projects. This functionality is particularly useful for applications that need to access files contained within ISO images without requiring the user to manually mount them.

## Variables
- **isoPath**: A string parameter representing the file path of the ISO image to be mounted or unmounted.

## Functions

### MountISO(string isoPath)
This static method mounts an ISO file specified by the `isoPath` parameter. It utilizes PowerShell's `Mount-DiskImage` command to perform the operation. The method sets up a `ProcessStartInfo` object to configure the execution of the PowerShell command, including redirecting the standard output and suppressing the window display. After executing the command, it waits for the process to complete and logs the output to the Unity console.

### UnmountISO(string isoPath)
This static method unmounts an ISO file specified by the `isoPath` parameter. It uses PowerShell's `Dismount-DiskImage` command for unmounting the ISO. Similar to the `MountISO` method, it creates a `ProcessStartInfo` object to run the command in the background without displaying a window. The method waits for the process to finish and logs the output to the Unity console.