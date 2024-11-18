using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enviro;

public class EclipseOnFullMoon : MonoBehaviour
{
    private EnviroManager enviroManager;

    private void Start()
    {
        // Initialize the EnviroManager instance
        enviroManager = EnviroManager.instance; // [cite: 1267, 1268, 1269]

        if (enviroManager == null)
        {
            Debug.LogError("EnviroManager instance not found!"); // [cite: 1680, 1681, 1682]
            return;
        }
    }

    private void Update()
    {
        // Get the current moon phase from EnviroSky
        var moonPhase = enviroManager.Sky.Settings.moonPhase; // [cite: 2294, 2295, 2296, 2297, 2298, 2299, 2300, 2301, 2302]

        // Check if the moon phase is Full Moon
        if (moonPhase == 0f) // [cite: 2294, 2295, 2296, 2297, 2298, 2299, 2300, 2301, 2302]
        {
            // Check if an eclipse is already active
            // This functionality is not available in the Enviro Plugin
            //if (!enviroManager.IsEclipseActive()) 
            //{
                TriggerEclipse(); // [cite: 1680, 1681, 1682]
            //}
        }
        else
        {
            // Reset any changes when not a full moon
            ResetEclipse(); // [cite: 1680, 1681, 1682]
        }
    }

    private void TriggerEclipse()
    {
        // Implementation of what should happen during an eclipse
        Debug.Log("Eclipse triggered on full moon!"); // [cite: 1680, 1681, 1682]
        // Modify lighting settings or effects based on the eclipse
        var lightingSettings = enviroManager.Lighting.Settings; // [cite: 1079, 1080, 1081]

        // Modify lighting settings here based on your requirements
        lightingSettings.directLightIntensityModifier = 0.5f; // [cite: 1079, 1080, 1081]
        // Example change, adjust as needed
    }

    private void ResetEclipse()
    {
        // Reset lighting settings or any other variables when the full moon is no longer active
        var lightingSettings = enviroManager.Lighting.Settings; // [cite: 1079, 1080, 1081]

        // Reset to default or desired lighting settings
        lightingSettings.directLightIntensityModifier = 1.0f; // [cite: 1079, 1080, 1081]
        // Example of resetting to normal intensity
    }
}