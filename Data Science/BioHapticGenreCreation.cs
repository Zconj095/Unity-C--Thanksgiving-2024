using UnityEngine;

public class BioHapticGenreCreation : MonoBehaviour
{
    // Simulated bio-signal data (heart rate for example)
    private float heartRate = 70f; // Example initial value in beats per minute
    private bool isGameIntense = false;

    // Haptic feedback strength (for vibration or wearable feedback)
    private float hapticStrength = 0f;

    // The target range for heart rate that triggers a change in game dynamics
    private const float LowHeartRateThreshold = 60f;
    private const float HighHeartRateThreshold = 100f;

    // Haptic vibration controller reference
    private HapticController hapticController;

    void Awake()
    {
        // Get the HapticController component
        hapticController = GetComponent<HapticController>();

        // Validate the hapticController component
        if (hapticController == null)
        {
            Debug.LogError("HapticController component is missing. Attach it to the GameObject.");
        }
    }

    void Update()
    {
        // Simulate bio-signal input or use actual sensor data
        heartRate = GetHeartRateData();

        // Adjust game dynamics based on heart rate
        AdjustGameDynamicsBasedOnHeartRate();

        // Provide haptic feedback
        ProvideHapticFeedback();
    }

    // Simulate acquisition of heart rate data (replace with real sensor logic)
    private float GetHeartRateData()
    {
        return Mathf.PingPong(Time.time * 10f, HighHeartRateThreshold * 2); // Simulated fluctuation
    }

    // Adjust the game's dynamics based on the player's bio-feedback
    private void AdjustGameDynamicsBasedOnHeartRate()
    {
        if (heartRate < LowHeartRateThreshold)
        {
            isGameIntense = false;
            // Adjust gameplay to be more relaxed
        }
        else if (heartRate > HighHeartRateThreshold)
        {
            isGameIntense = true;
            // Increase gameplay intensity
        }
        else
        {
            isGameIntense = false;
            // Maintain normal gameplay dynamics
        }
    }

    // Provide haptic feedback based on the current game state
    private void ProvideHapticFeedback()
    {
        if (hapticController == null)
        {
            Debug.LogError("HapticController is not available. Haptic feedback skipped.");
            return;
        }

        hapticStrength = isGameIntense
            ? Mathf.InverseLerp(HighHeartRateThreshold, 120f, heartRate)
            : Mathf.InverseLerp(LowHeartRateThreshold, HighHeartRateThreshold, heartRate);

        hapticController.Vibrate(hapticStrength);
    }
}

// Haptic feedback controller class
public class HapticController : MonoBehaviour
{
    public void Vibrate(float strength)
    {
        if (Application.isMobilePlatform)
        {
            Handheld.Vibrate();
        }

        // Implement device-specific vibration logic here
        // Example: Send vibration signal to hardware with 'strength' value
    }
}
