using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;
using UnityEngine;

[RequireComponent(typeof(CharacterAttributes))]
public class CharacterAttributeAbility : Ability
{
    private CharacterAttributes characterAttributes;

    // Ensure the method is public to match the base class.
    public override void Awake()
    {
        base.Awake();
        characterAttributes = GetComponent<CharacterAttributes>();
    }

    public void SetAttributes(float agility, float dexterity, float mobility, float vigilance, float stamina, float weight)
    {
        characterAttributes.Agility = agility;
        characterAttributes.Dexterity = dexterity;
        characterAttributes.Mobility = mobility;
        characterAttributes.Vigilance = vigilance;
        characterAttributes.Stamina = stamina;
        characterAttributes.Weight = weight;
    }
}
