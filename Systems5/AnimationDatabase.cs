using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationDatabase", menuName = "Animation Database/Animation Database")]
public class AnimationDatabase : ScriptableObject
{
    public List<AnimationMetadata> animations = new List<AnimationMetadata>();

    public void AddAnimation(AnimationMetadata animationData)
    {
        if (!animations.Contains(animationData))
        {
            animations.Add(animationData);
        }
    }

    public void RemoveAnimation(AnimationMetadata animationData)
    {
        if (animations.Contains(animationData))
        {
            animations.Remove(animationData);
        }
    }

    public AnimationMetadata FindAnimationByName(string name)
    {
        return animations.Find(animation => animation.animationName == name);
    }

    public List<AnimationMetadata> FindAnimationsByCreatureType(string creatureType)
    {
        return animations.FindAll(animation => animation.creatureType == creatureType);
    }

    public List<AnimationMetadata> FindAnimationsByTag(string tag)
    {
        return animations.FindAll(animation => System.Array.Exists(animation.tags, t => t == tag));
    }
}
