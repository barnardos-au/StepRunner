using System.Threading.Tasks;

namespace StepRunner.Tests.Steps
{
    public class PassThrough : IStep
    {
        public Task RunAsync(IExecutionContext executionContext)
        {
            var value = executionContext.Inputs["Value"];
            executionContext.Outputs["Value"] = value;
            
            executionContext.Logger.Log(value.ToString());
            
            return Task.CompletedTask;
        }
    }
}
