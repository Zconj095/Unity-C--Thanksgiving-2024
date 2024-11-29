# Gaussian Mixture Model

## Overview
The `GaussianMixtureModel` script implements a Gaussian Mixture Model (GMM) for clustering data points in a multi-dimensional space. It utilizes a collection of Gaussian distributions (clusters) to model the data, allowing for the identification of underlying patterns. This script is part of the `EdgeLoreMachineLearning` namespace and works in conjunction with the `KMeansClustering` class to initialize clusters before refining them through the Expectation-Maximization (EM) algorithm. The main function of the script is to learn from provided data and adjust the parameters of the Gaussian clusters to best fit the data.

## Variables

### GaussianMixtureModel Class
- **Clusters**: A list of `GaussianCluster` objects representing the different clusters in the model.
- **Tolerance**: A double value that determines the threshold for convergence during the EM algorithm.
- **MaxIterations**: An integer that sets the maximum number of iterations for the EM algorithm.
- **Initializations**: An integer that defines how many times the algorithm will be initialized with different starting points.
- **ComputeLabels**: A boolean indicating whether to compute labels for the data points based on cluster assignments.
- **ComputeLogLikelihood**: A boolean indicating whether to compute the log-likelihood of the model.
- **LogLikelihood**: A double value that stores the log-likelihood of the model at the current iteration.

### GaussianCluster Class
- **Mean**: An array of doubles representing the mean of the Gaussian cluster.
- **Covariance**: A 2D array of doubles representing the covariance matrix of the Gaussian cluster.
- **Coefficient**: A double value that represents the weight of the cluster in the mixture.

### KMeansClustering Class
- **K**: An integer representing the number of clusters.
- **Centroids**: A 2D array of doubles representing the centroids of the clusters.
- **Covariances**: A 3D array of doubles representing the covariance matrices for each cluster.
- **Weights**: An array of doubles representing the weights of each cluster.
- **data**: A 2D array of doubles that holds the input data for clustering.

## Functions

### GaussianMixtureModel Class
- **GaussianMixtureModel(int numberOfClusters)**: Constructor that initializes the GMM with a specified number of clusters and sets up empty cluster placeholders.
- **Learn(double[][] data, double[] weights = null)**: Main method for training the GMM on the provided data. It performs initial K-Means clustering and then runs the Expectation-Maximization algorithm to refine the clusters.
- **InitializeEmptyClusters(int numberOfClusters)**: Private method that initializes empty Gaussian clusters with uniform coefficients.
- **RunExpectationMaximization(double[][] data, double[] weights)**: Private method that implements the Expectation-Maximization algorithm to update the cluster parameters based on the data and weights.

### GaussianCluster Class
- **GaussianCluster(double[] mean, double[,] covariance, double coefficient)**: Constructor that initializes a Gaussian cluster with a specified mean, covariance matrix, and coefficient.
- **LogLikelihood(double[] x)**: Public method that computes the log-likelihood of a given data point based on the cluster's parameters.
- **CalculateLogLikelihood(double[] x, double[] mean, double[,] covariance, double coefficient)**: Private method that calculates the log-likelihood using Mahalanobis distance, determinant, and log probability density function for Gaussian distributions.
- **CalculateDeterminant(double[,] matrix)**: Private method that serves as a placeholder for calculating the determinant of the covariance matrix.

### KMeansClustering Class
- **KMeansClustering(int k, double[][] data)**: Constructor that initializes the KMeansClustering with a specified number of clusters (k) and the input data.
- **Run()**: Method that executes the K-Means clustering algorithm to find initial centroids and calculates weights and covariance placeholders for each cluster.