using UnityEngine;

[System.Serializable]
public class CoreCognitiveState
{
    public string Name;
    public float Intensity; // Range 0-1
    public Color StateColor;

    public CoreCognitiveState(string name, float intensity, Color color)
    {
        Name = name;
        Intensity = Mathf.Clamp(intensity, 0, 1);
        StateColor = color;
    }

    public void AdjustIntensity(float delta)
    {
        Intensity = Mathf.Clamp(Intensity + delta, 0, 1);
    }

    public override string ToString()
    {
        return $"{Name} (Intensity: {Intensity})";
    }
}
