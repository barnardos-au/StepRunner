namespace StepRunner.Loggers
{
    public class TeamCityLogger : ILogger
    {
        public void Log(string message)
        {
        }
        
        public static NullLogger Create()
        {
            return new NullLogger();
        }
    }
}