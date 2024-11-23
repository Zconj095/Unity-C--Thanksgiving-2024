using System.Collections.Generic;
using UnityEngine;

public class SpringtimeNighttimePatternManager : MonoBehaviour
{
    // Base class for spring nighttime patterns
    [System.Serializable]
    public class SpringNighttimePattern
    {
        public string EntityName;      // Name of the entity (e.g., Tree, Creature)
        public float StartHour;        // Start of the nighttime window
        public float EndHour;          // End of the nighttime window
        public string Description;     // Description of the pattern

        public SpringNighttimePattern(string entityName, float startHour, float endHour, string description)
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

    // Lists for specific categories of spring nighttime patterns
    public List<SpringNighttimePattern> TreePatterns = new List<SpringNighttimePattern>();
    public List<SpringNighttimePattern> BerryPatterns = new List<SpringNighttimePattern>();
    public List<SpringNighttimePattern> FlowerPatterns = new List<SpringNighttimePattern>();
    public List<SpringNighttimePattern> ItemPatterns = new List<SpringNighttimePattern>();
    public List<SpringNighttimePattern> MoonPhasePatterns = new List<SpringNighttimePattern>();
    public List<SpringNighttimePattern> WeatherPatterns = new List<SpringNighttimePattern>();
    public List<SpringNighttimePattern> RoutePatterns = new List<SpringNighttimePattern>();
    public List<SpringNighttimePattern> HabitatPatterns = new List<SpringNighttimePattern>();
    public List<SpringNighttimePattern> BiomePatterns = new List<SpringNighttimePattern>();
    public List<SpringNighttimePattern> EcosystemPatterns = new List<SpringNighttimePattern>();
    public List<SpringNighttimePattern> RegionPatterns = new List<SpringNighttimePattern>();
    public List<SpringNighttimePattern> CreaturePatterns = new List<SpringNighttimePattern>();

    // Current time simulation state
    public float CurrentHour = 22f; // Example time: 10 PM

    void Start()
    {
        // Initialize patterns with example data
        InitializePatterns();

        // Check active patterns
        CheckActivePatterns(TreePatterns, "Tree Patterns");
        CheckActivePatterns(CreaturePatterns, "Creature Patterns");
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
        TreePatterns.Add(new SpringNighttimePattern(
            "Cherry Blossom Glow",
            20f, 2f,
            "Cherry blossoms emit a faint glow under the spring moon."
        ));

        BerryPatterns.Add(new SpringNighttimePattern(
            "Wild Berry Ripening",
            22f, 4f,
            "Wild berries ripen rapidly during spring nights."
        ));

        FlowerPatterns.Add(new SpringNighttimePattern(
            "Moonlit Daisies",
            21f, 3f,
            "Daisies bloom under the moonlight in spring."
        ));

        ItemPatterns.Add(new SpringNighttimePattern(
            "Nightly Treasure Hunt",
            19f, 1f,
            "Special treasures are scattered during spring nights."
        ));

        MoonPhasePatterns.Add(new SpringNighttimePattern(
            "Spring Full Moon Glow",
            20f, 23f,
            "The full moon during spring casts a magical light."
        ));

        WeatherPatterns.Add(new SpringNighttimePattern(
            "Spring Night Showers",
            0f, 6f,
            "Light rain showers occur frequently during spring nights."
        ));

        RoutePatterns.Add(new SpringNighttimePattern(
            "Hidden Forest Paths",
            22f, 5f,
            "Hidden paths appear in the forest during spring nights."
        ));

        HabitatPatterns.Add(new SpringNighttimePattern(
            "Frogs in Ponds",
            19f, 2f,
            "Frogs are active in ponds during spring nights."
        ));

        BiomePatterns.Add(new SpringNighttimePattern(
            "Wetland Revival",
            19f, 3f,
            "Wetlands show vibrant activity during spring nights."
        ));

        EcosystemPatterns.Add(new SpringNighttimePattern(
            "Nocturnal Pollinators",
            20f, 4f,
            "Pollinators like moths thrive during spring nights."
        ));

        RegionPatterns.Add(new SpringNighttimePattern(
            "Mountain Glow",
            21f, 2f,
            "Mountains reflect moonlight beautifully during spring nights."
        ));

        CreaturePatterns.Add(new SpringNighttimePattern(
            "Owls Hunting",
            19f, 3f,
            "Owls actively hunt prey during spring nights."
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

    private void CheckActivePatterns(List<SpringNighttimePattern> patterns, string category)
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
