using UnityEngine;

public class TrilinearAnalysis : MonoBehaviour
{
    [Header("Input Directions")]
    [SerializeField] private Vector3 inputDirection1 = Vector3.right;
    [SerializeField] private Vector3 inputDirection2 = Vector3.up;
    [SerializeField] private Vector3 inputDirection3 = Vector3.forward;

    [Header("Feedback Coefficients")]
    [SerializeField] private float feedbackCoefficient1 = 1.0f;
    [SerializeField] private float feedbackCoefficient2 = 1.0f;
    [SerializeField] private float feedbackCoefficient3 = 1.0f;

    [Header("Feedback Result (Read Only)")]
    [SerializeField, Tooltip("Resultant vector from the trilinear analysis.")]
    private Vector3 feedbackResult;

    private void Update()
    {
        // Perform the trilinear analysis and calculate feedback
        PerformTrilinearAnalysis();

        // Apply the feedback to the object's position (for demonstration)
        transform.position += feedbackResult * Time.deltaTime;
    }

    private void PerformTrilinearAnalysis()
    {
        // Calculate weighted contributions of the input directions
        Vector3 weightedDirection1 = inputDirection1 * feedbackCoefficient1;
        Vector3 weightedDirection2 = inputDirection2 * feedbackCoefficient2;
        Vector3 weightedDirection3 = inputDirection3 * feedbackCoefficient3;

        // Sum the weighted directions to determine the feedback result
        feedbackResult = weightedDirection1 + weightedDirection2 + weightedDirection3;

        // Normalize the result for consistency (optional)
        feedbackResult = feedbackResult.normalized;
    }

    /// <summary>
    /// Updates the input directions dynamically.
    /// </summary>
    /// <param name="newDirection1">New input direction 1.</param>
    /// <param name="newDirection2">New input direction 2.</param>
    /// <param name="newDirection3">New input direction 3.</param>
    public void UpdateInputDirections(Vector3 newDirection1, Vector3 newDirection2, Vector3 newDirection3)
    {
        inputDirection1 = newDirection1;
        inputDirection2 = newDirection2;
        inputDirection3 = newDirection3;
    }

    /// <summary>
    /// Updates the feedback coefficients dynamically.
    /// </summary>
    /// <param name="newCoefficient1">New coefficient for input direction 1.</param>
    /// <param name="newCoefficient2">New coefficient for input direction 2.</param>
    /// <param name="newCoefficient3">New coefficient for input direction 3.</param>
    public void UpdateFeedbackCoefficients(float newCoefficient1, float newCoefficient2, float newCoefficient3)
    {
        feedbackCoefficient1 = newCoefficient1;
        feedbackCoefficient2 = newCoefficient2;
        feedbackCoefficient3 = newCoefficient3;
    }

    private void OnDrawGizmos()
    {
        // Visualize input directions
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + inputDirection1);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + inputDirection2);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + inputDirection3);

        // Visualize feedback result
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + feedbackResult);
    }
}
