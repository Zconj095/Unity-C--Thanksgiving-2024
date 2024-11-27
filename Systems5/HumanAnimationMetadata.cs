using UnityEngine;

[CreateAssetMenu(fileName = "HumanAnimationMetadata", menuName = "Human Animation Database/Animation Metadata")]
public class HumanAnimationMetadata : ScriptableObject
{
    public string animationName;
    public AnimationClip animationClip; // Reference to the animation clip
    public string animationType; // e.g., "Idle", "Run", "Attack"
    public float duration; // Duration of the animation
    public string[] tags; // Tags for filtering, e.g., "Combat", "Casual"
    public string description; // Optional description of the animation
}
