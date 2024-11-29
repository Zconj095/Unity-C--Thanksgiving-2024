# MiniBatchKMeans

## Overview
The `MiniBatchKMeans` class implements the Mini-Batch K-Means clustering algorithm, which is a variant of the traditional K-Means algorithm. This implementation is designed to handle large datasets more efficiently by processing data in smaller batches. It fits within the broader context of machine learning applications, providing a method for clustering data points into K distinct groups based on their features.

## Variables

- **K**: (int) The number of clusters to form. It must be greater than zero.
- **BatchSize**: (int) The size of the mini-batch used during the fitting process. It must be greater than zero.
- **InitializationBatchSize**: (int?) The size of the batch used for initializing the centroids. It can be set to null, in which case a default value will be used.
- **NumberOfInitializations**: (int) The number of times the algorithm will be initialized with different centroid seeds. Default is 1.
- **Centroids**: (double[][]) An array that holds the coordinates of the centroids for each cluster.
- **Labels**: (int[]) An array that stores the cluster label for each data point.
- **DistanceFunction**: (Func<double[], double[], double>) A function that calculates the distance between two data points. Defaults to the Euclidean distance if not provided.
- **Tolerance**: (double) A threshold value for determining convergence of the centroids. Default is set to 1e-5.

## Functions

- **MiniBatchKMeans(int k, int batchSize, Func<double[], double[], double> distanceFunction = null)**: 
  Constructor that initializes a new instance of the `MiniBatchKMeans` class with the specified number of clusters and batch size. It also accepts an optional distance function.

- **SetInitializationBatchSize(int? size)**: 
  Sets the size of the initialization batch used for the centroids. Throws an exception if the size is less than or equal to zero.

- **SetNumberOfInitializations(int value)**: 
  Sets the number of times the algorithm will run with different initial centroid seeds. Throws an exception if the value is less than or equal to zero.

- **Fit(double[][] data)**: 
  Main method that performs the clustering on the provided dataset. It initializes centroids, assigns data points to the nearest centroids, updates the centroids, and checks for convergence.

- **InitializeCentroids(double[][] data)**: 
  Initializes the centroids by randomly selecting data points and calculating the best initial centroids based on the lowest distortion.

- **CreateBatch(int totalPoints, int batchSize)**: 
  Generates a random batch of indices from the dataset for processing.

- **CalculateDistortion(double[][] data)**: 
  Calculates the total distortion of the current clustering by summing the squared distances between each data point and its nearest centroid.

- **GetNearestCentroid(double[] point)**: 
  Determines the index of the nearest centroid for a given data point by calculating the distance to each centroid.

- **HasConverged(double[][] oldCentroids, double[][] newCentroids)**: 
  Checks if the centroids have converged by comparing the old and new centroid positions against the specified tolerance.

- **EuclideanDistance(double[] pointA, double[] pointB)**: 
  A static method that calculates the Euclidean distance between two points in the feature space.