# FrequencyFluxAnalyzer

## Overview
The `FrequencyFluxAnalyzer` script is designed to analyze data points representing frequency fluctuations over time. It simulates data input and evaluates these data points based on defined thresholds to detect high, low, or normal flux patterns. This script is typically used in a Unity environment where it can be attached to GameObjects to monitor frequency data in real-time.

## Variables
- **dataPoints**: A list of floats that stores the data points collected for analysis.
- **upperThreshold**: A float that represents the upper limit for variance; if the variance exceeds this value, it indicates high flux.
- **lowerThreshold**: A float that represents the lower limit for variance; if the variance is below this value, it indicates low flux.
- **analysisPeriod**: A float that defines the time interval (in seconds) for analyzing the data points. The default is set to 1.0 seconds.
- **timer**: A float that keeps track of the elapsed time since the last analysis was performed.

## Functions
- **Update()**: This method is called once per frame. It increments the timer based on the time elapsed since the last frame. It simulates data input using a sine function and adds this data to the `dataPoints` list. When the timer exceeds the `analysisPeriod`, it triggers the analysis of the data and resets the timer.

- **AddData(float value)**: This public method allows new data points to be added to the `dataPoints` list. It also includes an optional feature to limit the size of the list to prevent memory overload by removing the oldest data point if the count exceeds 1000.

- **AnalyzeData()**: This private method processes the collected data points to calculate the mean and variance. It then compares the variance against the defined thresholds to determine if the flux is high, low, or within a normal range. Depending on the result, it logs the appropriate message to the console. After analysis, it clears the `dataPoints` list to prepare for the next set of data.