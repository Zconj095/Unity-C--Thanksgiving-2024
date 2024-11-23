using UnityEngine;

public class CorticalActivity : MonoBehaviour
{
    public int gridSize = 10; // A 10x10 grid for simplicity
    private float[,] neuralActivity;

    void Start()
    {
        neuralActivity = new float[gridSize, gridSize];
        InitializeActivity();
    }

    void InitializeActivity()
    {
        // Randomize initial cortical activity
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                neuralActivity[x, y] = Random.Range(0.1f, 1.0f);
            }
        }
    }

    public float[,] GetActivity()
    {
        return neuralActivity;
    }

    public void UpdateActivity(float[,] newActivity)
    {
        neuralActivity = newActivity;
    }

    void OnDrawGizmos()
    {
        if (neuralActivity == null) return;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Gizmos.color = Color.Lerp(Color.black, Color.white, neuralActivity[x, y]);
                Gizmos.DrawCube(new Vector3(x, 0, y), Vector3.one * 0.9f);
            }
        }
    }
}
