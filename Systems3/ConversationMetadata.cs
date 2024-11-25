using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;

public class ConversationMetadata
{
    public List<string> LinkedVectorIds { get; private set; }

    public ConversationMetadata()
    {
        LinkedVectorIds = new List<string>();
    }

    public void LinkVector(string vectorId)
    {
        if (!LinkedVectorIds.Contains(vectorId))
        {
            LinkedVectorIds.Add(vectorId);
        }
    }

    public void UnlinkVector(string vectorId)
    {
        LinkedVectorIds.Remove(vectorId);
    }
}
