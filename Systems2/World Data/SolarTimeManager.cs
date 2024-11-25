using System.Collections.Generic;
using UnityEngine;

public class SolarTimeManager : MonoBehaviour
{
    // Enum for Solar Timeframes
    public enum Timeframe
    {
        Twilight,
        Sunrise,
        Morning,
        Noon,
        Afternoon,
        Evening,
        Midnight
    }

    // Struct for Solar Timeframe Details
    [System.Serializable]
    public class SolarTimeframe
    {
        public Timeframe Name;      // Name of the timeframe
        public float StartHour;     // Start time in hours (e.g., 0.0 for midnight)
        public float EndHour;       // End time in hours (e.g., 6.0 for 6 AM)
        public string Description;  // Description of the timeframe

        public SolarTimeframe(Timeframe name, float startHour, float endHour, string description)
        {
            Name = name;
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
                // Handle timeframes that span midnight
                return currentHour >= StartHour || currentHour <= EndHour;
            }
        }
    }

    // Data for Twilight Colors
    [System.Serializable]
    public class TwilightColor
    {
        public string Name;
        public Color ColorValue;
        public string Description;

        public TwilightColor(string name, Color colorValue, string description)
        {
            Name = name;
            ColorValue = colorValue;
            Description = description;
        }
    }

    // List of Solar Timeframes
    public List<SolarTimeframe> solarTimeframes = new List<SolarTimeframe>();

    // List of Twilight Colors
    public List<TwilightColor> twilightColors = new List<TwilightColor>();

    // Current simulation state
    public float currentHour = 0f; // Current time in hours
    private Timeframe currentActiveTimeframe;

    void Start()
    {
        // Initialize Timeframes
        InitializeTimeframes();

        // Initialize Twilight Colors
        InitializeTwilightColors();

        // Display current active timeframe
        UpdateTimeframe();
    }

    void Update()
    {
        // Simulate time progression
        SimulateTime();

        // Update and display the current timeframe
        UpdateTimeframe();
    }

    private void InitializeTimeframes()
    {
        solarTimeframes.Add(new SolarTimeframe(Timeframe.Twilight, 5f, 6f, "Hours between daybreak and sunrise."));
        solarTimeframes.Add(new SolarTimeframe(Timeframe.Sunrise, 6f, 8f, "Early morning hours of sunrise."));
        solarTimeframes.Add(new SolarTimeframe(Timeframe.Morning, 8f, 12f, "The time after sunrise and before noon."));
        solarTimeframes.Add(new SolarTimeframe(Timeframe.Noon, 12f, 13f, "The midpoint of the day."));
        solarTimeframes.Add(new SolarTimeframe(Timeframe.Afternoon, 13f, 17f, "The time after morning and before evening."));
        solarTimeframes.Add(new SolarTimeframe(Timeframe.Evening, 17f, 20f, "The hours leading into nighttime."));
        solarTimeframes.Add(new SolarTimeframe(Timeframe.Midnight, 0f, 5f, "The end of the day and beginning of night."));
    }

    private void InitializeTwilightColors()
    {
        twilightColors.Add(new TwilightColor("Dawn", new Color(1f, 0.7f, 0.5f), "The warm colors of dawn during twilight."));
        twilightColors.Add(new TwilightColor("Dusk", new Color(0.8f, 0.5f, 0.7f), "The cool colors of dusk during twilight."));
    }

    private void SimulateTime()
    {
        // Increment time (simulate time passing in hours)
        currentHour += Time.deltaTime / 60f; // 1 real-time second = 1 in-game minute
        if (currentHour >= 24f) currentHour -= 24f; // Wrap around to 0 after 24 hours
    }

    private void UpdateTimeframe()
    {
        foreach (var timeframe in solarTimeframes)
        {
            if (timeframe.IsActive(currentHour))
            {
                if (currentActiveTimeframe != timeframe.Name)
                {
                    currentActiveTimeframe = timeframe.Name;
                    Debug.Log($"Current Timeframe: {timeframe.Name} - {timeframe.Description}");
                }
            }
        }

        // Update twilight colors if in Twilight timeframe
        if (currentActiveTimeframe == Timeframe.Twilight)
        {
            UpdateTwilightColor();
        }
    }

    private void UpdateTwilightColor()
    {
        foreach (var color in twilightColors)
        {
            Debug.Log($"Twilight Color: {color.Name} - {color.Description}");
            // Apply twilight color to environment (e.g., Skybox or Lighting)
            RenderSettings.ambientLight = color.ColorValue;
        }
    }
}
