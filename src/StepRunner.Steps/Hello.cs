using System.Threading.Tasks;

namespace StepRunner
{
    public class Hello : IStep
    {
        public Task RunAsync(IExecutionContext executionContext)
        {
            executionContext.Outputs["Result"] = $"Hello, {executionContext.Inputs["Name"]}";
            
            return Task.CompletedTask;
        }
    }
}
