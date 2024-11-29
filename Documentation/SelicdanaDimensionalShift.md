# SelicdanaDimensionalShift

## Overview
The `SelicdanaDimensionalShift` class is designed to manage and organize data categorized by dimensions. It utilizes a dictionary to map integer dimensions to lists of `AizenFlowData` objects. This class serves as a central point for adding and retrieving data based on their respective dimensions, facilitating structured data management within the application.

## Variables
- `_dimensionMap`: A private dictionary that maps integers (representing dimensions) to lists of `AizenFlowData` objects. This structure allows for efficient storage and retrieval of data based on dimension keys.

## Functions
- **SelicdanaDimensionalShift()**: Constructor for the `SelicdanaDimensionalShift` class. It initializes the `_dimensionMap` variable as a new dictionary that will hold dimension keys and their associated lists of `AizenFlowData`.

- **AddData(int dimension, AizenFlowData data)**: This method adds a new `AizenFlowData` object to the list associated with a specified dimension. If the dimension does not already exist in the `_dimensionMap`, it creates a new entry with an empty list before adding the data.

- **GetData(int dimension)**: This method retrieves the list of `AizenFlowData` objects associated with a specified dimension. If the dimension does not exist in the `_dimensionMap`, it returns an empty list, ensuring that the caller does not encounter a null reference.