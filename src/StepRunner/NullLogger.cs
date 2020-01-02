namespace StepRunner
{
    public class NullLogger : ILogger
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