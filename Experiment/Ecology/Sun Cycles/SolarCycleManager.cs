using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enviro;
using Gaia;

[RequireComponent(typeof(Camera))]
public class SolarCycleManager : MonoBehaviour
{
    [Header("Solar Cycle Settings")]
    public float solarCycleDuration = 11f; // Average solar cycle is 11 years
    public float currentCycleProgress = 0f; // 0 to 1, where 0.5 is solar maximum
    public bool simulateFullCycle = false; // If true, will simulate entire 11-year cycle
    public float simulationSpeed = 1f; // How fast to simulate the cycle

    [Header("Solar Tide Settings")]
    public float baseSolarTideHeight = 0.5f;
    public float solarTideVariation = 0.3f;
    public float solarMaximumTideBoost = 0.2f;

    [Header("Environmental Effects")]
    public float baseAtmosphericDensity = 1f;
    public float solarWindStrength = 1f;
    public float auroraIntensity = 1f;
    public Color auroraBaseColor = new Color(0.1f, 0.8f, 0.3f);

    private float currentSolarTideHeight;
    private float lastDayTime = -1f;
    private EnviroManager enviroManager;
    private LunarCycleManager lunarManager;
    private Camera mainCamera;

    private void OnEnable()
    {
        InitializeComponents();
        StartCoroutine(UpdateSolarCycle());
    }

    private void InitializeComponents()
    {
        enviroManager = EnviroManager.instance;
        if (enviroManager == null)
        {
            Debug.LogError("EnviroManager instance not found!");
            enabled = false;
            return;
        }

        lunarManager = GetComponent<LunarCycleManager>();
        if (lunarManager == null)
        {
            Debug.LogWarning("LunarCycleManager not found. Solar-Lunar tide combination effects will be limited.");
        }

        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogError("Camera component not found!");
            enabled = false;
            return;
        }
    }

    private IEnumerator UpdateSolarCycle()
    {
        while (enabled)
        {
            if (simulateFullCycle)
            {
                // Update cycle progress
                currentCycleProgress += (Time.deltaTime * simulationSpeed) / (solarCycleDuration * 365f * 24f * 3600f);
                if (currentCycleProgress >= 1f)
                    currentCycleProgress = 0f;
            }

            UpdateSolarEffects();
            yield return new WaitForSeconds(1f); // Update every second
        }
    }

    private void Update()
    {
        // Access the solar time from the EnviroManager
        float dayTime = Enviro.EnviroManager.instance.solarTime / 24f; // Assuming solarTime is in hours

        // Update solar tide if time has changed
        if (Mathf.Abs(dayTime - lastDayTime) > Mathf.Epsilon)
        {
            lastDayTime = dayTime;
            UpdateSolarTide(dayTime);
        }
    }


    private void UpdateSolarTide(float dayTime)
    {
        // Calculate base solar tide based on time of day
        float dailyTideFactor = Mathf.Sin(dayTime * 2f * Mathf.PI) * 0.5f + 0.5f;
        
        // Apply solar cycle influence
        float cycleInfluence = GetSolarCycleInfluence();
        
        // Calculate final tide height
        currentSolarTideHeight = baseSolarTideHeight + 
            (dailyTideFactor * solarTideVariation) +
            (cycleInfluence * solarMaximumTideBoost);

        // Combine with lunar tide if available
        UpdateCombinedTideEffects();
    }

    private float GetSolarCycleInfluence()
    {
        // Returns maximum influence at solar maximum (0.5) and minimum at cycle start/end
        return 1f - Mathf.Abs((currentCycleProgress - 0.5f) * 2f);
    }

    private void UpdateSolarEffects()
    {
        float cycleInfluence = GetSolarCycleInfluence();
        
        // Update atmospheric effects
        UpdateAtmosphericEffects(cycleInfluence);
        
        // Update aurora effects
        UpdateAuroraEffects(cycleInfluence);
        
        // Update solar wind effects
        UpdateSolarWindEffects(cycleInfluence);
    }

    private void UpdateAtmosphericEffects(float cycleInfluence)
    {
        // Modify atmospheric density based on solar activity
        float currentDensity = baseAtmosphericDensity * (1f + cycleInfluence * 0.1f);
        RenderSettings.fogDensity = currentDensity;
        
        // You might want to integrate with Enviro's weather system here
        if (enviroManager != null)
        {
            // Example integration (adjust based on your Enviro version)
            // enviroManager.SetAtmosphericDensity(currentDensity);
        }
    }

    private void UpdateAuroraEffects(float cycleInfluence)
    {
        // Increase aurora intensity during solar maximum
        float currentIntensity = auroraIntensity * cycleInfluence;
        
        // Calculate aurora color variation based on solar activity
        Color currentAuroraColor = Color.Lerp(
            auroraBaseColor,
            new Color(0.8f, 0.2f, 0.2f), // Red shift during high activity
            cycleInfluence
        );

        // Apply aurora effects (integrate with your aurora system)
        // Example: auroraSystem.SetIntensity(currentIntensity);
        // Example: auroraSystem.SetColor(currentAuroraColor);
    }

    private void UpdateSolarWindEffects(float cycleInfluence)
    {
        // Calculate current solar wind strength
        float currentSolarWind = solarWindStrength * (1f + cycleInfluence);
        
        // Apply effects to particle systems or other environmental effects
        ParticleSystem[] solarWindEffects = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in solarWindEffects)
        {
            var main = ps.main;
            main.startSpeed = currentSolarWind;
        }
    }

    private void UpdateCombinedTideEffects()
    {
        if (lunarManager == null) return;

        // Get water object
        GameObject waterObject = GameObject.Find("Water");
        if (waterObject == null) return;

        // Combine lunar and solar tide effects
        // This assumes the LunarCycleManager has exposed its current water level
        float combinedTideHeight = currentSolarTideHeight; // Add lunar tide height here
        
        // Apply combined height
        Vector3 waterPosition = waterObject.transform.position;
        waterPosition.y = combinedTideHeight;
        waterObject.transform.position = waterPosition;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        // Draw debug visualization for solar activity
        Gizmos.color = Color.yellow;
        Vector3 position = transform.position;
        float size = GetSolarCycleInfluence() * 2f;
        Gizmos.DrawWireSphere(position, size);
    }

    public float GetCurrentSolarCycleProgress()
    {
        return currentCycleProgress;
    }

    public bool IsSolarMaximum()
    {
        return Mathf.Abs(currentCycleProgress - 0.5f) < 0.1f;
    }

    public bool IsSolarMinimum()
    {
        return currentCycleProgress < 0.1f || currentCycleProgress > 0.9f;
    }

    private void OnDisable()
    {
        // Reset any environmental effects
        if (RenderSettings.fogDensity != baseAtmosphericDensity)
        {
            RenderSettings.fogDensity = baseAtmosphericDensity;
        }
    }
}