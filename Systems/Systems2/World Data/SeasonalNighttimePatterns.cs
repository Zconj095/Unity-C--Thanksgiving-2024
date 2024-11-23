using System.Collections.Generic;
using UnityEngine;

public class SeasonalNighttimePatterns : MonoBehaviour
{
    // Enum for seasons
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    // Base class for Nighttime Pattern
    [System.Serializable]
    public class NighttimePattern
    {
        public string EntityName;  // Name of the entity (Tree, Berry, etc.)
        public Season ActiveSeason; // Season this pattern is active
        public float StartHour;    // Start time (nighttime)
        public float EndHour;      // End time (nighttime)
        public string Description; // Description of the pattern

        public NighttimePattern(string name, Season season, float startHour, float endHour, string description)
        {
            EntityName = name;
            ActiveSeason = season;
            StartHour = startHour;
            EndHour = endHour;
            Description = description;
        }

        public void DisplayDetails()
        {
            Debug.Log($"Entity: {EntityName}");
            Debug.Log($"Season: {ActiveSeason}, Active from {StartHour}:00 to {EndHour}:00");
            Debug.Log($"Description: {Description}");
        }

        public bool IsActive(Season currentSeason, float currentHour)
        {
            if (currentSeason == ActiveSeason)
            {
                if (StartHour <= EndHour)
                {
                    return currentHour >= StartHour && currentHour <= EndHour;
                }
                else
                {
                    // Handle patterns spanning midnight
                    return currentHour >= StartHour || currentHour <= EndHour;
                }
            }
            return false;
        }
    }

    // Lists for different categories of patterns
    public List<NighttimePattern> TreePatterns = new List<NighttimePattern>();
    public List<NighttimePattern> BerryPatterns = new List<NighttimePattern>();
    public List<NighttimePattern> FlowerPatterns = new List<NighttimePattern>();
    public List<NighttimePattern> ItemPatterns = new List<NighttimePattern>();
    public List<NighttimePattern> MoonPhasePatterns = new List<NighttimePattern>();
    public List<NighttimePattern> WeatherPatterns = new List<NighttimePattern>();
    public List<NighttimePattern> RoutePatterns = new List<NighttimePattern>();
    public List<NighttimePattern> HabitatPatterns = new List<NighttimePattern>();
    public List<NighttimePattern> BiomePatterns = new List<NighttimePattern>();
    public List<NighttimePattern> EcosystemPatterns = new List<NighttimePattern>();
    public List<NighttimePattern> RegionPatterns = new List<NighttimePattern>();
}
