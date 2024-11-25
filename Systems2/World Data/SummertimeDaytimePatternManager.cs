using System.Collections.Generic;
using UnityEngine;

public class SummertimeDaytimePatternManager : MonoBehaviour
{
    // Base class for summertime daytime patterns
    [System.Serializable]
    public class SummerDaytimePattern
    {
        public string EntityName;      // Name of the entity (e.g., Tree, Creature)
        public float StartHour;        // Start of the daytime window
        public float EndHour;          // End of the daytime window
        public string Description;     // Description of the pattern

        public SummerDaytimePattern(string entityName, float startHour, float endHour, string description)
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
                // Handle patterns that span midnight (not common for daytime but included for robustness)
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

    // Lists for specific categories of summertime daytime patterns
    public List<SummerDaytimePattern> TreePatterns = new List<SummerDaytimePattern>();
    public List<SummerDaytimePattern> BerryPatterns = new List<SummerDaytimePattern>();
    public List<SummerDaytimePattern> FlowerPatterns = new List<SummerDaytimePattern>();
    public List<SummerDaytimePattern> ItemPatterns = new List<SummerDaytimePattern>();
    public List<SummerDaytimePattern> MoonPhasePatterns = new List<SummerDaytimePattern>();
    public List<SummerDaytimePattern> WeatherPatterns = new List<SummerDaytimePattern>();
    public List<SummerDaytimePattern> RoutePatterns = new List<SummerDaytimePattern>();
    public List<SummerDaytimePattern> HabitatPatterns = new List<SummerDaytimePattern>();
    public List<SummerDaytimePattern> BiomePatterns = new List<SummerDaytimePattern>();
    public List<SummerDaytimePattern> EcosystemPatterns = new List<SummerDaytimePattern>();
    public List<SummerDaytimePattern> RegionPatterns = new List<SummerDaytimePattern>();
    public List<SummerDaytimePattern> CreaturePatterns = new List<SummerDaytimePattern>();

    // Current time simulation state
    public float CurrentHour = 10f; // Example time: 10 AM

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
        TreePatterns.Add(new SummerDaytimePattern(
            "Oak Tree Photosynthesis Boost",
            8f, 16f,
            "Oak trees accelerate photosynthesis during summer days."
        ));

        BerryPatterns.Add(new SummerDaytimePattern(
            "Raspberry Ripening",
            9f, 17f,
            "Raspberries ripen steadily during warm summer days."
        ));

        FlowerPatterns.Add(new SummerDaytimePattern(
            "Sunflower Blooming",
            6f, 18f,
            "Sunflowers fully bloom under the summer sun."
        ));

        ItemPatterns.Add(new SummerDaytimePattern(
            "Summer Festival Items",
            10f, 14f,
            "Special items become available during summer festivals."
        ));

        MoonPhasePatterns.Add(new SummerDaytimePattern(
            "Summer Solstice Moon Influence",
            12f, 13f,
            "The moon subtly influences the environment during summer solstice."
        ));

        WeatherPatterns.Add(new SummerDaytimePattern(
            "Heatwave",
            12f, 15f,
            "Heatwaves are common during peak summer hours."
        ));

        RoutePatterns.Add(new SummerDaytimePattern(
            "Mountain Pass Accessibility",
            7f, 19f,
            "Mountain passes are safest to traverse during summer days."
        ));

        HabitatPatterns.Add(new SummerDaytimePattern(
            "Meadow Activity",
            9f, 16f,
            "Meadows are teeming with life during summer daylight."
        ));

        BiomePatterns.Add(new SummerDaytimePattern(
            "Savannah Heat Adaptation",
            11f, 16f,
            "Savannah flora and fauna exhibit adaptations to high summer temperatures."
        ));

        EcosystemPatterns.Add(new SummerDaytimePattern(
            "Coral Reef Vibrance",
            8f, 15f,
            "Coral reefs are most vibrant and active during summer daylight."
        ));

        RegionPatterns.Add(new SummerDaytimePattern(
            "Coastal Breeze",
            10f, 18f,
            "Coastal regions experience refreshing breezes during summer days."
        ));

        CreaturePatterns.Add(new SummerDaytimePattern(
            "Deer Grazing in Meadows",
            6f, 12f,
            "Deer are active in meadows during early summer mornings."
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

    private void CheckActivePatterns(List<SummerDaytimePattern> patterns, string category)
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
