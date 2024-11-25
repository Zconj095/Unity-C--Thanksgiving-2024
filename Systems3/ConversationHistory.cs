using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class ConversationHistory
{
    public string ConversationId { get; private set; }
    public List<ConversationMessage> Messages { get; private set; }

    public ConversationHistory(string conversationId)
    {
        ConversationId = conversationId;
        Messages = new List<ConversationMessage>();
    }

    public void AddMessage(string sender, string message)
    {
        Messages.Add(new ConversationMessage(sender, message));
    }
}
