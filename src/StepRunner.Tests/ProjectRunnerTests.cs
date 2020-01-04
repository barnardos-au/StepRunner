using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace StepRunner.Tests
{
    [TestFixture]
    public class ProjectRunnerTests
    {
        [Test]
        public async Task Should_Run_Project()
        {
            // Arrange
            var testLogger = new TestLogger();
            var projectRunner = new ProjectRunner(testLogger);
            var projectFile = Path.Combine(AppContext.BaseDirectory, "sample1.yaml");
         
            // Act
            await projectRunner.RunAsync(projectFile);

            // Assert
            testLogger.LogEntries.Count.Should().Be(3);
            testLogger.LogEntries.First().Should().Be("Hello, Neil");
            testLogger.LogEntries.Last().Should().Be("Forward: Hello, Neil, Backward: lieN ,olleH");
        }
    }
}
