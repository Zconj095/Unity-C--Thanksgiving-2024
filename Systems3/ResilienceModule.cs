public class ResilienceModule
{
    public string HandleError(string errorType)
    {
        switch (errorType)
        {
            case "network": return "Reconnecting...";
            case "input": return "Invalid input, please try again.";
            default: return "An unexpected error occurred.";
        }
    }
}
