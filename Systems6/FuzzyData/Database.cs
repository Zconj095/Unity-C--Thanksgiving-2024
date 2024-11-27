using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Represents a fuzzy database, a set of linguistic variables used in a Fuzzy Inference System.
    /// </summary>
    public class Database
    {
        // The repository for linguistic variables
        private Dictionary<string, object> variables;

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        public Database()
        {
            // Instantiate the variables dictionary
            this.variables = new Dictionary<string, object>(10);
        }

        /// <summary>
        /// Adds a linguistic variable to the database.
        /// </summary>
        /// <param name="variable">A linguistic variable to add.</param>
        /// <exception cref="NullReferenceException">Thrown if the linguistic variable is not initialized.</exception>
        /// <exception cref="ArgumentException">Thrown if the linguistic variable name already exists in the database.</exception>
        public void AddVariable(object variable)
        {
            // Use reflection to retrieve the Name property
            Type variableType = variable.GetType();
            PropertyInfo nameProperty = variableType.GetProperty("Name", BindingFlags.Instance | BindingFlags.Public);

            if (nameProperty == null)
                throw new InvalidOperationException("LinguisticVariable does not have a 'Name' property.");

            string variableName = (string)nameProperty.GetValue(variable);

            // Check for existing name
            if (this.variables.ContainsKey(variableName))
                throw new ArgumentException("The linguistic variable name already exists in the database.");

            // Add the variable
            this.variables.Add(variableName, variable);
        }

        /// <summary>
        /// Removes all the linguistic variables from the database.
        /// </summary>
        public void ClearVariables()
        {
            this.variables.Clear();
        }

        /// <summary>
        /// Returns an existing linguistic variable from the database.
        /// </summary>
        /// <param name="variableName">Name of the linguistic variable to retrieve.</param>
        /// <returns>Reference to the named linguistic variable.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the variable is not found in the database.</exception>
        public object GetVariable(string variableName)
        {
            if (!this.variables.ContainsKey(variableName))
                throw new KeyNotFoundException($"Variable '{variableName}' not found in the database.");

            return this.variables[variableName];
        }
    }
}
