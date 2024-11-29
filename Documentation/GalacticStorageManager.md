# GalacticStorageManager

## Overview
The `GalacticStorageManager` class is responsible for managing the storage of galactic data across different dimensions within a Unity application. It acts as an intermediary that facilitates the addition and retrieval of data while also ensuring that the storage remains efficient over time. The class utilizes two main components: `SelicdanaDimensionalShift`, which manages the dimensional data organization, and `HyperfluxStorageContainer`, which handles the storage of data with a defined expiration policy. This class plays a critical role in maintaining the integrity and accessibility of the data stored in the application.

## Variables
- `_dimensionalShift`: An instance of `SelicdanaDimensionalShift` that is responsible for managing data across different dimensions.
- `_hyperfluxStorage`: An instance of `HyperfluxStorageContainer` that stores data with a specified expiration time (10 minutes in this case).

## Functions
- `void Start()`: Initializes the `_dimensionalShift` and `_hyperfluxStorage` instances when the script starts. This function sets up the necessary components for data management.
  
- `public void AddGalacticData(int dimension, string key, object value)`: Adds a new piece of galactic data to the specified dimension. It creates an `AizenFlowData` object with the provided key and value, along with the current timestamp. The data is then added to the `_dimensionalShift` and stored in `_hyperfluxStorage`.

- `public List<AizenFlowData> GetDimensionData(int dimension)`: Retrieves a list of `AizenFlowData` objects associated with the specified dimension. This allows other parts of the codebase to access stored data.

- `void Update()`: A Unity-specific method that is called once per frame. This function periodically invokes the cleanup process of the `_hyperfluxStorage`, ensuring that outdated data is removed and storage efficiency is maintained.