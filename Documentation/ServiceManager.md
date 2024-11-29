# ServiceManager

## Overview
The `ServiceManager` script is designed to manage various services within a Unity application. It provides functionality to add, start, stop, and remove services, allowing for streamlined control over service states. This script acts as a central hub for service management, making it easier to track and manipulate services throughout the codebase.

## Variables
- `services`: A private dictionary that maps service names (as strings) to their active status (as a boolean). This variable stores the state of each service, indicating whether it is active or inactive.

## Functions
- `AddService(string serviceName)`: Adds a new service to the `services` dictionary. If the service already exists, it logs an error message. By default, the service is considered active upon addition.

- `StartService(string serviceName)`: Starts a specified service. If the service is already active, it logs a message stating that the service is running. If the service is inactive, it activates the service and logs that it has started. If the service does not exist, it logs an error message.

- `StopService(string serviceName)`: Stops a specified service. If the service is already inactive, it logs a message indicating that the service is stopped. If the service is active, it deactivates the service and logs that it has stopped. If the service does not exist, it logs an error message.

- `RemoveService(string serviceName)`: Removes a specified service from the `services` dictionary. If the service is successfully removed, it logs a success message. If the service does not exist, it logs an error message.