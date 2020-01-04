﻿using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace NUnitTestRunner
{
    [TestFixture]
    public class TestFixture4
    {
        [Test]
        public async Task Test4A()
        {
            Console.WriteLine("Test 4A started");

            await Task.Delay(2000);

            Assert.Pass("Test 4A passed ");
            
            Console.WriteLine("Test 4A ended");
        }
        
        [Test]
        public async Task Test4B()
        {
            Console.WriteLine("Test 4B started");

            await Task.Delay(2000);

            Assert.Pass("Test 4B passed ");
            
            Console.WriteLine("Test 4B ended");
        }
        
        [Test]
        public async Task Test4C()
        {
            Console.WriteLine("Test 4C started");

            await Task.Delay(2000);

            Assert.Pass("Test 4C passed ");
            
            Console.WriteLine("Test 4C ended");
        }
        
        [Test]
        public async Task Test4D()
        {
            Console.WriteLine("Test 4D started");

            await Task.Delay(2000);

            Assert.Pass("Test 4D passed ");
            
            Console.WriteLine("Test 4D ended");
        }
        
        [Test]
        public async Task Test4E()
        {
            Console.WriteLine("Test 4E started");

            await Task.Delay(2000);

            Assert.Pass("Test 4E passed ");
            
            Console.WriteLine("Test 4E ended");
        }
    }
}
