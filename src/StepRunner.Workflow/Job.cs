using WorkflowCore.Interface;

namespace StepRunner.Workflow
{
    public class Job : IWorkflow<Context>
    {
        public void Build(IWorkflowBuilder<Context> builder)
        {    
            builder
                .StartWith<HelloWorld>()
                .Input(step => step.Input1, context => context.Value4)
                .Output(context => context.Value5, step => step.Output1)
                .Then<HelloWorld2>()
                .Input(step => step.Input1, context => context.Value4)
                .Output(context => context.Value5, step => step.Output1)
                .Then<HelloWorld3>()
                .Input(step => step.Input1, context => context.Value4)
                .Output(context => context.Value5, step => step.Output1);
        }

        public string Id => "Hello World";
        public int Version => 1;
    }
}