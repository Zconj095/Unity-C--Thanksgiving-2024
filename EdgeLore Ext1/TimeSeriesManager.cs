using System.Collections.Generic;
using UnityEngine;

public class TimeSeriesManager : MonoBehaviour
{
    private List<HiddenTimeField> timeFields = new List<HiddenTimeField>();
    public int maxRecords = 100; // Limit the number of records to save memory

    public void AddTimeField(float timeStamp, Dictionary<string, float> variables)
    {
        HiddenTimeField newField = new HiddenTimeField(timeStamp) { Variables = variables };
        timeFields.Add(newField);

        // Limit the size of stored records
        if (timeFields.Count > maxRecords)
        {
            timeFields.RemoveAt(0);
        }
    }

    public List<HiddenTimeField> GetTimeFields()
    {
        return new List<HiddenTimeField>(timeFields); // Return a copy of the data
    }
}
