using UnityEngine;

public class TripleFrequencyRelayDelay : MonoBehaviour
{
    // Frequency thresholds for detection
    private const float FrequencyA_Min = 50f;
    private const float FrequencyA_Max = 60f;

    private const float FrequencyB_Min = 100f;
    private const float FrequencyB_Max = 120f;

    private const float FrequencyC_Min = 150f;
    private const float FrequencyC_Max = 170f;

    // Time delay between frequency checks (in seconds)
    private const float DelayTime = 2.0f;

    // States for detecting each frequency
    private bool isFrequencyADetected = false;
    private bool isFrequencyBDetected = false;
    private bool isFrequencyCDetected = false;

    // Time tracking for delay
    private float timeSinceLastCheck = 0f;

    // Relay state
    private bool isRelayActivated = false;

    private void Update()
    {
        // Increment the timer based on real-time passage
        timeSinceLastCheck += Time.deltaTime;

        // Simulate random frequencies for testing
        float freqA = Random.Range(40f, 80f);
        float freqB = Random.Range(90f, 130f);
        float freqC = Random.Range(140f, 180f);

        Debug.Log($"Simulated Frequencies - A: {freqA} Hz, B: {freqB} Hz, C: {freqC} Hz");

        // Detect frequencies if the delay time has elapsed
        if (timeSinceLastCheck >= DelayTime)
        {
            DetectFrequencies(freqA, freqB, freqC);
        }
        else
        {
            Debug.Log($"Waiting for delay... {DelayTime - timeSinceLastCheck:F2} seconds remaining.");
        }
    }

    private void DetectFrequencies(float freqA, float freqB, float freqC)
    {
        Debug.Log("Starting frequency detection...");

        // Check Frequency A
        if (!isFrequencyADetected && freqA >= FrequencyA_Min && freqA <= FrequencyA_Max)
        {
            isFrequencyADetected = true;
            Debug.Log($"Frequency A detected: {freqA} Hz");
            timeSinceLastCheck = 0f; // Reset timer for delay
            return;
        }

        // Check Frequency B
        if (isFrequencyADetected && !isFrequencyBDetected && freqB >= FrequencyB_Min && freqB <= FrequencyB_Max)
        {
            isFrequencyBDetected = true;
            Debug.Log($"Frequency B detected: {freqB} Hz");
            timeSinceLastCheck = 0f; // Reset timer for delay
            return;
        }

        // Check Frequency C
        if (isFrequencyBDetected && !isFrequencyCDetected && freqC >= FrequencyC_Min && freqC <= FrequencyC_Max)
        {
            isFrequencyCDetected = true;
            Debug.Log($"Frequency C detected: {freqC} Hz");
            timeSinceLastCheck = 0f; // Reset timer for delay
        }

        // Activate relay if all frequencies are detected
        if (isFrequencyADetected && isFrequencyBDetected && isFrequencyCDetected && !isRelayActivated)
        {
            ActivateRelay();
        }
    }

    private void ActivateRelay()
    {
        isRelayActivated = true;
        Debug.Log("Relay activated after all frequencies detected with delay!");
        // Add your logic here for relay activation (e.g., triggering events)
    }

    public void ResetDetection()
    {
        isFrequencyADetected = false;
        isFrequencyBDetected = false;
        isFrequencyCDetected = false;
        isRelayActivated = false;
        timeSinceLastCheck = 0f;
        Debug.Log("Detection process has been reset.");
    }
}
