using System.Collections.Generic;
using UnityEngine;

public class WinterNighttimePatternManager : MonoBehaviour
{
    // Base class for winter nighttime patterns
    [System.Serializable]
    public class WinterNighttimePattern
    {
        public string EntityName;      // Name of the entity (e.g., Creature, Tree)
        public float StartHour;        // Start of the nighttime window
        public float EndHour;          // End of the nighttime window
        public string Description;     // Description of the pattern

        public WinterNighttimePattern(string entityName, float startHour, float endHour, string description)
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

    // Lists for specific categories of winter nighttime patterns
    public List<WinterNighttimePattern> CreaturePatterns = new List<WinterNighttimePattern>();
    public List<WinterNighttimePattern> TreePatterns = new List<WinterNighttimePattern>();
    public List<WinterNighttimePattern> BerryPatterns = new List<WinterNighttimePattern>();
    public List<WinterNighttimePattern> FlowerPatterns = new List<WinterNighttimePattern>();
    public List<WinterNighttimePattern> ItemPatterns = new List<WinterNighttimePattern>();
    public List<WinterNighttimePattern> MoonPhasePatterns = new List<WinterNighttimePattern>();
    public List<WinterNighttimePattern> WeatherPatterns = new List<WinterNighttimePattern>();
    public List<WinterNighttimePattern> RoutePatterns = new List<WinterNighttimePattern>();
    public List<WinterNighttimePattern> HabitatPatterns = new List<WinterNighttimePattern>();
    public List<WinterNighttimePattern> BiomePatterns = new List<WinterNighttimePattern>();
    public List<WinterNighttimePattern> EcosystemPatterns = new List<WinterNighttimePattern>();
    public List<WinterNighttimePattern> RegionPatterns = new List<WinterNighttimePattern>();

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
        CreaturePatterns.Add(new WinterNighttimePattern(
            "Arctic Fox Hunting",
            20f, 4f,
            "Arctic foxes are most active during winter nights, hunting for prey."
        ));

        TreePatterns.Add(new WinterNighttimePattern(
            "Snow-Laden Pines",
            21f, 3f,
            "Pine trees glisten with snow under the winter moonlight."
        ));

        BerryPatterns.Add(new WinterNighttimePattern(
            "Frozen Berries Formation",
            23f, 5f,
            "Winter berries freeze overnight, forming frosty coatings."
        ));

        FlowerPatterns.Add(new WinterNighttimePattern(
            "Winter Orchid Bloom",
            22f, 4f,
            "Winter orchids bloom in the cold, serene winter nights."
        ));

        ItemPatterns.Add(new WinterNighttimePattern(
            "Glacial Relics Discovery",
            19f, 1f,
            "Rare glacial relics can be found during winter nights in icy regions."
        ));

        MoonPhasePatterns.Add(new WinterNighttimePattern(
            "Winter Full Moon Effects",
            20f, 23f,
            "The winter full moon creates an ethereal glow across the landscape."
        ));

        WeatherPatterns.Add(new WinterNighttimePattern(
            "Snowfall",
            0f, 6f,
            "Snowfall occurs frequently during the coldest hours of winter nights."
        ));

        RoutePatterns.Add(new WinterNighttimePattern(
            "Icy Forest Paths",
            21f, 4f,
            "Forest paths become icy and treacherous during winter nights."
        ));

        HabitatPatterns.Add(new WinterNighttimePattern(
            "Hibernation Shelters",
            20f, 6f,
            "Many creatures retreat to their hibernation shelters during winter nights."
        ));

        BiomePatterns.Add(new WinterNighttimePattern(
            "Frozen Wetlands",
            19f, 3f,
            "Wetlands freeze over, showing a unique ecosystem during winter nights."
        ));

        EcosystemPatterns.Add(new WinterNighttimePattern(
            "Nocturnal Predation",
            20f, 4f,
            "Predators dominate the frozen ecosystems during winter nights."
        ));

        RegionPatterns.Add(new WinterNighttimePattern(
            "Northern Lights Visibility",
            21f, 2f,
            "The northern lights can be seen vividly in certain regions during winter nights."
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

    private void CheckActivePatterns(List<WinterNighttimePattern> patterns, string category)
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
