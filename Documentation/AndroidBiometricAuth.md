# AndroidBiometricAuth

## Overview
The `AndroidBiometricAuth` script is designed to handle biometric authentication on Android devices within a Unity application. It provides a method to initiate the biometric authentication process by calling a Java method that is assumed to be implemented on the Android side. This script acts as a bridge between Unity and the Android platform, allowing developers to integrate biometric features seamlessly into their games or applications.

## Variables

- **unityPlayer**: An instance of `AndroidJavaClass` that represents the Unity Player class in the Android environment. It is used to access static members of the Unity Player.
  
- **currentActivity**: An instance of `AndroidJavaObject` that represents the current activity in the Android application. This object is necessary to call methods that are tied to the lifecycle of the activity.

## Functions

- **Authenticate()**: 
  - This public method is responsible for initiating the biometric authentication process. It retrieves the current Android activity and calls the `runBiometricAuth` method, which is expected to be defined in the Android Java code. This method serves as the entry point for triggering biometric authentication, enabling secure access to features or data within the Unity application.