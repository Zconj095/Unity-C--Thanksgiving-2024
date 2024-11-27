using UnityEngine;

public class BushApplicator : MonoBehaviour
{
    public BushDatabase bushDatabase;
    public SpriteRenderer targetSprite; // For 2D Sprites
    public GameObject targetObject; // For objects using material textures

    public void ApplyBush(string bushName)
    {
        var bushData = bushDatabase.FindBushByName(bushName);
        if (bushData != null && bushData.bushFile != null)
        {
            Sprite bushSprite = Sprite.Create(
                bushData.bushFile, 
                new Rect(0, 0, bushData.bushFile.width, bushData.bushFile.height), 
                new Vector2(0.5f, 0.5f)
            );

            if (targetSprite != null)
            {
                targetSprite.sprite = bushSprite;
            }

            if (targetObject != null)
            {
                var renderer = targetObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.mainTexture = bushData.bushFile;
                }
            }
        }
        else
        {
            Debug.LogWarning($"Bush '{bushName}' not found in the database.");
        }
    }

    public void ApplyRandomBushByTag(string tag)
    {
        var bushes = bushDatabase.FindBushesByTag(tag);
        if (bushes.Count > 0)
        {
            var randomBush = bushes[Random.Range(0, bushes.Count)];
            ApplyBush(randomBush.bushName);
        }
        else
        {
            Debug.LogWarning($"No bushes with tag '{tag}' found.");
        }
    }
}
