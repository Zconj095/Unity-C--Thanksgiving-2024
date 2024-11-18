using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enviro;
using Gaia;

[RequireComponent(typeof(Camera))]
public class LunarCycleManager : MonoBehaviour
{
    [Header("Lunar Cycle Settings")]
    public float baseGravity = -9.81f;
    public float gravityChangeFactor = 0.5f;
    public float baseWaterLevel = 0f;
    public float fullMoonWaterLevelChange = 1f;

    [Header("Culling Settings")]
    public GaiaSceneCullingProfile cullingProfile;
    public bool applyToEditorCamera = false;
    public bool realtimeUpdate = false;
    public float additionalCullingDistance = 0f;

    private float currentGravity;
    private float currentWaterLevel;
    private float lastLunarTime = -1f;
    private EnviroManager enviroManager;
    private Camera mainCamera;
    private GaiaSettings gaiaSettings;

    private void OnEnable()
    {
        InitializeComponents();
        SetupCullingProfile();
    }

    private void InitializeComponents()
    {
        // Get required components
        enviroManager = EnviroManager.instance;
        if (enviroManager == null)
        {
            Debug.LogError("EnviroManager instance not found!");
            enabled = false;
            return;
        }

        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogError("Camera component not found!");
            enabled = false;
            return;
        }

        // Try to find Gaia Settings
        gaiaSettings = Resources.Load<GaiaSettings>("GaiaSettings");
        if (gaiaSettings == null)
        {
            Debug.LogWarning("Gaia Settings not found. Using default culling values.");
        }
    }

    private void SetupCullingProfile()
    {
        if (cullingProfile == null)
        {
            Debug.LogWarning("No culling profile assigned. Creating new profile.");
            cullingProfile = ScriptableObject.CreateInstance<GaiaSceneCullingProfile>();
            cullingProfile.InitCulling(gaiaSettings);
        }

        // Apply initial culling settings
        UpdateCullingSettings();
    }

    private void Start()
    {
        UpdateGravityAndWater();
    }

    private void Update()
    {
        float lunarTime = enviroManager.lunarTime;

        // Update lunar cycle effects if time has changed
        if (Mathf.Abs(lunarTime - lastLunarTime) > Mathf.Epsilon)
        {
            lastLunarTime = lunarTime;
            UpdateGravityAndWater();
        }

        // Update culling if realtime updates are enabled
        if (realtimeUpdate)
        {
            UpdateCullingSettings();
        }
    }

    private void UpdateCullingSettings()
    {
        if (cullingProfile == null || mainCamera == null) return;

        // Update culling distances based on lunar phase
        float phaseFactor = GetLunarPhaseFactor();
        
        // Modify culling distances based on lunar phase
        for (int i = 0; i < cullingProfile.m_layerDistances.Length; i++)
        {
            string layerName = LayerMask.LayerToName(i);
            float baseDistance = cullingProfile.m_layerDistances[i];

            // Adjust culling distances during full moon
            switch (layerName)
            {
                case "PW_Object_Small":
                case "PW_Object_Medium":
                case "PW_Object_Large":
                    // Increase view distance during full moon
                    cullingProfile.m_layerDistances[i] = baseDistance * (1f + phaseFactor * 0.25f);
                    break;
            }
        }

        // Apply culling settings to camera
        ApplyCullingSettings();
    }

    private void ApplyCullingSettings()
    {
        if (mainCamera == null) return;

        // Apply layer culling distances
        for (int i = 0; i < cullingProfile.m_layerDistances.Length; i++)
        {
            float distance = cullingProfile.m_layerDistances[i] + additionalCullingDistance;
            mainCamera.layerCullDistances[i] = distance;
        }
    }

    private float GetLunarPhaseFactor()
    {
        // Returns a factor between 0 (new moon) and 1 (full moon)
        float lunarTime = enviroManager.lunarTime;
        if (lunarTime < 0.25f || lunarTime >= 0.75f)
            return 0f;
        else if (lunarTime >= 0.45f && lunarTime < 0.55f)
            return 1f;
        else if (lunarTime < 0.5f)
            return (lunarTime - 0.25f) * 4f;
        else
            return (0.75f - lunarTime) * 4f;
    }

    private void UpdateGravityAndWater()
    {
        LunarPhase currentPhase = GetCurrentPhase(enviroManager.lunarTime);
        
        // Update gravity and water based on phase
        switch (currentPhase)
        {
            case LunarPhase.NewMoon:
                currentGravity = baseGravity;
                currentWaterLevel = baseWaterLevel;
                break;
            case LunarPhase.FirstQuarter:
                currentGravity = baseGravity * (1 + gravityChangeFactor * 0.25f);
                currentWaterLevel = baseWaterLevel + fullMoonWaterLevelChange * 0.25f;
                break;
            case LunarPhase.FullMoon:
                currentGravity = baseGravity * (1 + gravityChangeFactor);
                currentWaterLevel = baseWaterLevel + fullMoonWaterLevelChange;
                break;
            case LunarPhase.LastQuarter:
                currentGravity = baseGravity * (1 + gravityChangeFactor * 0.25f);
                currentWaterLevel = baseWaterLevel + fullMoonWaterLevelChange * 0.25f;
                break;
        }

        // Apply changes
        Physics.gravity = new Vector3(0, currentGravity, 0);
        UpdateWaterLevel();
    }

    private void UpdateWaterLevel()
    {
        GameObject waterObject = GameObject.Find("Water");
        if (waterObject != null)
        {
            Vector3 waterPosition = waterObject.transform.position;
            waterPosition.y = currentWaterLevel;
            waterObject.transform.position = waterPosition;
        }
    }

    private LunarPhase GetCurrentPhase(float lunarTime)
    {
        if (lunarTime < 0.25f)
            return LunarPhase.NewMoon;
        else if (lunarTime < 0.5f)
            return LunarPhase.FirstQuarter;
        else if (lunarTime < 0.75f)
            return LunarPhase.FullMoon;
        else
            return LunarPhase.LastQuarter;
    }

    private void OnDisable()
    {
        // Reset camera culling settings when disabled
        if (mainCamera != null)
        {
            for (int i = 0; i < 32; i++)
            {
                mainCamera.layerCullDistances[i] = 0;
            }
        }
    }

    public enum LunarPhase
    {
        NewMoon,
        FirstQuarter,
        FullMoon,
        LastQuarter
    }
}