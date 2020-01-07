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
            // dotnet app.dll -s NumberOfTestWorkers=4 --teamcity

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Start);
        }

        private static void Start(Options options)
        {
            var testEngine = TestEngineActivator.CreateInstance();

            var assembly = System.Reflection.Assembly.GetEntryAssembly();

            var testPackage = new TestPackage(assembly.Location);
            foreach (var setting in options.GetSettings())
            {
                testPackage.AddSetting(setting.Key, setting.Value);
            }

            var filterService = testEngine.Services.GetService<ITestFilterService>();
            
            var testFilterBuilder = filterService.GetTestFilterBuilder();

            var testFilter = testFilterBuilder.GetFilter();
            
            ITestEventListener testListener;
            if (options.TeamCity)
            {
                testListener = new TeamCityEventListener();
            }
            else
            {
                testListener = new ConsoleEventListener();
            }
            
            using (var testRunner = testEngine.GetRunner(testPackage))
            {
                //XmlNode result = runner.Explore(emptyFilter);
                testRunner.Run(testListener, testFilter);
            }

        }
    }
}
