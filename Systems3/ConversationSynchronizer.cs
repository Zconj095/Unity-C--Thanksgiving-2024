using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;
public class ConversationSynchronizer
{
    private ConversationHistoryManager historyManager;

    public ConversationSynchronizer(ConversationHistoryManager manager)
    {
        historyManager = manager;
    }

    public string StartNewConversation()
    {
        string conversationId = Guid.NewGuid().ToString();
        historyManager.CreateConversation(conversationId);
        return conversationId;
    }

    public void SynchronizeMessage(string conversationId, string sender, string message)
    {
        var conversation = historyManager.GetConversation(conversationId);
        conversation.AddMessage(sender, message);
    }

    public List<string> GetActiveConversations()
    {
        return historyManager.ListConversations();
    }
}
