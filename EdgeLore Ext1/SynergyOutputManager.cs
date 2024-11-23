using System.Collections.Generic;
using UnityEngine;
public class SynergyOutputManager : MonoBehaviour
{
    private SynergyInputManager inputManager;

    void Start()
    {
        inputManager = gameObject.AddComponent<SynergyInputManager>();
    }

    void Update()
    {
        // Example: Simulated inputs (replace with actual game data)
        inputManager.AddInput("PlayerHealth", Mathf.Sin(Time.time));
        inputManager.AddInput("EnvironmentFactor", Mathf.Cos(Time.time));
        inputManager.AddInput("AIResponse", Random.Range(0f, 1f));

        // Calculate synergy
        float synergy = SynergyCalculator.CalculateSynergy(inputManager.GetInputs());

        // Apply synergy to game systems
        ApplySynergyEffects(synergy);
    }

    private void ApplySynergyEffects(float synergy)
    {
        Debug.Log($"Synergy Output: {synergy}");

        // Example: Adjust game object scale based on synergy
        transform.localScale = Vector3.one * synergy;

        // Additional effects can include UI updates, particle systems, etc.
    }

    void OnDrawGizmos()
    {
        // Draw a visual indicator of synergy
        float synergy = SynergyCalculator.CalculateSynergy(new Dictionary<string, float>
        {
            { "Input1", Random.Range(0f, 1f) },
            { "Input2", Random.Range(0f, 1f) },
        });

        Gizmos.color = Color.Lerp(Color.green, Color.red, synergy);
        Gizmos.DrawSphere(transform.position, synergy);
    }

}
