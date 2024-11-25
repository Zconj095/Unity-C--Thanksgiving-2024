using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class ContextManager2
{
    private Dictionary<string, object> context;

    public ContextManager2()
    {
        context = new Dictionary<string, object>();
    }

    public void SetContext(string key, object value)
    {
        context[key] = value;
    }

    public object GetContext(string key)
    {
        return context.ContainsKey(key) ? context[key] : null;
    }
}
