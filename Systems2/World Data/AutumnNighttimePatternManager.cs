using System.Collections.Generic;
using UnityEngine;

public class AutumnNighttimePatternManager : MonoBehaviour
{
    // Base class for autumn nighttime patterns
    [System.Serializable]
    public class AutumnNighttimePattern
    {
        public string EntityName;      // Name of the entity (e.g., Creature, Tree)
        public float StartHour;        // Start of the nighttime window
        public float EndHour;          // End of the nighttime window
        public string Description;     // Description of the pattern

        public AutumnNighttimePattern(string entityName, float startHour, float endHour, string description)
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

    // Lists for specific categories of autumn nighttime patterns
    public List<AutumnNighttimePattern> CreaturePatterns = new List<AutumnNighttimePattern>();
    public List<AutumnNighttimePattern> TreePatterns = new List<AutumnNighttimePattern>();
    public List<AutumnNighttimePattern> BerryPatterns = new List<AutumnNighttimePattern>();
    public List<AutumnNighttimePattern> FlowerPatterns = new List<AutumnNighttimePattern>();
    public List<AutumnNighttimePattern> ItemPatterns = new List<AutumnNighttimePattern>();
    public List<AutumnNighttimePattern> MoonPhasePatterns = new List<AutumnNighttimePattern>();
    public List<AutumnNighttimePattern> WeatherPatterns = new List<AutumnNighttimePattern>();
    public List<AutumnNighttimePattern> RoutePatterns = new List<AutumnNighttimePattern>();
    public List<AutumnNighttimePattern> HabitatPatterns = new List<AutumnNighttimePattern>();
    public List<AutumnNighttimePattern> BiomePatterns = new List<AutumnNighttimePattern>();
    public List<AutumnNighttimePattern> EcosystemPatterns = new List<AutumnNighttimePattern>();
    public List<AutumnNighttimePattern> RegionPatterns = new List<AutumnNighttimePattern>();

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
        CreaturePatterns.Add(new AutumnNighttimePattern(
            "Owls Hunting",
            20f, 4f,
            "Owls actively hunt small prey during autumn nights."
        ));

        TreePatterns.Add(new AutumnNighttimePattern(
            "Moonlit Birch Leaves",
            22f, 2f,
            "Birch trees glow faintly under the moonlight in autumn."
        ));

        BerryPatterns.Add(new AutumnNighttimePattern(
            "Cranberry Ripening",
            23f, 5f,
            "Cranberries ripen steadily during cool autumn nights."
        ));

        FlowerPatterns.Add(new AutumnNighttimePattern(
            "Autumn Jasmine Bloom",
            21f, 3f,
            "Jasmine flowers emit their fragrance during autumn nights."
        ));

        ItemPatterns.Add(new AutumnNighttimePattern(
            "Hidden Autumn Treasures",
            19f, 1f,
            "Special treasures appear during the night in autumn forests."
        ));

        MoonPhasePatterns.Add(new AutumnNighttimePattern(
            "Harvest Moonlight Effects",
            20f, 23f,
            "The harvest moon casts a golden glow during autumn nights."
        ));

        WeatherPatterns.Add(new AutumnNighttimePattern(
            "Cool Autumn Winds",
            0f, 6f,
            "Gentle cool winds sweep across the fields during autumn nights."
        ));

        RoutePatterns.Add(new AutumnNighttimePattern(
            "Dimly Lit Forest Paths",
            21f, 4f,
            "Forest paths are dimly lit during autumn nights."
        ));

        HabitatPatterns.Add(new AutumnNighttimePattern(
            "Nocturnal Forest Creatures",
            20f, 5f,
            "Nocturnal animals thrive in the forest during autumn nights."
        ));

        BiomePatterns.Add(new AutumnNighttimePattern(
            "Wetland Evening Activity",
            19f, 3f,
            "Wetlands show vibrant activity during autumn nights."
        ));

        EcosystemPatterns.Add(new AutumnNighttimePattern(
            "Nocturnal Pollination",
            20f, 4f,
            "Pollinators such as moths are active during autumn nights."
        ));

        RegionPatterns.Add(new AutumnNighttimePattern(
            "Mountain Star Visibility",
            21f, 2f,
            "Stars are most visible in mountain regions during autumn nights."
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

    private void CheckActivePatterns(List<AutumnNighttimePattern> patterns, string category)
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
