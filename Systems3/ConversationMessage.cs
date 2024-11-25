using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class ConversationMessage
{
    public string Sender { get; set; } // "User" or "AI"
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }

    public ConversationMessage(string sender, string message)
    {
        Sender = sender;
        Message = message;
        Timestamp = DateTime.UtcNow;
    }
}

