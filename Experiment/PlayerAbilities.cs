using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public float attackPower = 10f;   // Player's attack power
    public float speed = 5f;           // Player's movement speed
    private float originalAttackPower;
    private float originalSpeed;

    void Start()
    {
        // Store the original values for reverting later
        originalAttackPower = attackPower;
        originalSpeed = speed;
    }

    public void BoostAbilities()
    {
        attackPower *= 2; // Example boost to attack power
        speed *= 1.5f;    // Example boost to speed
        Debug.Log("Player abilities boosted! New Attack Power: " + attackPower + ", New Speed: " + speed);
    }

    public void RevertAbilities()
    {
        attackPower = originalAttackPower; // Revert to original attack power
        speed = originalSpeed;               // Revert to original speed
        Debug.Log("Player abilities reverted! Attack Power: " + attackPower + ", Speed: " + speed);
    }

    // Fuzzy logic to adjust movement speed based on chakra energy
    public void AdjustMovementSpeedBasedOnChakras(ChakraSystem chakraSystem)
    {
        float totalEnergy = 0;
        foreach (var energy in chakraSystem.chakraEnergies)
        {
            totalEnergy += energy;
        }

        // Normalize energy to a scale (0 to 1)
        float normalizedEnergy = totalEnergy / (chakraSystem.maxEnergy * chakraSystem.chakraEnergies.Length);

        // Fuzzy logic adjustment
        if (normalizedEnergy < 0.3f)
        {
            speed = originalSpeed * 0.5f; // Reduced speed
        }
        else if (normalizedEnergy < 0.7f)
        {
            speed = originalSpeed; // Normal speed
        }
        else
        {
            speed = originalSpeed * 1.5f; // Increased speed
        }
        
        Debug.Log("Adjusted movement speed: " + speed);
    }
}
