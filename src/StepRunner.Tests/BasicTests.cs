using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using StepRunner.Loggers;

namespace StepRunner.Tests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public async Task Should_Return_Hello()
        {
            // Act
            var helloInstance = ReflectionExtensions.CreateInstance("StepRunner.Tests.Steps.Hello, StepRunner.Steps");
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

            // Assert
            outputs.Count.Should().Be(1);
            outputs.Keys.Should().Contain("Result");
            outputs["Result"].Should().Be("Hello, Barnardos");
        }
        
        [Test]
        public async Task Should_Return_HelloTyped()
        {
            // Act
            var helloInstance = ReflectionExtensions.CreateInstance("StepRunner.Tests.Steps.HelloTyped, StepRunner.Steps");
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

            // Assert
            outputs.Count.Should().Be(1);
            outputs.Keys.Should().Contain("Result");
            outputs["Result"].Should().Be("Hello, Barnardos");
        }
    }
}
