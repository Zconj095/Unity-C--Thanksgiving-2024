# ARSystemStats

## Overview
The `ARSystemStats` script is designed to gather and display system performance statistics, specifically CPU usage and available RAM, within a Unity application. It is particularly tailored for Windows standalone builds, utilizing platform-specific APIs to retrieve system information. The script updates the displayed statistics every second, providing real-time feedback on system performance, which can be useful for debugging or optimizing applications that utilize augmented reality (AR) features.

## Variables
- `cpuText`: A `Text` component that displays the current CPU usage percentage.
- `ramText`: A `Text` component that shows the amount of available RAM in megabytes.
- `prevIdleTime`: A long integer that stores the previous idle time of the CPU for calculating CPU usage.
- `prevKernelTime`: A long integer that stores the previous kernel time for CPU usage calculations.
- `prevUserTime`: A long integer that stores the previous user time for CPU usage calculations.

## Functions
- `Start()`: This Unity lifecycle method is called before the first frame update. It sets up a repeating invocation of the `UpdateStats` method every second, allowing for continuous updates of system statistics.
  
- `UpdateStats()`: This method retrieves the current CPU usage and available RAM, then updates the respective UI text components (`cpuText` and `ramText`) to display the latest statistics.

- `GetCpuUsage()`: This private method calculates the current CPU usage percentage by comparing the current system times (idle, kernel, and user) to their previous values. It computes the percentage of CPU usage based on the difference in these values.

- `GetAvailableRAM()`: This private method retrieves the amount of available physical RAM using the `GlobalMemoryStatusEx` function. It converts the available bytes to megabytes and returns the value.

## Platform-Specific Code
The script contains a conditional compilation directive (`#if UNITY_STANDALONE_WIN`) that ensures the system statistics functionality is only included in Windows standalone builds. If the script is run on any other platform, it will log an error message indicating that system statistics are not supported.

This structure allows the script to maintain compatibility across different platforms while providing valuable system information where applicable.