# FastMeanShift

## Overview
The `FastMeanShift` class implements the Fast Mean Shift clustering algorithm, which is compatible with Unity. This algorithm is used for clustering points in a three-dimensional space, allowing for the identification of modes (or clusters) within a set of data points. The clustering process is crucial for various applications in machine learning, computer vision, and data analysis, as it helps to group similar data points together based on their spatial proximity.

## Variables
- `bandwidth`: A float that controls the smoothness of the clustering. It defines the radius within which points are considered for clustering. Must be greater than zero.
- `tolerance`: A float that sets the convergence criterion for shifting points toward modes. It determines how close a point must be to a mode before stopping the iterative process.
- `maxIterations`: An integer that specifies the maximum number of iterations allowed for the mean shift algorithm to converge to a mode.
- `seeds`: A list of `Vector3` objects that holds the initial seed points for the clustering process. These seeds are derived from the input points.
- `modes`: A list of `Vector3` objects that contains the identified modes (clusters) after the clustering process.
- `useParallelProcessing`: A boolean that indicates whether to use parallel processing for the mean shift algorithm, which can enhance performance on large datasets.

## Functions
- `public float Bandwidth`: A property that gets or sets the `bandwidth` variable. It throws an `ArgumentOutOfRangeException` if the new value is less than or equal to zero.

- `public int[] Compute(Vector3[] points)`: The main function that executes the clustering process. It takes an array of `Vector3` points, initializes the seeds, performs the mean shift algorithm (either in parallel or sequentially), and assigns cluster labels to each input point. Returns an array of integers representing the cluster labels.

- `private List<Vector3> InitializeSeeds(Vector3[] points)`: Initializes the seed points by binning the input points into a grid based on the bandwidth. Returns a list of unique seed points.

- `private void PerformMeanShift(Vector3[] points)`: Executes the mean shift algorithm iteratively for each seed point to find the modes. The results are stored in the `modes` list.

- `private void PerformMeanShiftParallel(Vector3[] points)`: Similar to `PerformMeanShift`, but utilizes parallel processing to improve performance. It finds modes concurrently and updates the `modes` list.

- `private Vector3 ShiftToMode(Vector3 point, Vector3[] points)`: Shifts a given point iteratively toward the nearest mode using the mean shift algorithm. It returns the mode to which the point converges.

- `private Vector3 ComputeMean(Vector3 center, Vector3[] points)`: Calculates the mean position of points within the specified bandwidth radius from a given center point. Returns the mean vector or the original center if no points are within the radius.

- `private int[] AssignLabels(Vector3[] points)`: Assigns cluster labels to each input point based on the closest mode. Returns an array of integers representing the cluster labels corresponding to the input points.

- `private Vector3 RoundVector(Vector3 vector, float gridSize)`: Rounds a given vector to the nearest grid point defined by the bandwidth. Returns the rounded vector.