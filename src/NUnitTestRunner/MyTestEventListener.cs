using NUnit.Engine;
using System;

namespace NUnitTestRunner
{
    public class MyTestEventListener : ITestEventListener
    {
        public void OnTestEvent(string report)
        {
            Console.WriteLine(report);
        }
    }
}
