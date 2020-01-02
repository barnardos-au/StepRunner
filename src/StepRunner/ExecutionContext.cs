using System.Collections.Generic;

namespace StepRunner
{
    public class ExecutionContext : IExecutionContext
    {
        public ExecutionContext(
            ILogger logger, 
            IReadOnlyDictionary<string, object> inputs,
            string stepName,
            string description)
        {
            Logger = logger;
            Inputs = inputs;
            StepName = stepName;
            Description = description;
        }

        public ILogger Logger { get; }

        public IReadOnlyDictionary<string, object> Inputs { get; }

        public string StepName { get; }

        public string Description { get; }
        
        public IDictionary<string, object> Outputs { get; set; } = new Dictionary<string, object>();
    }
}
