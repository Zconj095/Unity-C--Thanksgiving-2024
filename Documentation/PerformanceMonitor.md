# PerformanceMonitor

## Overview
The `PerformanceMonitor` script is designed to track and log the memory usage and processing time of a Unity application during runtime. It is attached to a GameObject and updates its metrics every frame in the `Update` method. This functionality helps developers monitor the performance of their game or application, allowing them to identify potential bottlenecks or memory issues.

## Variables
- `memoryUsage`: A float variable that stores the total memory used by the application, measured in megabytes (MB). It is calculated using the `System.GC.GetTotalMemory` method.
- `processingTime`: A float variable that represents the time taken to process the current frame, measured in milliseconds (ms). It is derived from the `Time.deltaTime` property, which provides the time since the last frame.

## Functions
- `Update()`: This method is called once per frame. It calculates the current memory usage and processing time, then logs these metrics to the console using `Debug.Log`. The memory usage is converted from bytes to megabytes, and the processing time is converted from seconds to milliseconds.