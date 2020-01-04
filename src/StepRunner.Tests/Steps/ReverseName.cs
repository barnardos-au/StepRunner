using System;
using System.Threading.Tasks;

namespace StepRunner.Tests.Steps
{
    public class ReverseName : TypedStep<ReverseNameInput, ReverseNameOutput>
    {
        protected override async Task<ReverseNameOutput> RunAsync(ReverseNameInput input)
        {
            await Task.CompletedTask;
            
            var charArray = input.Name.ToCharArray();
            Array.Reverse( charArray );
            
            var result = new ReverseNameOutput
            {
                Result = new string(charArray)
            };
            
            Context.Logger.Log(result.Result);

            return result;
        }
    }
    
    public class ReverseNameInput
    {
        public string Name { get; set; }
    }

    public class ReverseNameOutput
    {
        public string Result { get; set; }
    }
}
