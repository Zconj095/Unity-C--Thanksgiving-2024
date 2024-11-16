using System;
using System.Collections.Generic;

public class Nduv
{
    public DateTime Date { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public double Value { get; set; }

    public Nduv(DateTime date, string name, string unit, double value)
    {
        Date = date;
        Name = name;
        Unit = unit;
        Value = value;
    }

    public static Nduv FromDictionary(Dictionary<string, object> data)
    {
        return new Nduv(
            Convert.ToDateTime(data["date"]),
            data["name"].ToString(),
            data["unit"].ToString(),
            Convert.ToDouble(data["value"])
        );
    }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "date", Date },
            { "name", Name },
            { "unit", Unit },
            { "value", Value }
        };
    }

    public override bool Equals(object obj)
    {
        return obj is Nduv other && ToDictionary().Equals(other.ToDictionary());
    }

    public override string ToString()
    {
        return $"Nduv({Date}, {Name}, {Unit}, {Value})";
    }
}
