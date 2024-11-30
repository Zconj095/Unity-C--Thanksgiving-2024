# BagOfWordsStatistics

## Overview
The `BagOfWordsStatistics` class is designed to capture and analyze statistics related to the Bag of Words model in a machine learning context, specifically adapted for use in Unity. This class stores various metrics about the training data, including the number of instances and descriptors, as well as their distribution characteristics. It provides utility functions for calculating statistical measures such as mean, variance, and range, which are essential for understanding the performance and characteristics of the learning process.

## Variables

- **TotalNumberOfInstances**: An integer that represents the total number of instances (e.g., images or audio signals) in the training set.
  
- **TotalNumberOfDescriptors**: An integer that indicates the total number of descriptors seen in the training set.
  
- **TotalNumberOfDescriptorsPerInstance**: A `Vector2` that holds the average (X) and variance (Y) of descriptors per instance, replacing the NormalDistribution with Unity-compatible statistics.
  
- **TotalNumberOfDescriptorsPerInstanceRange**: A `Vector2Int` that defines the minimum (X) and maximum (Y) number of descriptors per instance seen in the training set.
  
- **NumberOfInstancesTaken**: An integer that indicates the number of instances actually utilized in the learning process.
  
- **NumberOfDescriptorsTaken**: An integer that represents the number of descriptors actually used in the learning process.
  
- **NumberOfDescriptorsTakenPerInstance**: A `Vector2` that contains the average (X) and variance (Y) of descriptors actually used per instance.
  
- **NumberOfDescriptorsTakenPerInstanceRange**: A `Vector2Int` that specifies the minimum (X) and maximum (Y) number of descriptors per instance that were actually used in the learning process.

## Functions

- **CalculateMeanAndVariance(int[] data)**: 
  - This static method calculates the mean and variance of a given set of integers. It takes an array of integers as input and returns a `Vector2`, where X represents the mean and Y represents the variance. It throws an `ArgumentException` if the input data is null or empty.

- **CalculateMinMax(int[] data)**: 
  - This static method computes the minimum and maximum values from a provided set of integers. It accepts an array of integers and returns a `Vector2Int`, where X is the minimum value and Y is the maximum value. Like the previous method, it throws an `ArgumentException` if the input data is null or empty.