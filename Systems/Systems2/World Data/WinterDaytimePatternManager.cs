using System.Collections.Generic;
using UnityEngine;

public class WinterDaytimePatternManager : MonoBehaviour
{
    // Base class for winter daytime patterns
    [System.Serializable]
    public class WinterDaytimePattern
    {
        public string EntityName;      // Name of the entity (e.g., Creature, Tree)
        public float StartHour;        // Start of the daytime window
        public float EndHour;          // End of the daytime window
        public string Description;     // Description of the pattern

        public WinterDaytimePattern(string entityName, float startHour, float endHour, string description)
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
                // Handle patterns that span midnight (unlikely for daytime but included for robustness)
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

    // Lists for specific categories of winter daytime patterns
    public List<WinterDaytimePattern> CreaturePatterns = new List<WinterDaytimePattern>();
    public List<WinterDaytimePattern> TreePatterns = new List<WinterDaytimePattern>();
    public List<WinterDaytimePattern> BerryPatterns = new List<WinterDaytimePattern>();
    public List<WinterDaytimePattern> FlowerPatterns = new List<WinterDaytimePattern>();
    public List<WinterDaytimePattern> ItemPatterns = new List<WinterDaytimePattern>();
    public List<WinterDaytimePattern> MoonPhasePatterns = new List<WinterDaytimePattern>();
    public List<WinterDaytimePattern> WeatherPatterns = new List<WinterDaytimePattern>();
    public List<WinterDaytimePattern> RoutePatterns = new List<WinterDaytimePattern>();
    public List<WinterDaytimePattern> HabitatPatterns = new List<WinterDaytimePattern>();
    public List<WinterDaytimePattern> BiomePatterns = new List<WinterDaytimePattern>();
    public List<WinterDaytimePattern> EcosystemPatterns = new List<WinterDaytimePattern>();
    public List<WinterDaytimePattern> RegionPatterns = new List<WinterDaytimePattern>();

    // Current time simulation state
    public float CurrentHour = 8f; // Example time: 8 AM

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
        CreaturePatterns.Add(new WinterDaytimePattern(
            "Deer Foraging",
            7f, 15f,
            "Deer forage for food during the short winter days."
        ));

        TreePatterns.Add(new WinterDaytimePattern(
            "Snow-Laden Branch Shedding",
            9f, 16f,
            "Snow falls from the branches of trees during warmer winter afternoons."
        ));

        BerryPatterns.Add(new WinterDaytimePattern(
            "Frozen Berry Clusters",
            10f, 14f,
            "Berries remain frozen and visible during the daylight hours of winter."
        ));

        FlowerPatterns.Add(new WinterDaytimePattern(
            "Frost Tulip Growth",
            7f, 13f,
            "Frost tulips grow in the early winter mornings."
        ));

        ItemPatterns.Add(new WinterDaytimePattern(
            "Frozen Crystals Discovery",
            8f, 12f,
            "Rare frozen crystals can be discovered in icy regions during winter mornings."
        ));

        MoonPhasePatterns.Add(new WinterDaytimePattern(
            "Subtle Winter Moonlight",
            6f, 7f,
            "The winter moon subtly influences the early morning before sunrise."
        ));

        WeatherPatterns.Add(new WinterDaytimePattern(
            "Gentle Snowfall",
            8f, 16f,
            "Gentle snow falls during the daylight hours of winter."
        ));

        RoutePatterns.Add(new WinterDaytimePattern(
            "Icy Forest Trails",
            9f, 14f,
            "Forest trails remain icy and challenging to navigate during winter days."
        ));

        HabitatPatterns.Add(new WinterDaytimePattern(
            "Hibernating Bears",
            7f, 15f,
            "Bears remain hibernating, with subtle movements during the warmest daylight hours."
        ));

        BiomePatterns.Add(new WinterDaytimePattern(
            "Snowy Plains",
            8f, 16f,
            "The snowy plains reflect sunlight during the short winter days."
        ));

        EcosystemPatterns.Add(new WinterDaytimePattern(
            "Limited Daylight Activity",
            9f, 14f,
            "Ecosystems adapt to the limited daylight of winter."
        ));

        RegionPatterns.Add(new WinterDaytimePattern(
            "Frosted Peaks",
            7f, 15f,
            "Mountain peaks glisten under the short winter sunlight."
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

    private void CheckActivePatterns(List<WinterDaytimePattern> patterns, string category)
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
