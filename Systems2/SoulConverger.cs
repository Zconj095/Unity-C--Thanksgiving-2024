using System.Collections.Generic;
using UnityEngine;

public class SoulConverger : MonoBehaviour
{
    [System.Serializable]
    public class SoulEntity
    {
        public string Name;
        public float Intensity; // Overall strength of the soul
        public Vector3 Position; // Current position in space
        public Color SoulColor; // Visual representation
    }

    public List<SoulEntity> InputSouls; // Souls to be merged
    public SoulEntity ConvergedSoul; // Resultant soul

    public ParticleSystem ConvergenceEffect; // Effect during merging
    private MeshRenderer visualRepresentation;

    void Start()
    {
        // Initialize the visual representation
        GameObject representation = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        visualRepresentation = representation.GetComponent<MeshRenderer>();
        visualRepresentation.material = new Material(Shader.Find("Standard"));
    }

    public void ConvergeSouls()
    {
        if (InputSouls.Count == 0)
        {
            Debug.LogWarning("No souls to converge.");
            return;
        }

        Vector3 combinedPosition = Vector3.zero;
        Color blendedColor = Color.black;
        float totalIntensity = 0f;

        foreach (var soul in InputSouls)
        {
            combinedPosition += soul.Position * soul.Intensity;
            blendedColor += soul.SoulColor * soul.Intensity;
            totalIntensity += soul.Intensity;
        }

        combinedPosition /= totalIntensity;
        blendedColor /= InputSouls.Count;

        ConvergedSoul = new SoulEntity
        {
            Name = "Converged Soul",
            Intensity = Mathf.Clamp(totalIntensity, 0, 1),
            Position = combinedPosition,
            SoulColor = blendedColor
        };

        UpdateVisualRepresentation();
        TriggerConvergenceEffect();
    }

    private void UpdateVisualRepresentation()
    {
        if (visualRepresentation != null)
        {
            visualRepresentation.material.color = ConvergedSoul.SoulColor;
            visualRepresentation.transform.position = ConvergedSoul.Position;
            visualRepresentation.transform.localScale = Vector3.one * ConvergedSoul.Intensity * 2.0f;
        }
    }

    private void TriggerConvergenceEffect()
    {
        if (ConvergenceEffect != null)
        {
            ConvergenceEffect.transform.position = ConvergedSoul.Position;
            ConvergenceEffect.startColor = ConvergedSoul.SoulColor;
            ConvergenceEffect.Play();
        }
    }

    public void AddSoul(SoulEntity soul)
    {
        InputSouls.Add(soul);
        Debug.Log($"Added soul: {soul.Name}");
    }

    public void RemoveSoul(SoulEntity soul)
    {
        if (InputSouls.Contains(soul))
        {
            InputSouls.Remove(soul);
            Debug.Log($"Removed soul: {soul.Name}");
        }
    }
}
