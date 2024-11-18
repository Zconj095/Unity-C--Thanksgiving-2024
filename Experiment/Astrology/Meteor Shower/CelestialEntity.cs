using UnityEngine;
using System.Collections.Generic;
using Enviro; // Assuming the namespace is defined in MeteorShowerEvent.cs

public class CelestialEntity : MonoBehaviour
{
    public string entityName;
    public float frequency;
    public float resonance;
    public float powerLevel;
    public Vector3 influenceZoneCenter;
    public float influenceRadius;
    public Dictionary<string, float> affirmations = new Dictionary<string, float>();

    private void Start()
    {
        influenceZoneCenter = transform.position;
        influenceRadius = CalculateInfluenceRadius();
    }

    private float CalculateInfluenceRadius()
    {
        return Mathf.Sqrt(powerLevel * resonance) * 10.0f;
    }

    public void UpdateInfluenceZone()
    {
        influenceRadius = CalculateInfluenceRadius();
    }

    public float GetIntentLevel()
    {
        float intentLevel = 0;
        foreach (var affirmation in affirmations.Values)
        {
            intentLevel += affirmation;
        }
        return intentLevel;
    }

    public void ResetIntentLevel()
    {
        affirmations.Clear();
    }
}