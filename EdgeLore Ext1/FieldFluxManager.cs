using System.Collections.Generic;
using UnityEngine;

public class FieldFluxManager : MonoBehaviour
{
    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public float[,] fieldValues; // 2D field representation

    void Start()
    {
        // Initialize field with random values
        fieldValues = new float[gridSizeX, gridSizeY];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                fieldValues[x, y] = Random.Range(0f, 1f);
            }
        }
    }
}
