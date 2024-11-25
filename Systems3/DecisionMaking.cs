public class DecisionMaking
{
    public string MakeDecision(string situation)
    {
        if (situation.Contains("threat")) return "Prepare defense.";
        if (situation.Contains("opportunity")) return "Take advantage.";
        return "Analyze further.";
    }
}
