using UnityEngine;

public class CloudApplicator : MonoBehaviour
{
    public CloudDatabase cloudDatabase;
    public SpriteRenderer targetSprite; // For 2D Sprites
    public ParticleSystemRenderer particleRenderer; // For Particle Systems

    public void ApplyCloud(string cloudName)
    {
        var cloudData = cloudDatabase.FindCloudByName(cloudName);
        if (cloudData != null && cloudData.cloudFile != null)
        {
            Sprite cloudSprite = Sprite.Create(
                cloudData.cloudFile, 
                new Rect(0, 0, cloudData.cloudFile.width, cloudData.cloudFile.height), 
                new Vector2(0.5f, 0.5f)
            );

            if (targetSprite != null)
            {
                targetSprite.sprite = cloudSprite;
            }

            if (particleRenderer != null)
            {
                particleRenderer.material.mainTexture = cloudData.cloudFile;
            }
        }
        else
        {
            Debug.LogWarning($"Cloud '{cloudName}' not found in the database.");
        }
    }

    public void ApplyRandomCloudByTag(string tag)
    {
        var clouds = cloudDatabase.FindCloudsByTag(tag);
        if (clouds.Count > 0)
        {
            var randomCloud = clouds[Random.Range(0, clouds.Count)];
            ApplyCloud(randomCloud.cloudName);
        }
        else
        {
            Debug.LogWarning($"No clouds with tag '{tag}' found.");
        }
    }
}
