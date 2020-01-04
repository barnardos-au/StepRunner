using System;
using System.Xml;
using NUnit.Engine;

namespace StepRunner.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            ITestEngine nunitEngine = TestEngineActivator.CreateInstance();

            var assembly = System.Reflection.Assembly.GetEntryAssembly();

            TestPackage package = new TestPackage(assembly.Location);
            package.AddSetting("WorkDirectory", @"c:\nunit\work");

            var filterService = nunitEngine.Services.GetService<ITestFilterService>();
            ITestFilterBuilder builder = filterService.GetTestFilterBuilder();

            TestFilter emptyFilter = builder.GetFilter();
            ITestEventListener testListener = new MyTestEventListener();

            using (ITestRunner runner = nunitEngine.GetRunner(package))
            {
                XmlNode result = runner.Explore(emptyFilter);
                XmlNode result1 = runner.Run(testListener, emptyFilter);
            }
            
        }
    }
    
    public class MyTestEventListener : ITestEventListener
    {
        public void OnTestEvent(string report)
        {
            Console.WriteLine(report);
        }
    }
}