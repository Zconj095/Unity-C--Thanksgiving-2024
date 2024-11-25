using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ReinforcedClustering
{
    private Dictionary<int, float> clusterRewards = new Dictionary<int, float>();

    public void RewardCluster(int clusterId, float reward)
    {
        if (!clusterRewards.ContainsKey(clusterId))
        {
            clusterRewards[clusterId] = 0;
        }
        clusterRewards[clusterId] += reward;
        Debug.Log($"Cluster {clusterId} rewarded with {reward}");
    }

    public int GetBestCluster()
    {
        return clusterRewards.OrderByDescending(c => c.Value).FirstOrDefault().Key;
    }
}
