# GaussianClusterCollection

## Overview
The `GaussianClusterCollection` class is designed to manage a collection of Gaussian clusters used in machine learning, specifically for clustering tasks. Each cluster represents a Gaussian distribution characterized by its mean, variance, and covariance. This class allows for the initialization of these clusters and provides access to their parameters. It also implements the `IEnumerable` interface, enabling iteration over the contained clusters.

## Variables
- `private List<GaussianCluster> clusters`: A list that holds instances of `GaussianCluster`, representing the collection of clusters.
- `private double[][] means`: A two-dimensional array storing the means of each Gaussian cluster.
- `private double[][] variances`: A two-dimensional array storing the variances of each Gaussian cluster.
- `private double[][,] covariances`: An array of two-dimensional arrays storing the covariance matrices for each Gaussian cluster.
- `private double[] coefficients`: An array that holds the coefficients (weights) for each Gaussian cluster, representing their proportions in the overall mixture.

## Functions
- `public GaussianClusterCollection(int components)`: Constructor that initializes a new instance of `GaussianClusterCollection` with a specified number of components (clusters). It creates and adds the corresponding `GaussianCluster` instances to the `clusters` list.

- `public double[] Proportions`: Property that returns the coefficients of the Gaussian clusters, representing their proportions in the mixture.

- `public double[][] Means`: Property that returns the means of the Gaussian clusters.

- `public double[][] Variances`: Property that returns the variances of the Gaussian clusters.

- `public double[][,] Covariances`: Property that returns the covariance matrices of the Gaussian clusters.

- `public void Initialize(double[] initialCoefficients, double[][] initialMeans, double[][] initialVariances, double[][,] initialCovariances)`: Method that initializes the Gaussian clusters with given coefficients, means, variances, and covariances. It updates each cluster's data accordingly.

- `public IEnumerator<GaussianCluster> GetEnumerator()`: Method that returns an enumerator for iterating over the clusters.

- `IEnumerator IEnumerable.GetEnumerator()`: Explicit interface implementation that returns the enumerator for the collection.

### GaussianCluster Class
- `public class GaussianCluster`: Represents an individual Gaussian cluster within the `GaussianClusterCollection`.

#### Variables
- `private GaussianClusterCollection owner`: A reference to the parent `GaussianClusterCollection` that owns this cluster.
- `private int index`: The index of this cluster within the collection.
- `private double[] mean`: The mean of the Gaussian distribution for this cluster.
- `private double[] variance`: The variance of the Gaussian distribution for this cluster.
- `private double[,] covariance`: The covariance matrix of the Gaussian distribution for this cluster.
- `private double coefficient`: The weight of this cluster in the overall mixture.

#### Functions
- `public GaussianCluster(GaussianClusterCollection owner, int index)`: Constructor that initializes a new instance of `GaussianCluster`, associating it with its owner collection and setting its index.

- `public double[] Mean`: Property that returns the mean of the cluster.

- `public double[] Variance`: Property that returns the variance of the cluster.

- `public double[,] Covariance`: Property that returns the covariance matrix of the cluster.

- `public double Coefficient`: Property that returns the coefficient (weight) of the cluster.

- `public void UpdateData(double[] newMean, double[] newVariance, double[,] newCovariance, double newCoefficient)`: Method that updates the cluster's parameters with new data.

- `public double LogLikelihood(double[] x)`: Method that calculates the log-likelihood of a given data point `x` based on the Gaussian distribution of this cluster.

- `public double Likelihood(double[] x)`: Method that calculates the likelihood of a given data point `x`, which is the exponential of the log-likelihood.

- `private double CalculateLogLikelihood(double[] x, double[] mean, double[,] covariance, double weight)`: Private method that computes the log-likelihood using the Mahalanobis distance and the properties of the Gaussian distribution.

- `private double[,] InvertMatrix(double[,] matrix)`: Private placeholder method for matrix inversion (to be implemented using appropriate math libraries).

- `private double CalculateDeterminant(double[,] matrix)`: Private placeholder method for calculating the determinant of a matrix (to be implemented using appropriate math libraries).