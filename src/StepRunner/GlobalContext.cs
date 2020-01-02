using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ServiceStack;
using StepRunner.Models;

namespace StepRunner
{
    public class GlobalContext : Dictionary<string, object>
    {
        public const string VariablePattern = @"#{([a-zA-Z0-9\[\]\s.]+)}";
        public const string StepNamePattern = @"(\[[a-zA-Z0-9\s]+\])";
        public const string ArgumentNamePattern = @"([a-zA-Z0-9\s]+)";
        public static readonly string SteppedArgumentNamePattern = $"{StepNamePattern}.{ArgumentNamePattern}";

        private readonly Project project;

        public GlobalContext(Project project)
        {
            this.project = project;
            
            foreach (var variable in project.Variables)
            {
                Add($"{nameof(Variables)}.{variable.Key}", variable.Value);
            }

            foreach (var step in project.Steps)
            {
                foreach (var variable in step.Inputs)
                {
                    Add($"[{step.Name}].{nameof(Step.Inputs)}.{variable.Key}", variable.Value);
                }
            }
        }

        public Dictionary<string, object> GetInputs(string stepName)
        {
            var step = project.Steps.SingleOrDefault(s => s.Name == stepName);
            if (step == null) throw new ArgumentException("Invalid step name", stepName);

            var inputs = new Dictionary<string, object>();
            foreach (var variable in step.Inputs)
            {
                var contextKey = $"[{step.Name}].{nameof(Step.Inputs)}.{variable.Key}";
                inputs.Add(variable.Key, ParseValue(this[contextKey]));
            }

            return inputs;
        }

        public void AddOutputs(IExecutionContext context)
        {
            foreach (var output in context.Outputs)
            {
                Add($"[{context.StepName}].{nameof(IExecutionContext.Outputs)}.{output.Key}", output.Value);
            }
        }
        
        public object GetValue(string key)
        {
            if (!ContainsKey(key))
                throw new ArgumentException($"Value with key: '{key}' not found");

            var value = this[key];

            return ParseValue(value);
        }

        private object ParseValue(object pattern)
        {
            var variableRegex = new Regex(VariablePattern, RegexOptions.IgnoreCase);
            var stringPattern = pattern.ToString();
            var variableMatch = variableRegex.Match(stringPattern);

            // No variable found, treat as literal value
            if (!variableMatch.Success)
                return pattern;

            var argumentRegex = new Regex(ArgumentNamePattern, RegexOptions.IgnoreCase);
            var steppedArgumentRegex = new Regex(SteppedArgumentNamePattern, RegexOptions.IgnoreCase);

            while (variableMatch.Success)
            {
                if (variableMatch.Groups.Count < 2)
                    throw new ArgumentException($"Unable to parse value: {variableMatch.Value} for pattern: {stringPattern}");

                // Get inner pattern/value
                var innerKey = variableMatch.Groups[1].Value;

                // Check pattern is valid. Must be Arg such as "Arg1" or Step/Arg such as "[Step 1].Arg1"
                if (!(argumentRegex.IsMatch(innerKey) || steppedArgumentRegex.IsMatch(innerKey)))
                    throw new ArgumentException($"Unable to parse pattern: {innerKey} from: {variableMatch.Value}");

                var innerValue = GetValue(innerKey);
                var stringValue = innerValue.ToString();

                // Replace entire pattern with inner value
                stringPattern = stringPattern.ReplaceFirst(variableMatch.Value, stringValue);

                variableMatch = variableMatch.NextMatch();
            }

            return pattern;
        }
    }
}
