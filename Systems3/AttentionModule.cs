public class AttentionModule
{
    public float ComputeAttentionScore(string input, string context)
    {
        // Simplified example: match input relevance to context
        float score = 0f;
        string[] keywords = context.Split(' ');
        foreach (string keyword in keywords)
        {
            if (input.Contains(keyword)) score += 1f;
        }
        return score / keywords.Length;
    }
}
