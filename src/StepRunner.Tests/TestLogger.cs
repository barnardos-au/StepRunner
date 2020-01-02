using System.Collections.Generic;

namespace StepRunner.Tests
{
    public class TestLogger : ILogger
    {
        public List<string> LogEntries { get; private set; } = new List<string>();

        public void Clear()
        {
            LogEntries = new List<string>();
        }
        
        public void Log(string message)
        {
            LogEntries.Add(message);
        }
    }
}