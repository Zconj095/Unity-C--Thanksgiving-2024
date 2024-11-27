using UnityEngine;

public class HumanAnimationController : MonoBehaviour
{
    public HumanAnimationDatabase animationDatabase;
    public Animator animator;

    public void PlayAnimation(string animationName)
    {
        var animation = animationDatabase.FindAnimationByName(animationName);
        if (animation != null && animation.animationClip != null)
        {
            animator.Play(animation.animationClip.name);
        }
        else
        {
            Debug.LogWarning($"Animation '{animationName}' not found in the database.");
        }
    }

    public void PlayAnimationByTag(string tag)
    {
        var animations = animationDatabase.FindAnimationsByTag(tag);
        if (animations.Count > 0)
        {
            var randomAnimation = animations[Random.Range(0, animations.Count)];
            animator.Play(randomAnimation.animationClip.name);
        }
        else
        {
            Debug.LogWarning($"No animations with tag '{tag}' found.");
        }
    }    
}
