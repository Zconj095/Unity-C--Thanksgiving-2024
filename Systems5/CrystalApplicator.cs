using UnityEngine;

public class CrystalApplicator : MonoBehaviour
{
    public CrystalDatabase crystalDatabase;
    public SpriteRenderer targetSprite; // For 2D Sprites
    public GameObject targetObject; // For objects using material textures

    public void ApplyCrystal(string crystalName)
    {
        var crystalData = crystalDatabase.FindCrystalByName(crystalName);
        if (crystalData != null && crystalData.crystalFile != null)
        {
            Sprite crystalSprite = Sprite.Create(
                crystalData.crystalFile, 
                new Rect(0, 0, crystalData.crystalFile.width, crystalData.crystalFile.height), 
                new Vector2(0.5f, 0.5f)
            );

            if (targetSprite != null)
            {
                targetSprite.sprite = crystalSprite;
            }

            if (targetObject != null)
            {
                var renderer = targetObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.mainTexture = crystalData.crystalFile;
                }
            }
        }
        else
        {
            Debug.LogWarning($"Crystal '{crystalName}' not found in the database.");
        }
    }

    public void ApplyRandomCrystalByTag(string tag)
    {
        var crystals = crystalDatabase.FindCrystalsByTag(tag);
        if (crystals.Count > 0)
        {
            var randomCrystal = crystals[Random.Range(0, crystals.Count)];
            ApplyCrystal(randomCrystal.crystalName);
        }
        else
        {
            Debug.LogWarning($"No crystals with tag '{tag}' found.");
        }
    }
}
