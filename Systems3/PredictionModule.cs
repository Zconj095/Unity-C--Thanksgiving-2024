public class PredictionModule
{
    public string PredictOutcome(string input)
    {
        if (input.Contains("weather")) return "It might rain tomorrow.";
        if (input.Contains("schedule")) return "Your meeting is likely to run late.";
        return "No prediction available.";
    }
}
