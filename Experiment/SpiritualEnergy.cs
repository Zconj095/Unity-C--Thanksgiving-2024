using UnityEngine;

public class SpiritualEnergy : MonoBehaviour
{
    private float maxEnergy; // Maximum energy limit
    private float currentEnergy; // Current energy

    // Constructor to initialize Spiritual Energy
    public SpiritualEnergy(float maxEnergy)
    {
        this.maxEnergy = maxEnergy;
        this.currentEnergy = maxEnergy; // Start with full energy
    }

    // Method to regenerate spiritual energy over time
    public void Regenerate(float amount)
    {
        currentEnergy = Mathf.Min(maxEnergy, currentEnergy + amount);
    }

    // Method to use energy for actions
    public bool UseEnergy(float amount)
    {
        if (amount <= currentEnergy)
        {
            currentEnergy -= amount;
            return true; // Energy usage successful
        }
        return false; // Not enough energy
    }

    // Check if enough energy is available for an action
    public bool IsEnergyAvailable(float amount)
    {
        return amount <= currentEnergy;
    }

    // Return the current energy level
    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }

    // Return the maximum energy level
    public float GetMaxEnergy()
    {
        return maxEnergy;
    }

    private void Update()
    {
        // Example: Regenerate energy over time
        Regenerate(0.1f * Time.deltaTime); // Regenerate energy based on time
    }

    private void Start()
    {
        maxEnergy = 100; // Set maximum energy value
        currentEnergy = maxEnergy; // Initialize current energy
    }
}
