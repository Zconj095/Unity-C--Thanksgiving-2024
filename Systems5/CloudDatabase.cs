using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CloudDatabase", menuName = "Cloud Database/Database")]
public class CloudDatabase : ScriptableObject
{
    public List<CloudMetadata> clouds = new List<CloudMetadata>();

    public void AddCloud(CloudMetadata cloud)
    {
        if (!clouds.Contains(cloud))
        {
            clouds.Add(cloud);
        }
    }

    public void RemoveCloud(CloudMetadata cloud)
    {
        if (clouds.Contains(cloud))
        {
            clouds.Remove(cloud);
        }
    }

    public CloudMetadata FindCloudByName(string name)
    {
        return clouds.Find(cloud => cloud.cloudName == name);
    }

    public List<CloudMetadata> FindCloudsByTag(string tag)
    {
        return clouds.FindAll(cloud => System.Array.Exists(cloud.tags, t => t == tag));
    }

    public List<CloudMetadata> FindCloudsByType(string type)
    {
        return clouds.FindAll(cloud => cloud.cloudType == type);
    }
}
