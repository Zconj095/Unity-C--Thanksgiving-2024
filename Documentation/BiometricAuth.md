# BiometricAuth

## Overview
The `BiometricAuth` script is designed to facilitate biometric authentication within a Unity application. It leverages the PowerShell command line to initiate an external process called 'HelloBiometric', which is expected to handle the biometric authentication. This script primarily serves as a bridge between Unity and the external authentication process, allowing developers to integrate biometric verification seamlessly into their applications.

## Variables
- **process**: An instance of the `Process` class that is responsible for starting and managing the external PowerShell command to invoke the biometric authentication process.

## Functions
- **Authenticate()**: This method initiates the authentication process. It sets up a new `Process` to run PowerShell with specified arguments, which include starting the 'HelloBiometric' process with elevated privileges. The method waits for the process to complete and logs a message indicating that the authentication process has finished.