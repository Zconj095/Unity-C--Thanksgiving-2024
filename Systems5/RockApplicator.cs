using UnityEngine;

public class RockApplicator : MonoBehaviour
{
    public RockDatabase rockDatabase;
    public SpriteRenderer targetSprite; // For 2D Sprites
    public GameObject targetObject; // For objects using material textures

    public void ApplyRock(string rockName)
    {
        var rockData = rockDatabase.FindRockByName(rockName);
        if (rockData != null && rockData.rockFile != null)
        {
            Sprite rockSprite = Sprite.Create(
                rockData.rockFile, 
                new Rect(0, 0, rockData.rockFile.width, rockData.rockFile.height), 
                new Vector2(0.5f, 0.5f)
            );

            if (targetSprite != null)
            {
                targetSprite.sprite = rockSprite;
            }

            if (targetObject != null)
            {
                var renderer = targetObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.mainTexture = rockData.rockFile;
                }
            }
        }
        else
        {
            Debug.LogWarning($"Rock '{rockName}' not found in the database.");
        }
    }

    public void ApplyRandomRockByTag(string tag)
    {
        var rocks = rockDatabase.FindRocksByTag(tag);
        if (rocks.Count > 0)
        {
            var randomRock = rocks[Random.Range(0, rocks.Count)];
            ApplyRock(randomRock.rockName);
        }
        else
        {
            Debug.LogWarning($"No rocks with tag '{tag}' found.");
        }
    }
}
