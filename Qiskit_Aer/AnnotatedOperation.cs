using System;

public class AnnotatedOperation
{
    public string OperationName { get; set; }
    public string Annotation { get; set; }

    public AnnotatedOperation(string operationName, string annotation)
    {
        OperationName = operationName;
        Annotation = annotation;
    }

    // Display information about the annotated operation
    public void Display()
    {
        Console.WriteLine($"Operation: {OperationName}, Annotation: {Annotation}");
    }

    // Example method to modify the annotation
    public void UpdateAnnotation(string newAnnotation)
    {
        Annotation = newAnnotation;
        Console.WriteLine($"Updated annotation to: {Annotation}");
    }
}
