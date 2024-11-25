using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class QueryParser
{
    public static string Preprocess(string input)
    {
        // Convert to lowercase
        input = input.ToLower();

        // Remove special characters
        input = Regex.Replace(input, @"[^\w\s]", "");

        // Normalize whitespace
        input = Regex.Replace(input, @"\s+", " ").Trim();

        return input;
    }

    public static int[] Tokenize(string input)
    {
        string[] words = input.Split(' ');
        return words.Select(word => word.GetHashCode()).ToArray();
    }
}
