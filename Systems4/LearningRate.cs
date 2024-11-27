using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class LearningRate
{
    private IEnumerator<float> _generator;
    private float? _currentValue;

    public LearningRate(object learningRate)
    {
        if (learningRate is float singleValue)
        {
            _generator = Constant(singleValue).GetEnumerator();
        }
        else if (learningRate is IEnumerable<float> enumerable)
        {
            _generator = enumerable.GetEnumerator();
        }
        else if (learningRate is Func<IEnumerator<float>> generatorFunc)
        {
            _generator = generatorFunc.Invoke();
        }
        else
        {
            throw new ArgumentException("Invalid type for learning rate");
        }

        _currentValue = null;
    }

    public float Current => _currentValue ?? throw new InvalidOperationException("No current value available.");

    public float Send()
    {
        if (_generator.MoveNext())
        {
            _currentValue = _generator.Current;
            return Current;
        }
        else
        {
            throw new InvalidOperationException("Generator has no more values.");
        }
    }

    public void Throw(Type exceptionType, string message = null)
    {
        if (!typeof(Exception).IsAssignableFrom(exceptionType))
        {
            throw new ArgumentException("Type must be an Exception type.", nameof(exceptionType));
        }

        Exception exceptionInstance = string.IsNullOrEmpty(message)
            ? (Exception)Activator.CreateInstance(exceptionType)
            : (Exception)Activator.CreateInstance(exceptionType, message);

        throw exceptionInstance;
    }

    private static IEnumerable<float> Constant(float value)
    {
        while (true)
        {
            yield return value;
        }
    }
}
