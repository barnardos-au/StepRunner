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
            var testLogger = new TestLogger();
            var projectRunner = new ProjectRunner(testLogger);
            var projectFile = Path.Combine(AppContext.BaseDirectory, "sample1.yaml");
         
            await projectRunner.RunAsync(projectFile);

            testLogger.LogEntries.Count.Should().Be(1);
            testLogger.LogEntries.Single().Should().Be("Hello, Neil");
        }
    }
}
