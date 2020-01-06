using CommandLine;
using NUnit.Engine;
using NUnit.Engine.Listeners;
using NUnitSelfRunner.Listeners;

namespace NUnitSelfRunner
{
    public static class Tests
    {
        public static void Run(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Start);
        }

        private static void Start(Options options)
        {
            var testEngine = TestEngineActivator.CreateInstance();

            var assembly = System.Reflection.Assembly.GetEntryAssembly();

            var testPackage = new TestPackage(assembly.Location);
            testPackage.AddSetting("NumberOfTestWorkers", 4);

            var filterService = testEngine.Services.GetService<ITestFilterService>();
            
            var testFilterBuilder = filterService.GetTestFilterBuilder();

            var testFilter = testFilterBuilder.GetFilter();
            //var testListener = new ConsoleEventListener();
            var testListener = new TeamCityEventListener();

            using (var testRunner = testEngine.GetRunner(testPackage))
            {
                //XmlNode result = runner.Explore(emptyFilter);
                testRunner.Run(testListener, testFilter);
            }

        }
    }
}
