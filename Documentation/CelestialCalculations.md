# CelestialCalculations

## Overview
The `CelestialCalculations` script is designed to perform various astronomical and astrological calculations while also managing celestial clocks to track time in relation to these calculations. It serves as a foundation for simulating cosmic phenomena and their effects, allowing developers to explore celestial mechanics within a Unity environment.

## Variables
- **astronomicalCalculation**: An instance of the `Astronomical` class that holds the parameters and methods for performing astronomical calculations.
- **astrologicalCalculation**: An instance of the `Astrological` class that holds the parameters and methods for performing astrological calculations.
- **astronomicalClock**: An instance of the `CelestialClock` class that tracks time for astronomical events.
- **astrologicalClock**: An instance of the `CelestialClock` class that tracks time for astrological events.

## Functions
### Start()
This method is called when the script is first executed. It initializes the instances of the `Astronomical`, `Astrological`, and `CelestialClock` classes with specific parameters. It also performs the initial calculations for both astronomical and astrological events and updates the clocks to display their starting times.

### Update()
This method is called once per frame. It simulates real-time updates to the celestial clocks. Every 60 frames (approximately every second), it updates the time for both the astronomical and astrological clocks, logging their current times and the time of day (daytime or nighttime) based on the clock's current time.

## Classes
### Astronomical
- **Name**: The name of the astronomical calculation.
- **Description**: A brief description of what the calculation entails.
- **PerformCalculation()**: Logs the name and description of the astronomical calculation and simulates the computation of massive-scale values related to the universe.

### Astrological
- **Name**: The name of the astrological calculation.
- **Description**: A brief description of what the calculation involves.
- **PerformCalculation()**: Logs the name and description of the astrological calculation and simulates the determination of quantum-based outcomes.

### CelestialClock
- **Name**: The name of the celestial clock.
- **Description**: A brief description of the purpose of the clock.
- **CurrentTime**: A float representing the current time in hours (ranging from 0.0 to 24.0).
- **TimeStep**: A float representing the increment in hours per update.
- **UpdateTime()**: Increments the current time by the specified time step and wraps around to maintain a 24-hour format. Logs the current time after updating.
- **GetTimeFrame()**: Returns a string indicating whether it is "Daytime" (6:00 to 17:59) or "Nighttime" (18:00 to 5:59) based on the current time.