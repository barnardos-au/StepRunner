using System;
using NUnit.Engine;
using System.Xml;
using NUnit.Framework;
using System.Threading.Tasks;
using NUnit.Engine.Listeners;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace NUnitTestRunner
{
    class TestRunner
    {
        public static void Run()
        {
            ITestEngine nunitEngine = TestEngineActivator.CreateInstance();

            var assembly = System.Reflection.Assembly.GetEntryAssembly();

            TestPackage package = new TestPackage(assembly.Location);
            package.AddSetting("NumberOfTestWorkers", 4);

            var filterService = nunitEngine.Services.GetService<ITestFilterService>();
            ITestFilterBuilder builder = filterService.GetTestFilterBuilder();

            TestFilter emptyFilter = builder.GetFilter();
            //ITestEventListener testListener = new MyTestEventListener();
            var testListener = new TeamCityEventListener();

            using (ITestRunner runner = nunitEngine.GetRunner(package))
            {
                //XmlNode result = runner.Explore(emptyFilter);
                var result = runner.Run(testListener, emptyFilter);
            }

        }
    }
}
