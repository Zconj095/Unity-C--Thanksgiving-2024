# HyperfluxStorageContainer

## Overview
The `HyperfluxStorageContainer` class is designed to manage the storage of `AizenFlowData` objects, allowing for the addition, retrieval, and cleanup of stored data. It ensures that only relevant data is retained by implementing a time-based decay mechanism, which automatically removes data that exceeds a specified age. This class is essential for maintaining a clean and efficient dataset within the larger codebase, especially in scenarios where data relevance is time-sensitive.

## Variables
- `_storage`: A private list that holds instances of `AizenFlowData`. This variable stores all the data entries until they are either retrieved or removed during the cleanup process.
- `_timeDecay`: A `TimeSpan` variable that defines the maximum age of the data entries. It determines how long data can remain in the storage before being considered outdated and eligible for removal.

## Functions
- `HyperfluxStorageContainer(TimeSpan timeDecay)`: Constructor that initializes a new instance of the `HyperfluxStorageContainer` class. It sets up the storage list and specifies the time decay period for data entries.

- `void StoreData(AizenFlowData data)`: This method allows the addition of a new `AizenFlowData` object to the `_storage` list. It is used to store incoming data for future retrieval.

- `void Cleanup()`: This method removes all `AizenFlowData` entries from the `_storage` list that have a timestamp older than the specified `_timeDecay`. It helps maintain the integrity and relevance of the stored data by ensuring that outdated entries are eliminated.

- `IEnumerable<AizenFlowData> RetrieveAll()`: This method first calls `Cleanup()` to remove outdated data and then returns all currently stored `AizenFlowData` objects as an enumerable collection. It provides access to the active dataset for further processing or analysis.