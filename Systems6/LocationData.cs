using UnityEngine;
public class LocationData : MonoBehaviour
{
    public string RegionName = "Location One";
    public int DomainLocation = 1;
    public int SensitivityLevel = 5;

    public string GetLocationDetails()
    {
        return $"Region: {RegionName}, Danger Level: {DomainLocation}, Region: {SensitivityLevel}";
    }
}
