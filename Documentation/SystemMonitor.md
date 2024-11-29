# SystemMonitor

## Overview
The `SystemMonitor` script is designed to provide a real-time display of CPU and RAM usage statistics within a Unity application. It updates these statistics every second, allowing developers and users to monitor system performance dynamically. While the script includes mock data for CPU and RAM usage, it sets a foundation that can be expanded upon to incorporate actual system metrics if needed. This script fits into the larger codebase by serving as a utility for performance monitoring, enhancing user experience through transparency of system resource usage.

## Variables

- **cpuUsageText**: A `Text` component that displays the current CPU usage as a percentage. This is updated every second with new mock data.
  
- **ramUsageText**: A `Text` component that shows the available RAM in megabytes. This value is calculated based on a mock usage value subtracted from the total system memory.
  
- **mockCpuUsage**: A `float` variable that holds a randomly generated value representing the mock CPU usage percentage. This simulates CPU load for demonstration purposes.

## Functions

- **Start()**: This Unity lifecycle method is called when the script is first initialized. It sets up a repeating invocation of the `UpdateStats` method, which occurs every second. This ensures that the CPU and RAM usage statistics are updated regularly.

- **UpdateStats()**: This method is responsible for updating the CPU and RAM usage statistics. It performs the following tasks:
  - Generates a random mock value for CPU usage.
  - Retrieves the total system memory using `SystemInfo.systemMemorySize`.
  - Calculates available RAM by subtracting a randomly generated mock usage value from the total memory.
  - Updates the `cpuUsageText` and `ramUsageText` UI components with the new values. If the respective text components are not null, it formats and displays the current CPU usage and available RAM.