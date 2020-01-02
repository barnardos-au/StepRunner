using System.Collections.Generic;

namespace StepRunner
{
    public interface IExecutionContext
    {
        string StepName { get; }
        string Description { get; }
        IReadOnlyDictionary<string, string> Inputs { get; }
        IDictionary<string, string> Outputs { get; }
        ILogger Logger { get; }
    }
}
