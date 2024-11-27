using UnityEngine;
using UnityEngine.UI;

public class FlowerApplicator : MonoBehaviour
{
    public FlowerDatabase flowerDatabase;
    public Image targetImage; // For UI
    public SpriteRenderer targetSprite; // For 2D Sprites

    public void ApplyFlower(string flowerName)
    {
        var flowerData = flowerDatabase.FindFlowerByName(flowerName);
        if (flowerData != null && flowerData.flowerFile != null)
        {
            Sprite flowerSprite = Sprite.Create(
                flowerData.flowerFile, 
                new Rect(0, 0, flowerData.flowerFile.width, flowerData.flowerFile.height), 
                new Vector2(0.5f, 0.5f)
            );

            if (targetImage != null)
            {
                targetImage.sprite = flowerSprite;
            }

            if (targetSprite != null)
            {
                targetSprite.sprite = flowerSprite;
            }
        }
        else
        {
            Debug.LogWarning($"Flower '{flowerName}' not found in the database.");
        }
    }

    public void ApplyRandomFlowerByTag(string tag)
    {
        var flowers = flowerDatabase.FindFlowersByTag(tag);
        if (flowers.Count > 0)
        {
            var randomFlower = flowers[Random.Range(0, flowers.Count)];
            ApplyFlower(randomFlower.flowerName);
        }
        else
        {
            Debug.LogWarning($"No flowers with tag '{tag}' found.");
        }
    }
}
