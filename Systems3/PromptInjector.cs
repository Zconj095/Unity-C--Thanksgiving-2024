public class PromptInjector
{
    private string currentPrompt = "";

    public void Inject(string newPrompt)
    {
        currentPrompt += newPrompt + "\n";
    }

    public string GetPrompt()
    {
        return currentPrompt;
    }
}
