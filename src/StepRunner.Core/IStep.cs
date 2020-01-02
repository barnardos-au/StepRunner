using System.Threading.Tasks;

namespace StepRunner
{
    public interface IStep
    {
        Task RunAsync(IExecutionContext executionContext);
    }
}
