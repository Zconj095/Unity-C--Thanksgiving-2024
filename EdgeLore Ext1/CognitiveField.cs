using UnityEngine;

public class CognitiveField : MonoBehaviour
{
    public float[] fieldData; // Simulated field layer data.

    public void ShiftToFieldDomainSystem(FieldDomainSystem targetSystem)
    {
        targetSystem.ReceiveFieldData(fieldData);
        Debug.Log("Cognitive Field Layer data shifted to Field Domain System.");
    }
}

