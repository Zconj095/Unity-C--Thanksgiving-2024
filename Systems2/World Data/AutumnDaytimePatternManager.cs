using System.Collections.Generic;
using UnityEngine;

public class AutumnDaytimePatternManager : MonoBehaviour
{
    // Base class for autumn daytime patterns
    [System.Serializable]
    public class AutumnDaytimePattern
    {
        public string EntityName;      // Name of the entity (e.g., Creature, Tree)
        public float StartHour;        // Start of the daytime window
        public float EndHour;          // End of the daytime window
        public string Description;     // Description of the pattern

        public AutumnDaytimePattern(string entityName, float startHour, float endHour, string description)
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

    // Lists for specific categories of autumn daytime patterns
    public List<AutumnDaytimePattern> CreaturePatterns = new List<AutumnDaytimePattern>();
    public List<AutumnDaytimePattern> TreePatterns = new List<AutumnDaytimePattern>();
    public List<AutumnDaytimePattern> BerryPatterns = new List<AutumnDaytimePattern>();
    public List<AutumnDaytimePattern> FlowerPatterns = new List<AutumnDaytimePattern>();
    public List<AutumnDaytimePattern> ItemPatterns = new List<AutumnDaytimePattern>();
    public List<AutumnDaytimePattern> MoonPhasePatterns = new List<AutumnDaytimePattern>();
    public List<AutumnDaytimePattern> WeatherPatterns = new List<AutumnDaytimePattern>();
    public List<AutumnDaytimePattern> RoutePatterns = new List<AutumnDaytimePattern>();
    public List<AutumnDaytimePattern> HabitatPatterns = new List<AutumnDaytimePattern>();
    public List<AutumnDaytimePattern> BiomePatterns = new List<AutumnDaytimePattern>();
    public List<AutumnDaytimePattern> EcosystemPatterns = new List<AutumnDaytimePattern>();
    public List<AutumnDaytimePattern> RegionPatterns = new List<AutumnDaytimePattern>();

    // Current time simulation state
    public float CurrentHour = 10f; // Example time: 10 AM

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
        CreaturePatterns.Add(new AutumnDaytimePattern(
            "Squirrels Gathering Nuts",
            8f, 16f,
            "Squirrels actively gather nuts during autumn days."
        ));

        TreePatterns.Add(new AutumnDaytimePattern(
            "Maple Leaves Falling",
            9f, 17f,
            "Maple trees shed their colorful leaves throughout the day in autumn."
        ));

        BerryPatterns.Add(new AutumnDaytimePattern(
            "Cranberry Growth",
            10f, 14f,
            "Cranberries grow rapidly during autumn daylight hours."
        ));

        FlowerPatterns.Add(new AutumnDaytimePattern(
            "Autumn Marigold Bloom",
            7f, 15f,
            "Marigolds bloom under the gentle autumn sun."
        ));

        ItemPatterns.Add(new AutumnDaytimePattern(
            "Seasonal Decorations",
            10f, 16f,
            "Special autumn decorations appear during the daytime."
        ));

        MoonPhasePatterns.Add(new AutumnDaytimePattern(
            "Harvest Moon Effects",
            12f, 13f,
            "The harvest moon subtly influences the environment during autumn days."
        ));

        WeatherPatterns.Add(new AutumnDaytimePattern(
            "Cool Breezes",
            11f, 18f,
            "Gentle cool breezes sweep through the land during autumn days."
        ));

        RoutePatterns.Add(new AutumnDaytimePattern(
            "Forest Path Visibility",
            8f, 18f,
            "Forest paths are most visible under autumn daylight."
        ));

        HabitatPatterns.Add(new AutumnDaytimePattern(
            "Bird Migration",
            9f, 17f,
            "Birds begin their migration patterns during autumn daylight hours."
        ));

        BiomePatterns.Add(new AutumnDaytimePattern(
            "Forest Color Changes",
            10f, 16f,
            "Forests undergo vivid color changes during autumn days."
        ));

        EcosystemPatterns.Add(new AutumnDaytimePattern(
            "Wetland Activity",
            7f, 13f,
            "Wetlands are most vibrant during the early autumn mornings."
        ));

        RegionPatterns.Add(new AutumnDaytimePattern(
            "Mountain Mist",
            9f, 12f,
            "Mist blankets mountain regions during the morning hours of autumn."
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

    private void CheckActivePatterns(List<AutumnDaytimePattern> patterns, string category)
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
