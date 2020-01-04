using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace StepRunner.Workflow
{
    public class HelloWorld : StepBody
    {
        public Complex Input1 { get; set; }
        public int Input2 { get; set; }
        public string Output1 { get; set; }
        
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Output1 = "Hello World 1";
            Console.WriteLine(Output1);
            return ExecutionResult.Next();
        }
    }
}