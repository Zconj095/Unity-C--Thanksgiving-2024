using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ISRES : NLoptOptimizer
{
    public ISRES(int maxEvals = 1000) : base(maxEvals)
    {
        // ISRES constructor that sets max evaluations
    }

    protected override NLoptOptimizerType GetNloptOptimizer()
    {
        // Return the specific optimizer type for ISRES
        return NLoptOptimizerType.GN_ISRES;
    }

    public object InvokeMethod(string methodName, object[] parameters)
    {
        // Use reflection to dynamically invoke methods
        MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (method == null)
        {
            throw new MissingMethodException($"Method {methodName} not found in class {GetType().Name}");
        }
        return method.Invoke(this, parameters);
    }

    public object GetProperty(string propertyName)
    {
        // Use reflection to dynamically access properties
        PropertyInfo property = GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (property == null)
        {
            throw new MissingMemberException($"Property {propertyName} not found in class {GetType().Name}");
        }
        return property.GetValue(this);
    }

    public void SetProperty(string propertyName, object value)
    {
        // Use reflection to dynamically set properties
        PropertyInfo property = GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (property == null)
        {
            throw new MissingMemberException($"Property {propertyName} not found in class {GetType().Name}");
        }
        property.SetValue(this, value);
    }
}

