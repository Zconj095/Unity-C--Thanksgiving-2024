using System.Collections.Generic;
using UnityEngine;

public class SynergyInputManager : MonoBehaviour
{
    private Dictionary<string, float> inputs = new Dictionary<string, float>();

    public void AddInput(string key, float value)
    {
        if (inputs.ContainsKey(key))
        {
            inputs[key] = value;
        }
        else
        {
            inputs.Add(key, value);
        }
    }

    public Dictionary<string, float> GetInputs()
    {
        return new Dictionary<string, float>(inputs);
    }

    public float GetInputValue(string key)
    {
        return inputs.ContainsKey(key) ? inputs[key] : 0f;
    }
}
