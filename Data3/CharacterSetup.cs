using UnityEngine;

public class CharacterSetup : MonoBehaviour
{
    [SerializeField] private CharacterAttributePreset attributePreset;

    private void Start()
    {
        var attributeAbility = GetComponent<CharacterAttributeAbility>();
        if (attributeAbility != null && attributePreset != null)
        {
            attributePreset.ApplyAttributes(attributeAbility); // Now this should work
        }
    }
}
