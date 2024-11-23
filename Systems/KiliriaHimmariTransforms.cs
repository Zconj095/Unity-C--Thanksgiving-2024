using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
public static class KiliriaHimmariTransforms
{
    // Kiliria Transform: Cross-product-based vector transformation
    public static Vector3 KiliriaTransform(Vector3 vector, Vector3 reference)
    {
        return Vector3.Cross(vector, reference).normalized * vector.magnitude;
    }

    // Himmari Transform: Rotates a vector by a quaternion
    public static Vector3 HimmariTransform(Vector3 vector, Quaternion rotation)
    {
        return rotation * vector;
    }
}

public class ParallelKiliriaHimmari : MonoBehaviour
{
    [BurstCompile]
    private struct KiliriaJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Vector3> InputVectors;
        [ReadOnly] public NativeArray<Vector3> ReferenceVectors;
        public NativeArray<Vector3> OutputVectors;

        public void Execute(int index)
        {
            OutputVectors[index] = KiliriaHimmariTransforms.KiliriaTransform(InputVectors[index], ReferenceVectors[index]);
        }
    }

    [BurstCompile]
    private struct HimmariJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Vector3> InputVectors;
        [ReadOnly] public NativeArray<Quaternion> Rotations;
        public NativeArray<Vector3> OutputVectors;

        public void Execute(int index)
        {
            OutputVectors[index] = KiliriaHimmariTransforms.HimmariTransform(InputVectors[index], Rotations[index]);
        }
    }

    // Test dataset
    private Vector3[] testVectors = new Vector3[10];
    private Vector3[] referenceVectors = new Vector3[10];
    private Quaternion[] rotations = new Quaternion[10];
    private Vector3[] kiliriaResults = new Vector3[10];
    private Vector3[] himmariResults = new Vector3[10];

    void Start()
    {
        // Initialize test data
        for (int i = 0; i < testVectors.Length; i++)
        {
            testVectors[i] = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            referenceVectors[i] = new Vector3(1, 0, 0); // Example reference
            rotations[i] = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }

        ExecuteParallelTransforms();
    }

    private void ExecuteParallelTransforms()
    {
        // Create NativeArrays for jobs
        var inputVectors = new NativeArray<Vector3>(testVectors, Allocator.TempJob);
        var referenceVectorsNative = new NativeArray<Vector3>(referenceVectors, Allocator.TempJob);
        var rotationsNative = new NativeArray<Quaternion>(rotations, Allocator.TempJob);
        var kiliriaOutput = new NativeArray<Vector3>(testVectors.Length, Allocator.TempJob);
        var himmariOutput = new NativeArray<Vector3>(testVectors.Length, Allocator.TempJob);

        // Initialize Jobs
        var kiliriaJob = new KiliriaJob
        {
            InputVectors = inputVectors,
            ReferenceVectors = referenceVectorsNative,
            OutputVectors = kiliriaOutput
        };

        var himmariJob = new HimmariJob
        {
            InputVectors = inputVectors,
            Rotations = rotationsNative,
            OutputVectors = himmariOutput
        };

        // Schedule Jobs
        var kiliriaHandle = kiliriaJob.Schedule(testVectors.Length, 64); // 64: Batch size
        var himmariHandle = himmariJob.Schedule(testVectors.Length, 64);

        // Ensure both jobs complete before proceeding
        JobHandle.CombineDependencies(kiliriaHandle, himmariHandle).Complete();

        // Copy results back to arrays
        kiliriaOutput.CopyTo(kiliriaResults);
        himmariOutput.CopyTo(himmariResults);

        // Dispose of NativeArrays
        inputVectors.Dispose();
        referenceVectorsNative.Dispose();
        rotationsNative.Dispose();
        kiliriaOutput.Dispose();
        himmariOutput.Dispose();

        Debug.Log("Kiliria and Himmari Transforms completed in parallel.");
        for (int i = 0; i < testVectors.Length; i++)
        {
            Debug.Log($"Input: {testVectors[i]}, Kiliria: {kiliriaResults[i]}, Himmari: {himmariResults[i]}");
        }
    }
}
