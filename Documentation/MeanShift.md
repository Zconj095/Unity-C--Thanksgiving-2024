# MeanShift Class

## Overview
The `MeanShift` class implements the Mean Shift clustering algorithm specifically for Unity. This algorithm is used to identify clusters in a set of data points in a multidimensional space. The main function of this class is to add data points, run the clustering algorithm to find cluster centers (modes), and assign each point to its respective cluster based on proximity to these centers. This class fits into the larger codebase by providing a method for unsupervised learning, enabling the identification of patterns or groupings within spatial data.

## Variables
- `bandwidth`: A `float` that defines the radius for clustering. It determines the neighborhood size used to identify clusters.
- `distanceFunc`: A `Func<Vector3, Vector3, float>` that represents the distance function used to calculate the distance between two points. This can be customized (e.g., to use Euclidean distance).
- `points`: A `List<Vector3>` that stores the data points added for clustering.
- `modes`: A `List<Vector3>` that holds the identified cluster centers (modes) after running the algorithm.

## Functions
- **MeanShift(float bandwidth, Func<Vector3, Vector3, float> distanceFunc)**: Constructor that initializes the Mean Shift algorithm with a specified bandwidth and distance function. It throws an exception if the bandwidth is not positive or if the distance function is null.

- **void AddPoints(IEnumerable<Vector3> dataPoints)**: Adds a collection of data points to the `MeanShift` algorithm for clustering.

- **void Run(int maxIterations = 100, float tolerance = 0.01f)**: Executes the clustering algorithm. It iteratively updates the positions of points based on their neighbors until convergence is achieved or the maximum number of iterations is reached. It throws an exception if no data points have been added.

- **IEnumerable<Vector3> GetModes()**: Returns the list of cluster centers (modes) identified by the algorithm.

- **Dictionary<Vector3, int> AssignClusters()**: Assigns each point in the dataset to a cluster based on its proximity to the identified modes. It returns a dictionary where the keys are the data points and the values are the corresponding cluster indices.