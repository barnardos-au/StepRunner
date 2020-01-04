using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace NUnitTestRunner
{
    [TestFixture]
    public class TestFixture1
    {
        [Test]
        public async Task Test1A()
        {
            Console.WriteLine("Test 1A started");

            await Task.Delay(2000);

            Assert.Pass("Test 1A passed ");
            
            Console.WriteLine("Test 1A ended");
        }
        
        [Test]
        public async Task Test1B()
        {
            Console.WriteLine("Test 1B started");

            await Task.Delay(2000);

            Assert.Pass("Test 1B passed ");
            
            Console.WriteLine("Test 1B ended");
        }
        
        [Test]
        public async Task Test1C()
        {
            Console.WriteLine("Test 1C started");

            await Task.Delay(2000);

            Assert.Pass("Test 1C passed ");
            
            Console.WriteLine("Test 1C ended");
        }
        
        [Test]
        public async Task Test1D()
        {
            Console.WriteLine("Test 1D started");

            await Task.Delay(2000);

            Assert.Pass("Test 1D passed ");
            
            Console.WriteLine("Test 1D ended");
        }
        
        [Test]
        public async Task Test1E()
        {
            Console.WriteLine("Test 1E started");

            await Task.Delay(2000);

            Assert.Pass("Test 1E passed ");
            
            Console.WriteLine("Test 1E ended");
        }
    }
}
