using UnityEngine;

[CreateAssetMenu(fileName = "New Character Attribute Preset", menuName = "Character Presets/Character Attribute Preset")]
public class CharacterAttributePreset : MonoBehaviour
{
    public float Agility = 100f;
    public float Dexterity = 100f;
    public float Mobility = 100f;
    public float Vigilance = 100f;
    public float Stamina = 100f;
    public float Weight = 70f; // Example value

    public void ApplyAttributes(PlayerAbilities abilities, ChakraSystem chakraSystem)
    {
        if (chakraSystem == null) return;

        // Adjust attributes based on chakra energies
        Agility += chakraSystem.GetChakraEnergy(0) * 0.1f; // Example: Root Chakra affects Agility
        Dexterity += chakraSystem.GetChakraEnergy(1) * 0.1f; // Sacral Chakra affects Dexterity
        Mobility += chakraSystem.GetChakraEnergy(2) * 0.1f; // Solar Plexus Chakra affects Mobility
        Vigilance += chakraSystem.GetChakraEnergy(3) * 0.1f; // Heart Chakra affects Vigilance
        Stamina += chakraSystem.GetChakraEnergy(4) * 0.1f; // Throat Chakra affects Stamina
        Weight += chakraSystem.GetChakraEnergy(5) * 0.01f; // Third Eye Chakra affects Weight

        abilities.attackPower += Agility; // Example application
        abilities.speed += Mobility; // Example application
    }

    public void ApplyAttributes(CharacterAttributeAbility attributeAbility)
    {
        attributeAbility.SetAttributes(Agility, Dexterity, Mobility, Vigilance, Stamina, Weight);
    }
}
