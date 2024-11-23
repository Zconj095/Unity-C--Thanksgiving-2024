using System.Collections.Generic;
using UnityEngine;

public class SummertimeNighttimePatternManager : MonoBehaviour
{
    // Base class for summertime nighttime patterns
    [System.Serializable]
    public class SummerNighttimePattern
    {
        public string EntityName;      // Name of the entity (e.g., Creature, Tree)
        public float StartHour;        // Start of the nighttime window
        public float EndHour;          // End of the nighttime window
        public string Description;     // Description of the pattern

        public SummerNighttimePattern(string entityName, float startHour, float endHour, string description)
        {
            EntityName = entityName;
            StartHour = startHour;
            EndHour = endHour;
            Description = description;
        }

        public bool IsActive(float currentHour)
        {
            if (StartHour <= EndHour)
            {
                return currentHour >= StartHour && currentHour <= EndHour;
            }
            else
            {
                // Handle patterns that span midnight
                return currentHour >= StartHour || currentHour <= EndHour;
            }
        }

        public void DisplayDetails()
        {
            Debug.Log($"Entity: {EntityName}");
            Debug.Log($"Active From: {StartHour}:00 to {EndHour}:00");
            Debug.Log($"Description: {Description}");
        }
    }

    // Lists for specific categories of summertime nighttime patterns
    public List<SummerNighttimePattern> CreaturePatterns = new List<SummerNighttimePattern>();
    public List<SummerNighttimePattern> TreePatterns = new List<SummerNighttimePattern>();
    public List<SummerNighttimePattern> BerryPatterns = new List<SummerNighttimePattern>();
    public List<SummerNighttimePattern> FlowerPatterns = new List<SummerNighttimePattern>();
    public List<SummerNighttimePattern> ItemPatterns = new List<SummerNighttimePattern>();
    public List<SummerNighttimePattern> MoonPhasePatterns = new List<SummerNighttimePattern>();
    public List<SummerNighttimePattern> WeatherPatterns = new List<SummerNighttimePattern>();
    public List<SummerNighttimePattern> RoutePatterns = new List<SummerNighttimePattern>();
    public List<SummerNighttimePattern> HabitatPatterns = new List<SummerNighttimePattern>();
    public List<SummerNighttimePattern> BiomePatterns = new List<SummerNighttimePattern>();
    public List<SummerNighttimePattern> EcosystemPatterns = new List<SummerNighttimePattern>();
    public List<SummerNighttimePattern> RegionPatterns = new List<SummerNighttimePattern>();

    // Current time simulation state
    public float CurrentHour = 22f; // Example time: 10 PM

    void Start()
    {
        // Initialize patterns with example data
        InitializePatterns();

        // Check active patterns
        CheckActivePatterns(CreaturePatterns, "Creature Patterns");
        CheckActivePatterns(TreePatterns, "Tree Patterns");
    }

    void Update()
    {
        // Simulate time progression
        SimulateTime();

        // Dynamically check active patterns
        CheckActivePatterns(FlowerPatterns, "Flower Patterns");
        CheckActivePatterns(WeatherPatterns, "Weather Patterns");
    }

    private void InitializePatterns()
    {
        CreaturePatterns.Add(new SummerNighttimePattern(
            "Fireflies in Meadows",
            20f, 3f,
            "Fireflies light up meadows during summer nights."
        ));

        TreePatterns.Add(new SummerNighttimePattern(
            "Moonlit Cypress Growth",
            21f, 2f,
            "Cypress trees grow under the moonlight during summer nights."
        ));

        BerryPatterns.Add(new SummerNighttimePattern(
            "Blackberry Ripening",
            23f, 5f,
            "Blackberries ripen steadily throughout summer nights."
        ));

        FlowerPatterns.Add(new SummerNighttimePattern(
            "Night Blooming Water Lilies",
            22f, 4f,
            "Water lilies bloom beautifully under the summer moonlight."
        ));

        ItemPatterns.Add(new SummerNighttimePattern(
            "Hidden Summer Relics",
            19f, 1f,
            "Special relics appear during summer nights in specific regions."
        ));

        MoonPhasePatterns.Add(new SummerNighttimePattern(
            "Full Moon Summer Glow",
            20f, 23f,
            "The summer full moon casts a warm glow on the environment."
        ));

        WeatherPatterns.Add(new SummerNighttimePattern(
            "Tropical Night Rains",
            0f, 5f,
            "Light rains are frequent during tropical summer nights."
        ));

        RoutePatterns.Add(new SummerNighttimePattern(
            "Moonlit Forest Paths",
            21f, 4f,
            "Forest paths are illuminated under the moonlight during summer."
        ));

        HabitatPatterns.Add(new SummerNighttimePattern(
            "Nocturnal Creatures in Forests",
            20f, 5f,
            "Nocturnal creatures thrive in forests during summer nights."
        ));

        BiomePatterns.Add(new SummerNighttimePattern(
            "Wetlands Nighttime Revival",
            19f, 3f,
            "Wetlands show vibrant activity during summer nights."
        ));

        EcosystemPatterns.Add(new SummerNighttimePattern(
            "Nighttime Pollinators",
            20f, 4f,
            "Pollinators such as moths thrive during summer nights."
        ));

        RegionPatterns.Add(new SummerNighttimePattern(
            "Mountain Starry Nights",
            21f, 2f,
            "Stars are most visible in mountain regions during summer nights."
        ));
    }

    private void SimulateTime()
    {
        // Increment current hour (1 second = 1 in-game minute)
        CurrentHour += Time.deltaTime / 60f;

        // Wrap around the hour if it exceeds 24
        if (CurrentHour >= 24f)
            CurrentHour -= 24f;
    }

    private void CheckActivePatterns(List<SummerNighttimePattern> patterns, string category)
    {
        Debug.Log($"--- Checking {category} ---");
        foreach (var pattern in patterns)
        {
            if (pattern.IsActive(CurrentHour))
            {
                Debug.Log($"Active Pattern: {pattern.EntityName} - {pattern.Description}");
            }
        }
    }
}
