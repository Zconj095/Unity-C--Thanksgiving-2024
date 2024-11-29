# DataSourceTracker

## Overview
The `DataSourceTracker` class is a Unity script that manages a collection of metadata associated with various data sources. It allows for the addition of new data sources, retrieval of a specific data source by its identifier, and fetching all data sources in a dictionary format. This functionality is essential for keeping track of where specific pieces of data originate from within the broader codebase, promoting better organization and traceability of data sources.

## Variables
- `sourceMetadata`: A dictionary that stores the relationship between data identifiers (keys) and their corresponding data sources (values). This variable is crucial for maintaining the mapping of data to its origin.

## Functions
- `DataSourceTracker()`: Constructor for the `DataSourceTracker` class. It initializes the `sourceMetadata` dictionary to ensure that it is ready to store data sources upon instantiation.

- `AddSource(string dataId, string source)`: This method takes a data identifier (`dataId`) and a corresponding source (`source`) as parameters. It adds or updates the entry in the `sourceMetadata` dictionary, allowing for the association of a specific data source with its identifier.

- `GetSource(string dataId)`: This method retrieves the data source associated with the provided data identifier (`dataId`). If the identifier exists in the `sourceMetadata`, it returns the corresponding source; otherwise, it returns the string "Unknown Source", indicating that no source was found for the given identifier.

- `GetAllSources()`: This method returns the entire `sourceMetadata` dictionary, allowing access to all stored data sources and their identifiers. This is useful for bulk operations or for displaying all sources in the application.