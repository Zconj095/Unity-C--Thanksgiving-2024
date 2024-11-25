using System.Collections.Generic;
using UnityEngine;

public class SeasonalNighttimePatternManager : MonoBehaviour
{
    // Enum for Seasons
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    // Base class for seasonal nighttime patterns
    [System.Serializable]
    public class SeasonalNighttimePattern
    {
        public string EntityName;      // Name of the entity (e.g., Creature, Tree)
        public Season ActiveSeason;    // Season when the pattern is active
        public float StartHour;        // Start of the nighttime window
        public float EndHour;          // End of the nighttime window
        public string Description;     // Description of the pattern

        public SeasonalNighttimePattern(string entityName, Season activeSeason, float startHour, float endHour, string description)
        {
            EntityName = entityName;
            ActiveSeason = activeSeason;
            StartHour = startHour;
            EndHour = endHour;
            Description = description;
        }

        public bool IsActive(float currentHour, Season currentSeason)
        {
            // Check if the pattern matches the current season
            if (currentSeason != ActiveSeason)
                return false;

            // Check if the current hour falls within the pattern's active time
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
            Debug.Log($"Entity: {EntityName}, Season: {ActiveSeason}");
            Debug.Log($"Active From: {StartHour}:00 to {EndHour}:00");
            Debug.Log($"Description: {Description}");
        }
    }

    // Lists of patterns for various categories
    public List<SeasonalNighttimePattern> CreaturePatterns = new List<SeasonalNighttimePattern>();
    public List<SeasonalNighttimePattern> TreePatterns = new List<SeasonalNighttimePattern>();
    public List<SeasonalNighttimePattern> BerryPatterns = new List<SeasonalNighttimePattern>();
    public List<SeasonalNighttimePattern> FlowerPatterns = new List<SeasonalNighttimePattern>();
    public List<SeasonalNighttimePattern> ItemPatterns = new List<SeasonalNighttimePattern>();
    public List<SeasonalNighttimePattern> MoonPhasePatterns = new List<SeasonalNighttimePattern>();
    public List<SeasonalNighttimePattern> WeatherPatterns = new List<SeasonalNighttimePattern>();
    public List<SeasonalNighttimePattern> RoutePatterns = new List<SeasonalNighttimePattern>();
    public List<SeasonalNighttimePattern> HabitatPatterns = new List<SeasonalNighttimePattern>();
    public List<SeasonalNighttimePattern> BiomePatterns = new List<SeasonalNighttimePattern>();
    public List<SeasonalNighttimePattern> EcosystemPatterns = new List<SeasonalNighttimePattern>();
    public List<SeasonalNighttimePattern> RegionPatterns = new List<SeasonalNighttimePattern>();

    // Current simulation state
    public Season CurrentSeason = Season.Spring;
    public float CurrentHour = 22f; // Example time: 10 PM

    void Start()
    {
        // Initialize patterns with example data
        InitializePatterns();

        // Check active patterns for each category
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
        CreaturePatterns.Add(new SeasonalNighttimePattern(
            "Bat Foraging",
            Season.Spring,
            20f, 4f,
            "Bats forage for food during spring nights."
        ));

        TreePatterns.Add(new SeasonalNighttimePattern(
            "Cherry Blossom Moonlight Bloom",
            Season.Spring,
            22f, 2f,
            "Cherry blossoms bloom beautifully under moonlight during spring nights."
        ));

        BerryPatterns.Add(new SeasonalNighttimePattern(
            "Blueberry Nocturnal Growth",
            Season.Summer,
            23f, 5f,
            "Blueberries grow rapidly during summer nights."
        ));

        FlowerPatterns.Add(new SeasonalNighttimePattern(
            "Night-blooming Jasmine",
            Season.Autumn,
            19f, 3f,
            "Jasmine flowers release their fragrance at night during autumn."
        ));

        MoonPhasePatterns.Add(new SeasonalNighttimePattern(
            "Full Moonlight Effects",
            Season.Winter,
            21f, 1f,
            "Full moonlight enhances the environment during winter nights."
        ));

        WeatherPatterns.Add(new SeasonalNighttimePattern(
            "Winter Night Snowfall",
            Season.Winter,
            0f, 6f,
            "Snowfall is frequent during winter nights."
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

    private void CheckActivePatterns(List<SeasonalNighttimePattern> patterns, string category)
    {
        Debug.Log($"--- Checking {category} ---");
        foreach (var pattern in patterns)
        {
            if (pattern.IsActive(CurrentHour, CurrentSeason))
            {
                Debug.Log($"Active Pattern: {pattern.EntityName} - {pattern.Description}");
            }
        }
    }
}
