using UnityEngine;

public class ReasoningController : MonoBehaviour
{
    public TemporalMemory temporalMemory;
    public CognitiveState cognitiveState;

    private float[] queryState; // The state used for retrieval
    private float[] retrievedState;

    void Update()
    {
        if (cognitiveState == null || temporalMemory == null) return;

        // Store the current state in memory
        temporalMemory.StoreState(cognitiveState.GetState());

        // Use reasoning to retrieve a relevant past state
        queryState = GenerateQueryState(); // Could be current cognitive state or a goal state
        retrievedState = temporalMemory.RetrieveStateClosestTo(queryState);

        if (retrievedState != null)
        {
            Debug.Log("Retrieved state: " + string.Join(",", retrievedState));
        }
    }

    private float[] GenerateQueryState()
    {
        // Example: Use the current state as the query
        return cognitiveState.GetState();
    }

    void OnDrawGizmos()
    {
        if (retrievedState != null)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < retrievedState.Length; i++)
            {
                Gizmos.DrawCube(new Vector3(i, retrievedState[i], -1), Vector3.one * 0.1f);
            }
        }
    }
}
