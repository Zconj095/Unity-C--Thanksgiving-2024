using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CrossCorrelation3D : MonoBehaviour
{
    [Header("3D Volume")]
    [SerializeField] private List<List<List<float>>> volume1 = new List<List<List<float>>> {
        new List<List<float>> {
            new List<float>{ 1f, 2f },
            new List<float>{ 3f, 4f }
        },
        new List<List<float>> {
            new List<float>{ 5f, 6f },
            new List<float>{ 7f, 8f }
        }
    };

    [Header("3D Kernel")]
    [SerializeField] private List<List<List<float>>> kernel = new List<List<List<float>>> {
        new List<List<float>> {
            new List<float>{ 1f, 0f },
            new List<float>{ 0f, -1f }
        },
        new List<List<float>> {
            new List<float>{ 1f, 0f },
            new List<float>{ 0f, -1f }
        }
    };

    private void Start()
    {
        if (!IsValidInput(volume1, kernel))
        {
            Debug.LogError("Invalid input: Ensure the volume and kernel have consistent 3D structures.");
            return;
        }

        // Convert Lists to 3D arrays for processing
        float[,,] volume1Array = ConvertTo3DArray(volume1);
        float[,,] kernelArray = ConvertTo3DArray(kernel);

        // Compute Cross-Correlation for the 3D volume and kernel
        float[,,] result = ComputeCrossCorrelation3D(volume1Array, kernelArray);

        // Output the result to the console
        Debug.Log("3D Cross Correlation Result:");
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                string row = "";
                for (int k = 0; k < result.GetLength(2); k++)
                {
                    row += result[i, j, k].ToString("F2") + " "; // Format to 2 decimal places
                }
                Debug.Log(row);
            }
        }
    }

    // Validate if the input List<List<List<float>>> structures are consistent
    private bool IsValidInput(List<List<List<float>>> volume, List<List<List<float>>> kernel)
    {
        if (volume == null || kernel == null || volume.Count == 0 || kernel.Count == 0) return false;

        int volumeDepth = volume[0].Count > 0 ? volume[0][0].Count : 0;
        int kernelDepth = kernel[0].Count > 0 ? kernel[0][0].Count : 0;

        foreach (var plane in volume)
        {
            if (plane.Count == 0 || plane[0].Count != volumeDepth) return false;
        }

        foreach (var plane in kernel)
        {
            if (plane.Count == 0 || plane[0].Count != kernelDepth) return false;
        }

        return true;
    }

    // Convert List<List<List<float>>> to 3D array for processing
    private float[,,] ConvertTo3DArray(List<List<List<float>>> list)
    {
        int x = list.Count;
        int y = list[0].Count;
        int z = list[0][0].Count;
        float[,,] array = new float[x, y, z];

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    array[i, j, k] = list[i][j][k];
                }
            }
        }

        return array;
    }

    // Function to compute the 3D cross-correlation between a volume and a kernel
    private float[,,] ComputeCrossCorrelation3D(float[,,] volume, float[,,] kernel)
    {
        int volumeX = volume.GetLength(0);
        int volumeY = volume.GetLength(1);
        int volumeZ = volume.GetLength(2);

        int kernelX = kernel.GetLength(0);
        int kernelY = kernel.GetLength(1);
        int kernelZ = kernel.GetLength(2);

        if (kernelX > volumeX || kernelY > volumeY || kernelZ > volumeZ)
        {
            Debug.LogError("Kernel dimensions are larger than the volume dimensions. Cross-correlation not possible.");
            return new float[0, 0, 0];
        }

        int outputX = volumeX - kernelX + 1;
        int outputY = volumeY - kernelY + 1;
        int outputZ = volumeZ - kernelZ + 1;

        float[,,] correlation = new float[outputX, outputY, outputZ];

        // Perform the 3D cross-correlation
        for (int i = 0; i < outputX; i++)
        {
            for (int j = 0; j < outputY; j++)
            {
                for (int k = 0; k < outputZ; k++)
                {
                    float sum = 0f;
                    for (int m = 0; m < kernelX; m++)
                    {
                        for (int n = 0; n < kernelY; n++)
                        {
                            for (int p = 0; p < kernelZ; p++)
                            {
                                sum += volume[i + m, j + n, k + p] * kernel[m, n, p];
                            }
                        }
                    }
                    correlation[i, j, k] = sum;
                }
            }
        }

        return correlation;
    }
}
