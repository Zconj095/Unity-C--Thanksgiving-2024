using System.Collections.Generic;
using UnityEngine;

public class CreatureBehaviorManager : MonoBehaviour
{
    // Enum to represent day/night phases
    public enum DayPhase
    {
        Daytime,
        Nighttime
    }

    // Enum for seasons
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    // Class to define creature behavior patterns
    [System.Serializable]
    public class CreaturePattern
    {
        public string CreatureName;
        public DayPhase Phase;       // Daytime or Nighttime
        public Season SeasonalCycle; // Seasonal patterns
        public float StartHour;      // Timeframe when behavior starts
        public float EndHour;        // Timeframe when behavior ends
        public string Behavior;      // Description of the pattern

        public CreaturePattern(string name, DayPhase phase, Season season, float startHour, float endHour, string behavior)
        {
            CreatureName = name;
            Phase = phase;
            SeasonalCycle = season;
            StartHour = startHour;
            EndHour = endHour;
            Behavior = behavior;
        }

        public void DisplayDetails()
        {
            Debug.Log($"Creature: {CreatureName}");
            Debug.Log($"Phase: {Phase}, Season: {SeasonalCycle}");
            Debug.Log($"Active from {StartHour}:00 to {EndHour}:00");
            Debug.Log($"Behavior: {Behavior}");
        }

        public bool IsActive(float currentHour, Season currentSeason, DayPhase currentPhase)
        {
            if (currentSeason == SeasonalCycle && currentPhase == Phase)
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
            return false;
        }
    }

    // List of creature patterns
    public List<CreaturePattern> creaturePatterns = new List<CreaturePattern>();

    // Example: Current simulation state
    public Season currentSeason = Season.Spring;
    public float currentHour = 22f; // Example time: 10 PM
    public DayPhase currentPhase = DayPhase.Nighttime;

    void Start()
    {
        // Define example creature patterns
        creaturePatterns.Add(new CreaturePattern(
            "Night Owl",
            DayPhase.Nighttime,
            Season.Winter,
            20f,  // 8 PM
            4f,   // 4 AM
            "Forages for food under the cover of darkness."
        ));

        creaturePatterns.Add(new CreaturePattern(
            "Sunbird",
            DayPhase.Daytime,
            Season.Summer,
            6f,   // 6 AM
            18f,  // 6 PM
            "Active during the bright summer days, searching for nectar."
        ));

        creaturePatterns.Add(new CreaturePattern(
            "Twilight Fox",
            DayPhase.Nighttime,
            Season.Autumn,
            18f,  // 6 PM
            6f,   // 6 AM
            "Hunts small prey and hides during the day."
        ));

        // Display all patterns
        foreach (var pattern in creaturePatterns)
        {
            pattern.DisplayDetails();
        }
    }

    void Update()
    {
        // Check active patterns every frame (or adapt to game tick)
        Debug.Log($"Current Simulation Time: {currentHour}:00, Phase: {currentPhase}, Season: {currentSeason}");

        foreach (var pattern in creaturePatterns)
        {
            if (pattern.IsActive(currentHour, currentSeason, currentPhase))
            {
                Debug.Log($"Active Pattern: {pattern.CreatureName} - {pattern.Behavior}");
            }
        }

        // Simulate time progression
        SimulateTime();
    }

    void SimulateTime()
    {
        currentHour += Time.deltaTime / 60f; // Simulate minutes passing
        if (currentHour >= 24f)
        {
            currentHour -= 24f;
            UpdatePhase();
        }
    }

    void UpdatePhase()
    {
        if (currentHour >= 6f && currentHour < 18f)
        {
            currentPhase = DayPhase.Daytime;
        }
        else
        {
            currentPhase = DayPhase.Nighttime;
        }
    }
}
