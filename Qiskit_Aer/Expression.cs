public class Expression
{
    public string ExpressionString { get; set; }

    public Expression(string expression)
    {
        ExpressionString = expression;
    }

    // You can add more functionality depending on the expected operations.
    public bool Evaluate()
    {
        // Dummy evaluation logic, replace with actual logic
        return !string.IsNullOrEmpty(ExpressionString);
    }

    public override string ToString()
    {
        return ExpressionString;
    }
}
