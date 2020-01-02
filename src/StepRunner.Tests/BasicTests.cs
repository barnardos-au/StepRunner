using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;

namespace StepRunner
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public async Task Should_Return_Hello()
        {
            var helloInstance = ReflectionExtensions.CreateInstance("StepRunner.Hello, StepRunner.Steps");
            var helloStep = helloInstance as IStep;

            helloStep.Should().NotBeNull();
            
            var context = new ExecutionContext(
                NullLogger.Create(),
                new Dictionary<string, object>
                {
                    { "Name", "Barnardos" }
                },
                "Hello",
                "Hello Barnardos");

            await helloStep.RunAsync(context);
            
            var outputs = context.Outputs;

            outputs.Count.Should().Be(1);
            outputs.Keys.Should().Contain("Result");
            outputs["Result"].Should().Be("Hello, Barnardos");
        }
        
        [Test]
        public async Task Should_Return_HelloTyped()
        {
            var helloInstance = ReflectionExtensions.CreateInstance("StepRunner.HelloTyped, StepRunner.Steps");
            var helloStep = helloInstance as IStep;

            helloStep.Should().NotBeNull();
            
            var context = new ExecutionContext(
                NullLogger.Create(),
                new Dictionary<string, object>
                {
                    { "Name", "Barnardos" }
                },
                "Hello",
                "Hello Barnardos");

            await helloStep.RunAsync(context);
            
            var outputs = context.Outputs;

            outputs.Count.Should().Be(1);
            outputs.Keys.Should().Contain("Result");
            outputs["Result"].Should().Be("Hello, Barnardos");
        }
    }
}
