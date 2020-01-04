using System;
using FluentAssertions;
using NUnit.Framework;
using StepRunner.Tests.Samples;
using WorkflowCore.Models;

namespace StepRunner.Tests
{
    [TestFixture]
    public class NUnitTest1 : WorkflowTest<MyWorkflow, MyDataClass>
    {
        [SetUp]
        protected override void Setup()
        {
            base.Setup();
        }

        [Test]
        public void NUnit_workflow_test_sample()
        {
            var workflowId = StartWorkflow(new MyDataClass() { Value1 = 2, Value2 = 3 });
            WaitForWorkflowToComplete(workflowId, TimeSpan.FromSeconds(30));

            GetStatus(workflowId).Should().Be(WorkflowStatus.Complete);
            UnhandledStepErrors.Count.Should().Be(0);
            GetData(workflowId).Value3.Should().Be(5);
        }

    }
}