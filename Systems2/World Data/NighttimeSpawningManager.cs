using System.Collections.Generic;
using UnityEngine;

public class NighttimeSpawningManager : MonoBehaviour
{
    // Enum for Seasons
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    // Base class for spawning patterns
    [System.Serializable]
    public class NighttimeSpawnPattern
    {
        public string EntityName;      // Name of the entity (e.g., Tree, Berry)
        public Season ActiveSeason;    // Season in which the pattern is active
        public float StartHour;        // Timeframe start during the night (in hours)
        public float EndHour;          // Timeframe end during the night (in hours)
        public string Description;     // Description of the spawning pattern

        public NighttimeSpawnPattern(string name, Season season, float startHour, float endHour, string description)
        {
            EntityName = name;
            ActiveSeason = season;
            StartHour = startHour;
            EndHour = endHour;
            Description = description;
        }

        public bool IsActive(float currentHour, Season currentSeason)
        {
            if (currentSeason != ActiveSeason)
                return false;

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

    // Lists of spawning patterns for various categories
    public List<NighttimeSpawnPattern> TreePatterns = new List<NighttimeSpawnPattern>();
    public List<NighttimeSpawnPattern> BerryPatterns = new List<NighttimeSpawnPattern>();
    public List<NighttimeSpawnPattern> FlowerPatterns = new List<NighttimeSpawnPattern>();
    public List<NighttimeSpawnPattern> ItemPatterns = new List<NighttimeSpawnPattern>();
    public List<NighttimeSpawnPattern> MoonPhasePatterns = new List<NighttimeSpawnPattern>();
    public List<NighttimeSpawnPattern> WeatherPatterns = new List<NighttimeSpawnPattern>();
    public List<NighttimeSpawnPattern> RoutePatterns = new List<NighttimeSpawnPattern>();
    public List<NighttimeSpawnPattern> HabitatPatterns = new List<NighttimeSpawnPattern>();
    public List<NighttimeSpawnPattern> BiomePatterns = new List<NighttimeSpawnPattern>();
    public List<NighttimeSpawnPattern> EcosystemPatterns = new List<NighttimeSpawnPattern>();
    public List<NighttimeSpawnPattern> RegionPatterns = new List<NighttimeSpawnPattern>();

    // Current simulation state
    public Season CurrentSeason = Season.Spring;
    public float CurrentHour = 22f; // Example time: 10 PM

    void Start()
    {
        // Initialize example patterns
        InitializePatterns();

        // Check for active spawn patterns
        CheckActivePatterns(TreePatterns, "Tree Patterns");
        CheckActivePatterns(BerryPatterns, "Berry Patterns");
        CheckActivePatterns(MoonPhasePatterns, "Moon Phase Patterns");
    }

    void Update()
    {
        // Simulate time progression
        SimulateTime();

        // Check active patterns
        CheckActivePatterns(TreePatterns, "Tree Patterns");
        CheckActivePatterns(FlowerPatterns, "Flower Patterns");
    }

    private void InitializePatterns()
    {
        TreePatterns.Add(new NighttimeSpawnPattern(
            "Oak Tree",
            Season.Spring,
            20f, // 8 PM
            4f,  // 4 AM
            "Oak trees release pollen at night in spring."
        ));

        BerryPatterns.Add(new NighttimeSpawnPattern(
            "Strawberry Bush",
            Season.Summer,
            22f, // 10 PM
            5f,  // 5 AM
            "Strawberry bushes thrive during summer nights."
        ));

        MoonPhasePatterns.Add(new NighttimeSpawnPattern(
            "Full Moon Glow",
            Season.Autumn,
            21f, // 9 PM
            3f,  // 3 AM
            "Full moon's light promotes nocturnal growth."
        ));

        WeatherPatterns.Add(new NighttimeSpawnPattern(
            "Rainfall",
            Season.Winter,
            0f,  // Midnight
            6f,  // 6 AM
            "Rainfall intensifies during winter nights."
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

    private void CheckActivePatterns(List<NighttimeSpawnPattern> patterns, string category)
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
