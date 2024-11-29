using UnityEngine;

public class ReadOnlyInterjection : MonoBehaviour
{
    public Transform observer; // The observer's position in space.
    public float detectionRadius = 5.0f; // Radius of the local space to monitor.
    public LayerMask targetLayer; // Layer of objects to observe.
    public bool debugDraw = true; // Toggle for visualizing the monitored space.

    void Update()
    {
        ReadLocalSpace();
    }

    void ReadLocalSpace()
    {
        // Get all colliders in the local space (sphere-shaped).
        Collider[] colliders = Physics.OverlapSphere(observer.position, detectionRadius, targetLayer);

        foreach (var collider in colliders)
        {
            // Read information about each object in the local space.
            GameObject target = collider.gameObject;
            Vector3 position = target.transform.position;
            string objectName = target.name;

            // Log or process the data.
            Debug.Log($"Observed Object: {objectName}, Position: {position}");
        }
    }

    void OnDrawGizmos()
    {
        // Visualize the detection area in the editor (optional).
        if (debugDraw && observer != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(observer.position, detectionRadius);
        }
    }
}
