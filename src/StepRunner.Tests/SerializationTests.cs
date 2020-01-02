using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using StepRunner.Models;

namespace StepRunner
{
    [TestFixture]
    public class SerializationTests
    {
        [Test]
        public async Task Should_Create_Yaml_Doc()
        {
            var project = new Project
            {
                Name = "Sample",
                Description = "Doesn't do very much at present",
                Version = "1.0",
                Variables = new Variables
                {
                    {"BaseDirectory", @"c:\temp"}
                },
                Steps = new Steps
                {
                    new Step
                    {
                        Type = "StepRunner.Hello, StepRunner.Steps",
                        Name = "First step",
                        Description = "Does something",
                        Inputs = new Variables
                        {
                            {"Name", "Barnardos"}
                        }
                    },
                    new Step
                    {
                        Type = "StepRunner.HelloTyped, StepRunner.Steps",
                        Name = "Second step",
                        Description = "Does something else",
                        Inputs = new Variables
                        {
                            {"Name", "Neil"}
                        }
                    },
                } 
            };
            
            var serializer = new YamlDotNet.Serialization.Serializer();
            var yaml = serializer.Serialize(project);

            yaml.Should().NotBeEmpty();
            
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var newProject = deserializer.Deserialize<Project>(yaml);

            newProject.Should().NotBeNull();
            newProject.Steps.Count.Should().Be(2);
        }
    }
}
