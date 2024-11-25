using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine;

public class ConversationHistoryManager
{
    private Dictionary<string, ConversationHistory> conversations;

    public ConversationHistoryManager()
    {
        conversations = new Dictionary<string, ConversationHistory>();
    }

    public ConversationHistory CreateConversation(string conversationId)
    {
        if (conversations.ContainsKey(conversationId))
        {
            throw new Exception($"Conversation ID {conversationId} already exists.");
        }

        ConversationHistory newConversation = new ConversationHistory(conversationId);
        conversations[conversationId] = newConversation;
        return newConversation;
    }

    public ConversationHistory GetConversation(string conversationId)
    {
        if (conversations.ContainsKey(conversationId))
        {
            return conversations[conversationId];
        }

        throw new Exception($"Conversation ID {conversationId} not found.");
    }

    public void DeleteConversation(string conversationId)
    {
        if (conversations.ContainsKey(conversationId))
        {
            conversations.Remove(conversationId);
        }
    }

    public List<string> ListConversations()
    {
        return new List<string>(conversations.Keys);
    }
}
