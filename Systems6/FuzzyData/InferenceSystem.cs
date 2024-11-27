using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Represents a Fuzzy Inference System capable of executing fuzzy computations.
    /// </summary>
    public class InferenceSystem
    {
        private readonly object database;
        private readonly object defuzzifier;
        private readonly object normOperator;
        private readonly object conormOperator;
        private readonly object rulebase;

        public InferenceSystem(object database, object defuzzifier)
            : this(database, defuzzifier, Activator.CreateInstance(typeof(MinimumNorm)), Activator.CreateInstance(typeof(MaximumCoNorm)))
        {
        }

        public InferenceSystem(object database, object defuzzifier, object normOperator, object conormOperator)
        {
            this.database = database;
            this.defuzzifier = defuzzifier;
            this.normOperator = normOperator;
            this.conormOperator = conormOperator;
            this.rulebase = Activator.CreateInstance(typeof(Rulebase));
        }

        public object NewRule(string name, string rule)
        {
            object ruleInstance = Activator.CreateInstance(typeof(Rule), database, name, rule, normOperator, conormOperator);
            MethodInfo addRuleMethod = rulebase.GetType().GetMethod("AddRule", BindingFlags.Instance | BindingFlags.Public);
            addRuleMethod?.Invoke(rulebase, new[] { ruleInstance });
            return ruleInstance;
        }

        public void SetInput(string variableName, float value)
        {
            MethodInfo getVariableMethod = database.GetType().GetMethod("GetVariable", BindingFlags.Instance | BindingFlags.Public);
            object variable = getVariableMethod?.Invoke(database, new object[] { variableName });
            PropertyInfo numericInputProperty = variable?.GetType().GetProperty("NumericInput", BindingFlags.Instance | BindingFlags.Public);
            numericInputProperty?.SetValue(variable, value);
        }

        public object GetLinguisticVariable(string variableName)
        {
            MethodInfo getVariableMethod = database.GetType().GetMethod("GetVariable", BindingFlags.Instance | BindingFlags.Public);
            return getVariableMethod?.Invoke(database, new object[] { variableName });
        }

        public object GetRule(string ruleName)
        {
            MethodInfo getRuleMethod = rulebase.GetType().GetMethod("GetRule", BindingFlags.Instance | BindingFlags.Public);
            return getRuleMethod?.Invoke(rulebase, new object[] { ruleName });
        }

        public float Evaluate(string variableName)
        {
            object fuzzyOutput = ExecuteInference(variableName);
            MethodInfo defuzzifyMethod = defuzzifier.GetType().GetMethod("Defuzzify", BindingFlags.Instance | BindingFlags.Public);
            return (float)defuzzifyMethod?.Invoke(defuzzifier, new[] { fuzzyOutput, normOperator });
        }

        public object ExecuteInference(string variableName)
        {
            MethodInfo getVariableMethod = database.GetType().GetMethod("GetVariable", BindingFlags.Instance | BindingFlags.Public);
            object linguisticVariable = getVariableMethod?.Invoke(database, new object[] { variableName });

            object fuzzyOutput = Activator.CreateInstance(typeof(FuzzyOutput), linguisticVariable);

            MethodInfo getRulesMethod = rulebase.GetType().GetMethod("GetRules", BindingFlags.Instance | BindingFlags.Public);
            Array rules = (Array)getRulesMethod?.Invoke(rulebase, null);

            foreach (object rule in rules)
            {
                PropertyInfo outputProperty = rule.GetType().GetProperty("Output", BindingFlags.Instance | BindingFlags.Public);
                object output = outputProperty?.GetValue(rule);

                PropertyInfo variableProperty = output?.GetType().GetProperty("Variable", BindingFlags.Instance | BindingFlags.Public);
                string ruleVariableName = (string)variableProperty?.GetValue(output);

                if (ruleVariableName == variableName)
                {
                    PropertyInfo labelProperty = output.GetType().GetProperty("Label", BindingFlags.Instance | BindingFlags.Public);
                    string labelName = (string)labelProperty?.GetValue(output);

                    MethodInfo evaluateFiringStrengthMethod = rule.GetType().GetMethod("EvaluateFiringStrength", BindingFlags.Instance | BindingFlags.Public);
                    float firingStrength = (float)evaluateFiringStrengthMethod?.Invoke(rule, null);

                    if (firingStrength > 0)
                    {
                        MethodInfo addOutputMethod = fuzzyOutput.GetType().GetMethod("AddOutput", BindingFlags.Instance | BindingFlags.NonPublic);
                        addOutputMethod?.Invoke(fuzzyOutput, new object[] { labelName, firingStrength });
                    }
                }
            }

            return fuzzyOutput;
        }
    }
}
