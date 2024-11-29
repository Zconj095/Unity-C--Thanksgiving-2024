# BayesianField

## Overview
The `BayesianField` script is a Unity component that implements a simple Bayesian inference model. It calculates and updates posterior probabilities based on prior probabilities and observed data using Bayes' theorem. This script is useful in scenarios where probabilistic reasoning is needed, such as in AI decision-making or data analysis within the Unity game engine. It initializes with uniform prior probabilities for different bands and allows for updating these probabilities based on new observations.

## Variables

- **priorProbabilities**: A dictionary that holds the initial prior probabilities for different bands (e.g., delta, theta, alpha, beta, gamma). Each band is associated with a float value representing its probability.
  
- **posteriorProbabilities**: A dictionary that stores the updated posterior probabilities for the bands after observing new data. It is initialized with the same values as `priorProbabilities`.

## Functions

- **BayesianField()**: Constructor that initializes the `priorProbabilities` with uniform values of 0.2 for each band. It also initializes the `posteriorProbabilities` to be the same as `priorProbabilities`.

- **UpdatePosterior(Dictionary<string, float> observedData)**: This function takes a dictionary of observed data, calculates the likelihood for each band using an exponential function, and updates the `posteriorProbabilities` accordingly. It normalizes the posterior probabilities so that they sum to 1.

- **GetPosterior()**: This function returns the current state of the `posteriorProbabilities` dictionary, allowing other components or scripts to access the updated probabilities after observations have been made.