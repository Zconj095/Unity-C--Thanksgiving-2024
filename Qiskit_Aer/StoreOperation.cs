public class StoreOperation
{
    public string OperationName { get; set; }
    public object Value { get; set; }

    public StoreOperation(string operationName, object value)
    {
        OperationName = operationName;
        Value = value;
    }

    public void Execute()
    {
        // Dummy execution logic, replace with actual logic
        Console.WriteLine($"Executing store operation: {OperationName} with value: {Value}");
    }
}
