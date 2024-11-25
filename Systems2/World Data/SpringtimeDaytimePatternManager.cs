using System.Collections.Generic;
using UnityEngine;

public class SpringtimeDaytimePatternManager : MonoBehaviour
{
    // Base class for springtime daytime patterns
    [System.Serializable]
    public class SpringtimeDaytimePattern
    {
        public string EntityName;      // Name of the entity (e.g., Tree, Berry)
        public float StartHour;        // Start of the daytime window
        public float EndHour;          // End of the daytime window
        public string Description;     // Description of the pattern

        public SpringtimeDaytimePattern(string entityName, float startHour, float endHour, string description)
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
                // Handle cases where the pattern spans over midnight
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

    // Lists for various categories of springtime patterns
    public List<SpringtimeDaytimePattern> TreePatterns = new List<SpringtimeDaytimePattern>();
    public List<SpringtimeDaytimePattern> BerryPatterns = new List<SpringtimeDaytimePattern>();
    public List<SpringtimeDaytimePattern> FlowerPatterns = new List<SpringtimeDaytimePattern>();
    public List<SpringtimeDaytimePattern> ItemPatterns = new List<SpringtimeDaytimePattern>();
    public List<SpringtimeDaytimePattern> MoonPhasePatterns = new List<SpringtimeDaytimePattern>();
    public List<SpringtimeDaytimePattern> WeatherPatterns = new List<SpringtimeDaytimePattern>();
    public List<SpringtimeDaytimePattern> RoutePatterns = new List<SpringtimeDaytimePattern>();
    public List<SpringtimeDaytimePattern> HabitatPatterns = new List<SpringtimeDaytimePattern>();
    public List<SpringtimeDaytimePattern> BiomePatterns = new List<SpringtimeDaytimePattern>();
    public List<SpringtimeDaytimePattern> EcosystemPatterns = new List<SpringtimeDaytimePattern>();
    public List<SpringtimeDaytimePattern> RegionPatterns = new List<SpringtimeDaytimePattern>();

    // Current simulation state
    public float CurrentHour = 10f; // Example time: 10 AM

    void Start()
    {
        // Initialize example patterns
        InitializePatterns();

        // Check for active patterns
        CheckActivePatterns(TreePatterns, "Tree Patterns");
        CheckActivePatterns(BerryPatterns, "Berry Patterns");
    }

    void Update()
    {
        // Simulate time progression
        SimulateTime();

        // Check active patterns dynamically
        CheckActivePatterns(FlowerPatterns, "Flower Patterns");
        CheckActivePatterns(WeatherPatterns, "Weather Patterns");
    }

    private void InitializePatterns()
    {
        TreePatterns.Add(new SpringtimeDaytimePattern(
            "Oak Tree Bloom",
            6f, 18f,
            "Oak trees produce vibrant blooms during spring daytime hours."
        ));

        BerryPatterns.Add(new SpringtimeDaytimePattern(
            "Strawberry Growth",
            8f, 16f,
            "Strawberries grow rapidly under spring sun."
        ));

        FlowerPatterns.Add(new SpringtimeDaytimePattern(
            "Tulip Blossom",
            7f, 12f,
            "Tulips reach full bloom in the morning during spring."
        ));

        MoonPhasePatterns.Add(new SpringtimeDaytimePattern(
            "Waxing Crescent Effect",
            10f, 14f,
            "The waxing crescent moon influences spring daytime activities."
        ));

        WeatherPatterns.Add(new SpringtimeDaytimePattern(
            "Morning Showers",
            6f, 9f,
            "Light morning showers occur during spring mornings."
        ));

        RoutePatterns.Add(new SpringtimeDaytimePattern(
            "Forest Trail Visibility",
            6f, 18f,
            "Forest trails become clear and vibrant during spring days."
        ));

        HabitatPatterns.Add(new SpringtimeDaytimePattern(
            "Meadow Habitat Activity",
            9f, 17f,
            "Meadow habitats flourish with activity in spring days."
        ));

        BiomePatterns.Add(new SpringtimeDaytimePattern(
            "Temperate Forest Bloom",
            6f, 18f,
            "Temperate forests burst into bloom during spring days."
        ));

        EcosystemPatterns.Add(new SpringtimeDaytimePattern(
            "Wetlands Renewal",
            7f, 15f,
            "Spring renews wetlands, enhancing biodiversity."
        ));

        RegionPatterns.Add(new SpringtimeDaytimePattern(
            "Mountain Mist",
            6f, 9f,
            "Springtime mornings bring mist to mountain regions."
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

    private void CheckActivePatterns(List<SpringtimeDaytimePattern> patterns, string category)
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
