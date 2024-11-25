using System.Collections.Generic;
using UnityEngine;

public class DaytimeSpawningManager : MonoBehaviour
{
    // Enum for seasons
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    // Base class for spawning patterns
    [System.Serializable]
    public class DaytimeSpawnPattern
    {
        public string EntityName;      // Name of the entity (e.g., Tree, Berry)
        public Season ActiveSeason;    // Season in which the pattern is active
        public float StartHour;        // Timeframe start during the day (in hours)
        public float EndHour;          // Timeframe end during the day (in hours)
        public string Description;     // Description of the spawning pattern

        public DaytimeSpawnPattern(string name, Season season, float startHour, float endHour, string description)
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
                // Handle patterns that span midnight (not typical for daytime patterns but included for robustness)
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
    public List<DaytimeSpawnPattern> TreePatterns = new List<DaytimeSpawnPattern>();
    public List<DaytimeSpawnPattern> BerryPatterns = new List<DaytimeSpawnPattern>();
    public List<DaytimeSpawnPattern> FlowerPatterns = new List<DaytimeSpawnPattern>();
    public List<DaytimeSpawnPattern> ItemPatterns = new List<DaytimeSpawnPattern>();
    public List<DaytimeSpawnPattern> MoonPhasePatterns = new List<DaytimeSpawnPattern>();
    public List<DaytimeSpawnPattern> WeatherPatterns = new List<DaytimeSpawnPattern>();
    public List<DaytimeSpawnPattern> RoutePatterns = new List<DaytimeSpawnPattern>();
    public List<DaytimeSpawnPattern> HabitatPatterns = new List<DaytimeSpawnPattern>();
    public List<DaytimeSpawnPattern> BiomePatterns = new List<DaytimeSpawnPattern>();
    public List<DaytimeSpawnPattern> EcosystemPatterns = new List<DaytimeSpawnPattern>();
    public List<DaytimeSpawnPattern> RegionPatterns = new List<DaytimeSpawnPattern>();
    public List<DaytimeSpawnPattern> CreaturePatterns = new List<DaytimeSpawnPattern>();

    // Current simulation state
    public Season CurrentSeason = Season.Spring;
    public float CurrentHour = 10f; // Example time: 10 AM

    void Start()
    {
        // Initialize example patterns
        InitializePatterns();

        // Check for active spawn patterns
        CheckActivePatterns(TreePatterns, "Tree Patterns");
        CheckActivePatterns(BerryPatterns, "Berry Patterns");
    }

    void Update()
    {
        // Simulate time progression
        SimulateTime();

        // Check active patterns
        CheckActivePatterns(FlowerPatterns, "Flower Patterns");
        CheckActivePatterns(CreaturePatterns, "Creature Patterns");
    }

    private void InitializePatterns()
    {
        TreePatterns.Add(new DaytimeSpawnPattern(
            "Pine Tree Growth",
            Season.Spring,
            6f, // 6 AM
            18f, // 6 PM
            "Pine trees grow rapidly during spring daytime hours."
        ));

        BerryPatterns.Add(new DaytimeSpawnPattern(
            "Blueberry Ripening",
            Season.Summer,
            8f, // 8 AM
            16f, // 4 PM
            "Blueberries ripen during summer days."
        ));

        FlowerPatterns.Add(new DaytimeSpawnPattern(
            "Sunflower Bloom",
            Season.Summer,
            6f, // 6 AM
            12f, // 12 PM
            "Sunflowers bloom with the rising sun."
        ));

        CreaturePatterns.Add(new DaytimeSpawnPattern(
            "Deer Grazing",
            Season.Autumn,
            9f, // 9 AM
            17f, // 5 PM
            "Deer are active during autumn days, grazing in open fields."
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

    private void CheckActivePatterns(List<DaytimeSpawnPattern> patterns, string category)
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
