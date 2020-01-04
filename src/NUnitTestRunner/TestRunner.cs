using NUnit.Engine;
using System.Xml;
using NUnit.Framework;
using System.Threading.Tasks;

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
            package.AddSetting("NumberOfTestWorkers", 2);

            var filterService = nunitEngine.Services.GetService<ITestFilterService>();
            ITestFilterBuilder builder = filterService.GetTestFilterBuilder();

            TestFilter emptyFilter = builder.GetFilter();
            //ITestEventListener testListener = new MyTestEventListener();
            var testListener = new NUnit.Engine.Listeners.TeamCityEventListener();

            using (ITestRunner runner = nunitEngine.GetRunner(package))
            {
                //XmlNode result = runner.Explore(emptyFilter);
                var result = runner.Run(testListener, emptyFilter);
                //var result1 = runner.RunAsync(testListener, emptyFilter);
                //result1.Wait(10000);
            }

        }
    }
}
