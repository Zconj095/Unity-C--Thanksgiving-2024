using System.Collections.Generic;
using UnityEngine;

public class FieldFluxCalculator : MonoBehaviour
{
    public int gridSizeX = 10; // Width of the grid
    public int gridSizeY = 10; // Height of the grid
    public float[,] fieldValues; // Field data values

    void Start()
    {
        // Initialize the fieldValues array
        fieldValues = new float[gridSizeX, gridSizeY];

        // Optionally populate the fieldValues with initial data
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                fieldValues[x, y] = Random.Range(0f, 1f); // Example initialization
            }
        }
    }

    public static float[,] CalculateFlux(float[,] field)
    {
        int sizeX = field.GetLength(0);
        int sizeY = field.GetLength(1);
        float[,] flux = new float[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                float dx = x > 0 ? field[x, y] - field[x - 1, y] : 0;
                float dy = y > 0 ? field[x, y] - field[x, y - 1] : 0;
                flux[x, y] = Mathf.Sqrt(dx * dx + dy * dy); // Magnitude of change
            }
        }

        return flux;
    }

    void OnDrawGizmos()
    {
        if (fieldValues == null) return;

        Gizmos.color = Color.blue;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Draw a sphere to represent field intensity
                Vector3 position = new Vector3(x, y, 0);
                Gizmos.DrawSphere(position, fieldValues[x, y] * 0.1f);

                // Calculate flux
                float[,] flux = CalculateFlux(fieldValues);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(position, position + new Vector3(flux[x, y], 0, 0));
            }
        }
    }

    void Update()
    {
        // Simulate field changes over time
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                fieldValues[x, y] += Mathf.Sin(Time.time + x + y) * 0.01f;
            }
        }
    }
}
