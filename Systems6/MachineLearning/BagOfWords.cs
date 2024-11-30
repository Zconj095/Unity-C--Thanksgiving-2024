using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Bag of Words implementation using Unity Reflection and Unity-compatible libraries.
    /// </summary>
    public class BagOfWords
    {
        private Dictionary<string, int> _stringToCode;
        private Dictionary<int, string> _codeToString;

        private IReadOnlyDictionary<string, int> _readOnlyStringToCode;
        private IReadOnlyDictionary<int, string> _readOnlyCodeToString;

        public int MaximumOccurrence { get; set; }
        public int NumberOfWords => _stringToCode.Count;

        public IReadOnlyDictionary<string, int> StringToCode => _readOnlyStringToCode;
        public IReadOnlyDictionary<int, string> CodeToString => _readOnlyCodeToString;

        /// <summary>
        /// Constructor to initialize the Bag of Words model.
        /// </summary>
        public BagOfWords()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the data structures.
        /// </summary>
        private void Initialize()
        {
            _stringToCode = new Dictionary<string, int>();
            _codeToString = new Dictionary<int, string>();

            _readOnlyStringToCode = _stringToCode;
            _readOnlyCodeToString = _codeToString;

            MaximumOccurrence = 1;
        }

        /// <summary>
        /// Learns the vocabulary from the given texts.
        /// </summary>
        /// <param name="texts">Array of text sequences to build the vocabulary.</param>
        public void Learn(string[][] texts)
        {
            if (texts == null) throw new ArgumentNullException(nameof(texts));

            int symbol = 0;
            foreach (var text in texts)
            {
                foreach (var word in text)
                {
                    if (!_stringToCode.ContainsKey(word))
                    {
                        _stringToCode[word] = symbol;
                        _codeToString[symbol] = word;
                        symbol++;
                    }
                }
            }
        }

        /// <summary>
        /// Transforms a single text input into a feature vector.
        /// </summary>
        /// <param name="input">The input text sequence.</param>
        /// <returns>An integer array representing the feature vector.</returns>
        public int[] Transform(string[] input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var result = new int[NumberOfWords];
            foreach (var word in input)
            {
                if (_stringToCode.TryGetValue(word, out int index))
                {
                    if (result[index] < MaximumOccurrence)
                    {
                        result[index]++;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Transforms multiple text inputs into feature vectors.
        /// </summary>
        /// <param name="inputs">The array of text sequences.</param>
        /// <returns>A 2D array where each row corresponds to a feature vector.</returns>
        public int[][] Transform(string[][] inputs)
        {
            if (inputs == null) throw new ArgumentNullException(nameof(inputs));

            var results = new int[inputs.Length][];
            for (int i = 0; i < inputs.Length; i++)
            {
                results[i] = Transform(inputs[i]);
            }
            return results;
        }

        /// <summary>
        /// Reflectively gets the value of a property from an object.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value of the property.</returns>
        public static object GetProperty(object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return property?.GetValue(obj);
        }

        /// <summary>
        /// Reflectively sets the value of a property on an object.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value to set.</param>
        public static void SetProperty(object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            property?.SetValue(obj, value);
        }

        /// <summary>
        /// Reflectively invokes a method on an object.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="parameters">An array of parameters to pass to the method.</param>
        /// <returns>The result of the method invocation.</returns>
        public static object InvokeMethod(object obj, string methodName, object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return method?.Invoke(obj, parameters);
        }
    }
}
