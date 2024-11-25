using System;
using System.Collections.Generic;
using UnityEngine;

public class EnviroPatterns : MonoBehaviour
{
    // Enumeration for Lunar and Solar patterns
    public enum PatternType
    {
        Lunar,
        Solar
    }

    // Data structure to hold pattern details
    [Serializable]
    public class CelestialPattern
    {
        public PatternType Type;      // Lunar or Solar
        public string Name;           // Name of the pattern
        public string Description;    // Description of the pattern
        public float StartHour;       // Time when the pattern starts
        public float EndHour;         // Time when the pattern ends
        public string Effects;        // Environmental effects (e.g., light level, temperature)

        public CelestialPattern(PatternType type, string name, string description, float startHour, float endHour, string effects)
        {
            Type = type;
            Name = name;
            Description = description;
            StartHour = startHour;
            EndHour = endHour;
            Effects = effects;
        }

        public void DisplayDetails()
        {
            Debug.Log($"Pattern: {Name} ({Type})");
            Debug.Log($"Description: {Description}");
            Debug.Log($"Active from {StartHour}:00 to {EndHour}:00");
            Debug.Log($"Effects: {Effects}");
        }
    }

    // Example data for patterns
    private List<CelestialPattern> celestialPatterns = new List<CelestialPattern>();

    void Start()
    {
        // Define some lunar and solar patterns
        celestialPatterns.Add(new CelestialPattern(
            PatternType.Lunar,
            "Full Moon Night",
            "Bright moonlight affects nocturnal activities and visibility.",
            21f, // 9 PM
            5f,  // 5 AM
            "Increased visibility, cooler temperature"
        ));

        celestialPatterns.Add(new CelestialPattern(
            PatternType.Solar,
            "Solar Noon",
            "Peak sunlight influencing temperature and shadow length.",
            12f, // Noon
            13f, // 1 PM
            "Maximum temperature, high light intensity"
        ));

        celestialPatterns.Add(new CelestialPattern(
            PatternType.Lunar,
            "New Moon Night",
            "Minimal moonlight impacts nocturnal visibility and activity.",
            20f, // 8 PM
            6f,  // 6 AM
            "Reduced visibility, colder temperatures"
        ));

        // Display pattern details
        foreach (var pattern in celestialPatterns)
        {
            pattern.DisplayDetails();
        }
    }

    // Function to simulate effects of patterns based on the current time
    public void SimulatePatterns(float currentHour)
    {
        Debug.Log($"Simulating patterns for time: {currentHour}:00");

        foreach (var pattern in celestialPatterns)
        {
            if (IsPatternActive(pattern, currentHour))
            {
                Debug.Log($"Active Pattern: {pattern.Name}");
                Debug.Log($"Effects: {pattern.Effects}");
            }
        }
    }

    // Helper function to check if a pattern is active
    private bool IsPatternActive(CelestialPattern pattern, float currentHour)
    {
        if (pattern.StartHour <= pattern.EndHour)
        {
            return currentHour >= pattern.StartHour && currentHour <= pattern.EndHour;
        }
        else
        {
            // Handle patterns that span midnight
            return currentHour >= pattern.StartHour || currentHour <= pattern.EndHour;
        }
    }
}
