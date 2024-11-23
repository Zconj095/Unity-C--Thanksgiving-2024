using System.Collections.Generic;
using UnityEngine;

public class VectorChainManager : MonoBehaviour
{
    private List<Vector3> vectorChain = new List<Vector3>();

    public void AddVector(Vector3 newVector)
    {
        vectorChain.Add(newVector);

        // Limit chain length for optimization
        if (vectorChain.Count > 100)
            vectorChain.RemoveAt(0);

        CheckForSynergy();
    }

    private void CheckForSynergy()
    {
        // Example: Detect if vectors form a certain pattern
        if (vectorChain.Count >= 3)
        {
            Vector3 v1 = vectorChain[vectorChain.Count - 3];
            Vector3 v2 = vectorChain[vectorChain.Count - 2];
            Vector3 v3 = vectorChain[vectorChain.Count - 1];

            // Check for alignment
            if (Vector3.Dot(v1.normalized, v2.normalized) > 0.9f &&
                Vector3.Dot(v2.normalized, v3.normalized) > 0.9f)
            {
                Debug.Log("Synergy detected in vector chain!");
            }
        }
    }
}
