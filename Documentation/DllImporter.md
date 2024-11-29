# DllImporter

## Overview
The `DllImporter` class is a Unity MonoBehaviour that facilitates the loading and interaction with dynamic link libraries (DLLs) in a Unity application. It provides methods to load a specified DLL, retrieve a function pointer from it, and convert that pointer into a usable delegate. This is particularly useful for integrating native code with Unity, allowing developers to leverage existing libraries and functionalities written in languages like C or C++.

## Variables
- **hModule**: An `IntPtr` that holds the handle to the loaded DLL. It is used to reference the DLL for further operations, such as retrieving function pointers.

## Functions
- **LoadLibrary(string dllToLoad)**: A static external function that loads a specified DLL into the process. It returns a handle to the loaded library, which is used for further operations. If the load fails, it sets the last error code.

- **GetProcAddress(IntPtr hModule, string procedureName)**: A static external function that retrieves the address of an exported function or variable from the specified DLL. It takes the handle of the loaded library and the name of the procedure as parameters.

- **FreeLibrary(IntPtr hModule)**: A static external function that frees the loaded DLL, releasing its resources. It requires the handle to the DLL as a parameter.

- **GetFunction<T>(string dllPath, string functionName)**: A generic static method that loads a DLL from the provided path and retrieves a function by its name. It returns a delegate of type `T`, which represents the function. If the DLL fails to load or the function is not found, it throws an exception. The method ensures type safety by using generics, allowing for the retrieval of functions with different signatures.