using UnityEngine;

public class AlignmentController : MonoBehaviour
{
    public CorticalActivity corticalActivity;
    public float learningRate = 0.01f;

    private float[,] targetPattern;

    void Start()
    {
        GenerateTargetPattern();
    }

    void GenerateTargetPattern()
    {
        // Create a target pattern for alignment
        targetPattern = new float[corticalActivity.gridSize, corticalActivity.gridSize];
        for (int x = 0; x < corticalActivity.gridSize; x++)
        {
            for (int y = 0; y < corticalActivity.gridSize; y++)
            {
                targetPattern[x, y] = Mathf.Sin((float)x / corticalActivity.gridSize * Mathf.PI) *
                                      Mathf.Cos((float)y / corticalActivity.gridSize * Mathf.PI);
            }
        }
    }

    void Update()
    {
        AlignCorticalActivity();
    }

    void AlignCorticalActivity()
    {
        float[,] currentActivity = corticalActivity.GetActivity();
        float[,] newActivity = new float[corticalActivity.gridSize, corticalActivity.gridSize];

        // Gradient descent for alignment
        for (int x = 0; x < corticalActivity.gridSize; x++)
        {
            for (int y = 0; y < corticalActivity.gridSize; y++)
            {
                float error = currentActivity[x, y] - targetPattern[x, y];
                newActivity[x, y] = currentActivity[x, y] - learningRate * error;
            }
        }

        corticalActivity.UpdateActivity(newActivity);
    }

    void OnDrawGizmos()
    {
        if (targetPattern == null) return;

        // Visualize the target pattern
        Gizmos.color = Color.blue;
        for (int x = 0; x < corticalActivity.gridSize; x++)
        {
            for (int y = 0; y < corticalActivity.gridSize; y++)
            {
                float height = targetPattern[x, y];
                Gizmos.DrawCube(new Vector3(x, -1, y), new Vector3(0.9f, 0.1f, 0.9f) * height);
            }
        }
    }
}
