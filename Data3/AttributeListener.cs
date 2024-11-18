using UnityEngine;

public class AttributeListener : MonoBehaviour
{
    [SerializeField] private CharacterAttributes characterAttributes;

    private void OnEnable()
    {
        characterAttributes.OnAgilityChanged += HandleAgilityChanged;
        characterAttributes.OnDexterityChanged += HandleDexterityChanged;
        characterAttributes.OnMobilityChanged += HandleMobilityChanged;
        characterAttributes.OnVigilanceChanged += HandleVigilanceChanged;
        characterAttributes.OnStaminaChanged += HandleStaminaChanged;
        characterAttributes.OnWeightChanged += HandleWeightChanged;
    }

    private void OnDisable()
    {
        characterAttributes.OnAgilityChanged -= HandleAgilityChanged;
        characterAttributes.OnDexterityChanged -= HandleDexterityChanged;
        characterAttributes.OnMobilityChanged -= HandleMobilityChanged;
        characterAttributes.OnVigilanceChanged -= HandleVigilanceChanged;
        characterAttributes.OnStaminaChanged -= HandleStaminaChanged;
        characterAttributes.OnWeightChanged -= HandleWeightChanged;
    }

    private void HandleAgilityChanged(float newValue)
    {
        Debug.Log($"Agility changed to: {newValue}");
        // Additional logic for agility changes
    }

    private void HandleDexterityChanged(float newValue)
    {
        Debug.Log($"Dexterity changed to: {newValue}");
        // Additional logic for dexterity changes
    }

    private void HandleMobilityChanged(float newValue)
    {
        Debug.Log($"Mobility changed to: {newValue}");
        // Additional logic for mobility changes
    }

    private void HandleVigilanceChanged(float newValue)
    {
        Debug.Log($"Vigilance changed to: {newValue}");
        // Additional logic for vigilance changes
    }

    private void HandleStaminaChanged(float newValue)
    {
        Debug.Log($"Stamina changed to: {newValue}");
        // Additional logic for stamina changes
    }

    private void HandleWeightChanged(float newValue)
    {
        Debug.Log($"Weight changed to: {newValue}");
        // Additional logic for weight changes
    }
}
