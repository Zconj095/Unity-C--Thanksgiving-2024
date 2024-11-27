using UnityEngine;

[CreateAssetMenu(fileName = "AnimationMetadata", menuName = "Animation Database/Animation Metadata")]
public class AnimationMetadata : ScriptableObject
{
    public string animationName;
    public string creatureType; // e.g., "Dragon", "Zombie", "Robot"
    public AnimationClip animationClip;
    public string[] tags; // Tags for filtering, e.g., "Attack", "Idle"
    public float duration; // Length of the animation
}
