using UnityEngine;

public class FieldDomainSystem : MonoBehaviour
{
    public float[] domainData;

    public void ReceiveFieldData(float[] data)
    {
        domainData = data;
        Debug.Log("Field Domain System updated with new data.");
    }
}
