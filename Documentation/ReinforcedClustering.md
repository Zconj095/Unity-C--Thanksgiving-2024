# ReinforcedClustering

## Overview
The `ReinforcedClustering` script is designed to manage and reward clusters based on their performance in a Unity application. It maintains a record of rewards associated with different clusters and provides functionalities to reward a specific cluster and retrieve the cluster that has received the highest reward. This script can be utilized in scenarios where clustering is applied, such as in machine learning or game development, to enhance decision-making processes based on cluster performance.

## Variables
- `clusterRewards`: A `Dictionary<int, float>` that stores the total rewards for each cluster. The key is the `clusterId` (an integer representing the unique identifier of the cluster), and the value is the total reward received by that cluster (a float).

## Functions
- `RewardCluster(int clusterId, float reward)`: This method takes two parameters: `clusterId`, which identifies the cluster to be rewarded, and `reward`, which is the amount of reward to add to that cluster. If the specified cluster does not already exist in the `clusterRewards` dictionary, it initializes its reward to zero before adding the specified reward. It also logs a message to the console indicating the cluster and the reward amount.

- `GetBestCluster()`: This method returns the `clusterId` of the cluster that has received the highest total reward. It orders the `clusterRewards` dictionary in descending order based on the reward values and retrieves the first entry, which corresponds to the cluster with the maximum reward. If there are no clusters rewarded yet, it will return the default value of `0`.