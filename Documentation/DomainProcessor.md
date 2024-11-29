# DomainProcessor

## Overview
The `DomainProcessor` script is a Unity component that processes all types within a specified namespace. It retrieves types from the current assembly, instantiates them if they have a parameterless constructor, and logs their properties and methods. This script is useful for dynamically interacting with classes in a specified domain, which can facilitate debugging, reflection-based operations, or runtime type management within a Unity game. 

## Variables
- `domainNamespace`: A string variable that specifies the namespace or domain to be processed. It is initialized with a default value of `"MyGame.Domain"`. This variable must be set to a valid namespace for the script to function correctly.

## Functions
- `Start()`: This Unity lifecycle method is called before the first frame update. It checks if the `domainNamespace` is specified. If it is not, an error message is logged. If it is valid, it logs the processing message and calls `ProcessDomain()` with the specified namespace.

- `ProcessDomain(string domainNamespace)`: This method takes a namespace as a parameter, retrieves all types within that namespace using the `GetTypesInNamespace()` method, and iterates through each type to process it by calling `ProcessType()`.

- `ProcessType(Type type)`: This method processes a given type. It attempts to create an instance of the type if it has a parameterless constructor. It logs the creation of the instance and then lists all properties and methods of the type. For each property, it logs the property's name and type, and if readable, it logs the property's value. For methods, it logs the method's name and attempts to invoke it if it has no parameters, logging the result or any exceptions that occur.

- `GetTypesInNamespace(Assembly assembly, string namespaceName)`: This helper method takes an assembly and a namespace name as parameters. It returns an array of types that belong to the specified namespace by filtering the types in the assembly.