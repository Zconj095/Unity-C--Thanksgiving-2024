# MemoryRetrievalSystem

## Overview
The `MemoryRetrievalSystem` class is designed to facilitate the retrieval of relevant vectors from a database based on a given query. It interacts with a `SynergyEngine` instance to find similar vectors and allows the user to specify how many of the top relevant results they wish to retrieve. This functionality is crucial in scenarios where quick access to similar data is required, such as in machine learning applications or data analysis within the Unity game engine.

## Variables

- `synergyEngine`: An instance of the `SynergyEngine` class, which is responsible for finding similar vectors based on the input query.

## Functions

### RetrieveRelevantVectors
```csharp
public List<(int dbIndex, int vectorId, float score)> RetrieveRelevantVectors(float[] query, MultiDatabaseManager dbManager, int topN = 5)
```
- **Parameters**:
  - `query`: An array of floats representing the input query vector for which similar vectors are to be retrieved.
  - `dbManager`: An instance of `MultiDatabaseManager`, which manages access to multiple databases.
  - `topN`: An optional integer parameter that specifies the number of top similar vectors to return, defaulting to 5.
  
- **Returns**: A list of tuples, where each tuple contains:
  - `dbIndex`: The index of the database from which the vector is retrieved.
  - `vectorId`: The identifier of the vector.
  - `score`: A float representing the similarity score of the vector relative to the query.

- **Description**: This function utilizes the `synergyEngine` to find vectors that are similar to the input `query` by calling the `FindSimilarVectors` method. It then takes the top `N` results from this list and returns them as a list of tuples, making it easy to access both the identifiers and the similarity scores associated with the retrieved vectors.