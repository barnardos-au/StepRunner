using System.Threading.Tasks;

namespace StepRunner
{
    public class HelloTyped : TypedStep<HelloInput, HelloOutput>
    {
        protected override async Task<HelloOutput> RunAsync(HelloInput input)
        {
            await Task.CompletedTask;
            
            return new HelloOutput
            {
                Result = $"Hello, {input.Name}"
            };
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
