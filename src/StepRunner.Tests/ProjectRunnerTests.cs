using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StepRunner
{
    [TestFixture]
    public class ProjectRunnerTests
    {
        [Test]
        public async Task Should_Run_Project()
        {
            var projectFile = Path.Combine(AppContext.BaseDirectory, "sample1.yaml");
            var projectRunner = new ProjectRunner();
            await projectRunner.RunAsync(projectFile);
        }
    }
}
