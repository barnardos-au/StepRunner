using System.Collections.Generic;

namespace StepRunner
{
    public class ExecutionContext : IExecutionContext
    {
        public ExecutionContext(
            ILogger logger, 
            IReadOnlyDictionary<string, string> inputs,
            string stepName,
            string description)
        {
            Logger = logger;
            Inputs = inputs;
            StepName = stepName;
            Description = description;
        }

        public ILogger Logger { get; }

        public IReadOnlyDictionary<string, string> Inputs { get; }

        public string StepName { get; }

        public string Description { get; }
        
        public IDictionary<string, string> Outputs { get; } = new Dictionary<string, string>();
    }
}
