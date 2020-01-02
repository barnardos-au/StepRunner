using System.Collections.Generic;

namespace StepRunner
{
    public interface IExecutionContext
    {
        string StepName { get; }
        string Description { get; }
        IReadOnlyDictionary<string, object> Inputs { get; }
        IDictionary<string, object> Outputs { get; set; }
        ILogger Logger { get; }
    }
}
