using System;
using FluentAssertions;
using NUnit.Framework;
using StepRunner.Workflow;
using WorkflowCore.Models;

namespace StepRunner.Tests
{
    [TestFixture]
    public class WorkflowTests : WorkflowTest<Job, Context>
    {
        [SetUp]
        protected override void Setup()
        {
            base.Setup();
        }

        [Test]
        public void NUnit_workflow_test_sample()
        {
            var workflowId = StartWorkflow(new Context { Value1 = 2, Value2 = 3 });
            WaitForWorkflowToComplete(workflowId, TimeSpan.FromSeconds(30));

            var status = GetStatus(workflowId);
            status.Should().Be(WorkflowStatus.Complete);
            UnhandledStepErrors.Count.Should().Be(0);
            var data = GetData(workflowId);
            data.Value5.Should().Be("Hello World 3");
        }

    }
}