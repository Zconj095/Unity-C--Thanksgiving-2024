using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Represents a Fuzzy Rule, a linguistic expression that drives a Fuzzy Inference System's behavior.
    /// </summary>
    public class Rule
    {
        private string name;
        private string rule;
        private readonly List<object> rpnTokenList;
        private readonly object database;
        private readonly object normOperator;
        private readonly object conormOperator;
        private readonly object notOperator;
        private readonly string unaryOperators = "NOT;VERY";
        private object output;

        /// <summary>
        /// Name of the rule.
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Consequent of the rule.
        /// </summary>
        public object Output => output;

        public Rule(object database, string name, string rule, object normOperator, object conormOperator)
        {
            this.database = database;
            this.name = name;
            this.rule = rule;
            this.normOperator = normOperator;
            this.conormOperator = conormOperator;
            this.notOperator = Activator.CreateInstance(typeof(NotOperator));
            rpnTokenList = new List<object>();
            ParseRule();
        }

        public Rule(object database, string name, string rule)
            : this(database, name, rule, Activator.CreateInstance(typeof(MinimumNorm)), Activator.CreateInstance(typeof(MaximumCoNorm)))
        {
        }

        public string GetRPNExpression()
        {
            string result = "";
            foreach (var token in rpnTokenList)
            {
                result += token is Clause clause ? clause.ToString() : token.ToString();
                result += ", ";
            }
            return result.TrimEnd(',', ' ');
        }

        private int Priority(string operatorToken)
        {
            if (unaryOperators.Contains(operatorToken)) return 4;

            return operatorToken switch
            {
                "(" => 1,
                "OR" => 2,
                "AND" => 3,
                _ => 0
            };
        }

        private void ParseRule()
        {
            bool consequent = false;
            string[] tokens = GetRuleTokens(rule);
            Stack<string> operatorStack = new();

            foreach (var token in tokens)
            {
                string trimmedToken = token.Trim();
                string upperToken = trimmedToken.ToUpper();

                if (string.IsNullOrEmpty(trimmedToken) || upperToken == "IF") continue;

                if (upperToken == "THEN")
                {
                    consequent = true;
                    continue;
                }

                if (!consequent && upperToken == "(")
                {
                    operatorStack.Push(upperToken);
                }
                else if (!consequent && upperToken == ")")
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                        rpnTokenList.Add(operatorStack.Pop());

                    if (operatorStack.Count == 0)
                        throw new ArgumentException("Unmatched parenthesis in rule.");

                    operatorStack.Pop();
                }
                else if (unaryOperators.Contains(upperToken) || upperToken == "AND" || upperToken == "OR")
                {
                    while (operatorStack.Count > 0 && Priority(operatorStack.Peek()) >= Priority(upperToken))
                        rpnTokenList.Add(operatorStack.Pop());

                    operatorStack.Push(upperToken);
                }
                else
                {
                    if (!consequent)
                    {
                        MethodInfo getVariableMethod = database.GetType().GetMethod("GetVariable");
                        var variable = getVariableMethod?.Invoke(database, new object[] { trimmedToken });

                        if (variable == null)
                            throw new ArgumentException($"Variable {trimmedToken} not found in database.");

                        rpnTokenList.Add(variable);
                    }
                    else
                    {
                        output = trimmedToken;
                    }
                }
            }

            while (operatorStack.Count > 0)
                rpnTokenList.Add(operatorStack.Pop());
        }

        private string[] GetRuleTokens(string rule)
        {
            rule = rule.Replace("(", " ( ").Replace(")", " ) ");
            return rule.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        public float EvaluateFiringStrength()
        {
            Stack<float> stack = new();

            foreach (var token in rpnTokenList)
            {
                if (token is Clause clause)
                {
                    MethodInfo evaluateMethod = clause.GetType().GetMethod("Evaluate");
                    float result = (float)evaluateMethod.Invoke(clause, null);
                    stack.Push(result);
                }
                else
                {
                    float y = stack.Pop();
                    float x = stack.Count > 0 ? stack.Pop() : 0;

                    switch (token.ToString())
                    {
                        case "AND":
                            MethodInfo andMethod = normOperator.GetType().GetMethod("Evaluate");
                            stack.Push((float)andMethod.Invoke(normOperator, new object[] { x, y }));
                            break;
                        case "OR":
                            MethodInfo orMethod = conormOperator.GetType().GetMethod("Evaluate");
                            stack.Push((float)orMethod.Invoke(conormOperator, new object[] { x, y }));
                            break;
                        case "NOT":
                            MethodInfo notMethod = notOperator.GetType().GetMethod("Evaluate");
                            stack.Push((float)notMethod.Invoke(notOperator, new object[] { y }));
                            break;
                    }
                }
            }

            return stack.Pop();
        }
    }
}
