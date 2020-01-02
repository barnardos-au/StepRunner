using System.Threading.Tasks;

namespace StepRunner.Tests.Steps
{
    public class HelloTyped : TypedStep<HelloInput, HelloOutput>
    {
        protected override async Task<HelloOutput> RunAsync(HelloInput input)
        {
            await Task.CompletedTask;
            
            var result = new HelloOutput
            {
                Result = $"Hello, {input.Name}"
            };
            
            Context.Logger.Log(result.Result);

            return result;
        }
    }
    
    public class HelloInput
    {
        public string Name { get; set; }
    }

    public class HelloOutput
    {
        public string Result { get; set; }
    }
}
