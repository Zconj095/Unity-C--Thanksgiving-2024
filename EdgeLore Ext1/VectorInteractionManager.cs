using System.Collections.Generic;
using UnityEngine;

public class VectorInteractionManager : MonoBehaviour
{
    private List<Vector3> vectorStates = new List<Vector3>();

    public void AddVectorState(Vector3 state)
    {
        vectorStates.Add(state);

        // Optional: Keep size under control
        if (vectorStates.Count > 100)
            vectorStates.RemoveAt(0);
    }

    public float CalculateInteractionCorrelation()
    {
        if (vectorStates.Count < 2) return 0;

        Vector3 last = vectorStates[vectorStates.Count - 1];
        Vector3 previous = vectorStates[vectorStates.Count - 2];

        return Vector3.Dot(last.normalized, previous.normalized);
    }
}
