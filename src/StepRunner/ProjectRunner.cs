using System;
using System.Threading.Tasks;
using ServiceStack;
using StepRunner.Models;

namespace StepRunner
{
    public class ProjectRunner
    {
        private readonly ILogger logger;

        public ProjectRunner(ILogger logger)
        {
            this.logger = logger;
        }
        
        public async Task RunAsync(string projectFile)
        {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var yaml = projectFile.ReadAllText();
            var project = deserializer.Deserialize<Project>(yaml);

            var globalContext = new GlobalContext(project);

            foreach (var projectStep in project.Steps)
            {
                if (projectStep.IsDisabled) continue;
                
                var stepContext = new ExecutionContext(
                    logger, 
                    globalContext.GetInputs(projectStep.Name),
                    projectStep.Name,
                    projectStep.Description);
                
                var instance = ReflectionExtensions.CreateInstance(projectStep.Type);
                if (instance == null) throw new ArgumentException("Step Type not found", projectStep.Type);
                if (!(instance is IStep step)) throw new ArgumentException("Step Type not found", projectStep.Type);

                await step.RunAsync(stepContext);
                
                globalContext.AddOutputs(stepContext);
            }
        }
    }
}
