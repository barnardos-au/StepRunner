using System.Threading.Tasks;

namespace StepRunner.Tests.Steps
{
    public class Hello : IStep
    {
        public Task RunAsync(IExecutionContext executionContext)
        {
            var result = $"Hello, {executionContext.Inputs["Name"]}";
            executionContext.Outputs["Result"] = result;
            
            executionContext.Logger.Log(result);
            
            return Task.CompletedTask;
        }
    }
}
