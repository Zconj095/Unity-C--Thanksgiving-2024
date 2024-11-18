using UnityEngine;

public class CelestialEvent : MonoBehaviour
{
    public string eventName;
    public float eventDuration;
    public float eventMagnitude;
    public AnimationCurve eventCurve;

    public virtual void ApplyEventInfluence(CelestialEntity entity)
    {
        // 1. Calculate Event Intensity
        float timeElapsed = Time.timeSinceLevelLoad; // Assuming the event starts at level load
        float normalizedTime = Mathf.Clamp01(timeElapsed / eventDuration);
        float intensity = eventCurve.Evaluate(normalizedTime) * eventMagnitude;

        // 2. Apply Influence based on Entity Properties
        float influenceFactor = intensity * entity.resonance; 

        // 3. Example: Modify Power Level
        entity.powerLevel += influenceFactor; 
        entity.UpdateInfluenceZone(); // Update the entity's influence zone

        // 4. (Optional) Trigger Visual/Sound Effects
        // You can add code here to trigger particle effects, sounds, etc.
        // based on the event and entity properties.
    }
}