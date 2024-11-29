# HinizagiTransform

## Overview
The `HinizagiTransform` script is designed to manage and analyze a collection of events in a 3D space within the Unity game engine. It encapsulates functionality for clustering events based on their spatial origins, performing temporal correlations, and predicting future outcomes using time series analysis. This script serves as a core component of the overall codebase, allowing for complex event analysis and visualization, which can be essential for game mechanics or simulations that rely on event data.

## Variables
- `_events`: A list of `GalacticEvent` instances that stores all the events to be analyzed.
- `_kMeansCluster`: An instance of the `KMeansCluster` class used for clustering the events based on their spatial origins.
- `_predictor`: An instance of the `TimeSeriesPredictor` class that is responsible for predicting future values based on a time series.

## Functions
- `Start()`: Initializes the script by creating an empty list for events, setting up the KMeans clustering with 3 clusters, and initializing the time series predictor.

- `Update()`: Called once per frame. It checks for user input (specifically the space key) to add a sample event and perform the Hinizagi Transform.

- `PerformHinizagiTransform()`: Executes the main functionality of the script, which includes clustering events, performing temporal correlations, and predicting future outcomes.

- `AddSampleEvent()`: Generates a new `GalacticEvent` with random attributes and adds it to the `_events` list.

- `ClusterEvents()`: Performs KMeans clustering on the `_events` list. It logs the details of the clusters and each event within the clusters.

- `VisualizeClusters()`: Visualizes the centroids of the clusters in the Unity scene by drawing spheres at their positions.

- `PerformTemporalCorrelations()`: Calculates and logs the cosine and tangent similarities between all pairs of events in the `_events` list.

- `PredictFutureOutcomes()`: Uses the `TimeSeriesPredictor` to generate future predictions based on a sample time series and logs the predicted values.