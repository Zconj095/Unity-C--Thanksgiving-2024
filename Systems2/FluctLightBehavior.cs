using UnityEngine;

public class FluctLightBehavior : MonoBehaviour
{
    [System.Serializable]
    public class FluctLight
    {
        public string Name;
        public float EmotionalIntensity; // Represents emotional state (0-1)
        public float MemoryCapacity; // Simulates memory usage
        public Color BaseColor; // Default glow color
        public Color ActiveColor; // Color during activation
    }

    public FluctLight CurrentFluctLight;
    public ParticleSystem GlowEffect;
    public Light LightAura;

    private float pulseSpeed = 2.0f;
    private bool isActive = false;

    void Start()
    {
        // Initialize the visual representation
        if (GlowEffect != null)
        {
            var main = GlowEffect.main;
            main.startColor = CurrentFluctLight.BaseColor;
        }

        if (LightAura != null)
        {
            LightAura.color = CurrentFluctLight.BaseColor;
            LightAura.intensity = CurrentFluctLight.EmotionalIntensity * 2.0f;
        }
    }

    void Update()
    {
        SimulatePulsing();
    }

    private void SimulatePulsing()
    {
        if (LightAura != null)
        {
            LightAura.intensity = Mathf.PingPong(Time.time * pulseSpeed, CurrentFluctLight.EmotionalIntensity * 3.0f);
        }
    }

    public void ActivateLight()
    {
        isActive = true;

        // Change visual representation
        if (GlowEffect != null)
        {
            var main = GlowEffect.main;
            main.startColor = CurrentFluctLight.ActiveColor;
        }

        if (LightAura != null)
        {
            LightAura.color = CurrentFluctLight.ActiveColor;
            LightAura.intensity = CurrentFluctLight.EmotionalIntensity * 4.0f;
        }
    }

    public void DeactivateLight()
    {
        isActive = false;

        // Reset to base visual representation
        if (GlowEffect != null)
        {
            var main = GlowEffect.main;
            main.startColor = CurrentFluctLight.BaseColor;
        }

        if (LightAura != null)
        {
            LightAura.color = CurrentFluctLight.BaseColor;
            LightAura.intensity = CurrentFluctLight.EmotionalIntensity * 2.0f;
        }
    }

    public void UpdateEmotionalState(float intensity)
    {
        CurrentFluctLight.EmotionalIntensity = Mathf.Clamp(intensity, 0, 1);
        Debug.Log($"{CurrentFluctLight.Name}'s emotional intensity updated to {CurrentFluctLight.EmotionalIntensity}");
    }
}
