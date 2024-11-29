# ARServiceManager

## Overview
The `ARServiceManager` class is responsible for managing various services in an augmented reality (AR) application within the Unity game engine. It allows for the addition, removal, starting, and stopping of services, providing a simple interface for managing service states. This class fits into the overall codebase by serving as a centralized point for service management, ensuring that services can be controlled efficiently and effectively.

## Variables
- **services**: A `Dictionary<string, bool>` that stores the state of each service. The key is the name of the service (a string), and the value is a boolean indicating whether the service is currently running (`true`) or stopped (`false`).

## Functions
- **StartService(string serviceName)**: Starts the specified service if it exists and is not already running. If the service is already running, it logs a message indicating that. If the service does not exist, it logs an error message.

- **StopService(string serviceName)**: Stops the specified service if it exists and is currently running. If the service is already stopped, it logs a message indicating that. If the service does not exist, it logs an error message.

- **AddService(string serviceName)**: Adds a new service to the manager. The service is initially set to a stopped state. If the service already exists, it logs an error message indicating that the service cannot be added.

- **RemoveService(string serviceName)**: Removes the specified service from the manager. If the service is successfully removed, it logs a success message. If the service does not exist, it logs an error message.