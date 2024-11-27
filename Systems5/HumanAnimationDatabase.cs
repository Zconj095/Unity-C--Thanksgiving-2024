using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanAnimationDatabase", menuName = "Human Animation Database/Animation Database")]
public class HumanAnimationDatabase : ScriptableObject
{
    public List<HumanAnimationMetadata> animations = new List<HumanAnimationMetadata>();

    public void AddAnimation(HumanAnimationMetadata animation)
    {
        if (!animations.Contains(animation))
        {
            animations.Add(animation);
        }
    }

    public void RemoveAnimation(HumanAnimationMetadata animation)
    {
        if (animations.Contains(animation))
        {
            animations.Remove(animation);
        }
    }

    public HumanAnimationMetadata FindAnimationByName(string name)
    {
        return animations.Find(animation => animation.animationName == name);
    }

    public List<HumanAnimationMetadata> FindAnimationsByType(string animationType)
    {
        return animations.FindAll(animation => animation.animationType == animationType);
    }

    public List<HumanAnimationMetadata> FindAnimationsByTag(string tag)
    {
        return animations.FindAll(animation => System.Array.Exists(animation.tags, t => t == tag));
    }
}
