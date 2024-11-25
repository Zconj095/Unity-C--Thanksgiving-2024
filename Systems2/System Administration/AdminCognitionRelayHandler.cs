using System;
using UnityEngine;
using System.Collections.Generic;
public class AdminCognitionRelayHandler : MonoBehaviour
{
    // Input Processing Module
    [Serializable]
    public class InputData
    {
        public string command;
        public string parameters;
    }

    // State Manager
    private class StateManager
    {
        private readonly Dictionary<string, object> states = new Dictionary<string, object>();

        public void UpdateState(string key, object value)
        {
            if (states.ContainsKey(key))
            {
                states[key] = value;
            }
            else
            {
                states.Add(key, value);
            }
        }

        public T GetState<T>(string key)
        {
            if (states.ContainsKey(key))
                return (T)states[key];
            return default;
        }
    }

    private StateManager stateManager = new StateManager();

    // Event Dispatcher
    public event Action<string, object> OnFeedbackRelay;

    // Logging system
    private void LogFeedback(string message)
    {
        Debug.Log("[ACRH Feedback]: " + message);
    }

    void Start()
    {
        // Initialize states
        stateManager.UpdateState("SystemActive", true);
        stateManager.UpdateState("FeedbackCount", 0);
    }

    // Main Cognition Relay Handler
    public void ProcessInput(InputData input)
    {
        if (!(stateManager.GetState<bool>("SystemActive")))
        {
            LogFeedback("System is inactive.");
            return;
        }

        switch (input.command.ToLower())
        {
            case "update":
                HandleUpdateCommand(input.parameters);
                break;
            case "reset":
                HandleResetCommand();
                break;
            default:
                LogFeedback($"Unknown command: {input.command}");
                break;
        }
    }

    private void HandleUpdateCommand(string parameters)
    {
        LogFeedback($"Processing update with parameters: {parameters}");

        // Update state or perform action
        stateManager.UpdateState("LastUpdateParams", parameters);
        int feedbackCount = stateManager.GetState<int>("FeedbackCount") + 1;
        stateManager.UpdateState("FeedbackCount", feedbackCount);

        // Trigger feedback event
        OnFeedbackRelay?.Invoke("UpdateProcessed", parameters);
    }

    private void HandleResetCommand()
    {
        LogFeedback("System resetting...");
        stateManager.UpdateState("FeedbackCount", 0);
        stateManager.UpdateState("LastUpdateParams", null);
    }

    void Update()
    {
        // Example: Periodic feedback monitoring
        int feedbackCount = stateManager.GetState<int>("FeedbackCount");
        if (feedbackCount > 5)
        {
            LogFeedback("High feedback activity detected.");
        }
    }
}
