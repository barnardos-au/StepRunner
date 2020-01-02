using System.Threading.Tasks;
using ServiceStack;

namespace StepRunner
{
    public abstract class TypedStep<TInput, TOutput> : IStep
    {
        protected abstract Task<TOutput> RunAsync(TInput input);

        protected IExecutionContext Context;
        
        public async Task RunAsync(IExecutionContext executionContext)
        {
            Context = executionContext;
            
            var input = executionContext.Inputs.FromObjectDictionary<TInput>();

            var output = await RunAsync(input);

            executionContext.Outputs = output.ToObjectDictionary();
        }
    }
}