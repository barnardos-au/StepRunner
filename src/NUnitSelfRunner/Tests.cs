﻿using System;
using System.IO;
using CommandLine;
using NUnit.Engine;
using NUnit.Engine.Listeners;
using NUnitSelfRunner.Listeners;
using StackExchange.Redis;

namespace NUnitSelfRunner
{
    public class Tests
    {
        private TextWriter textWriter;

        public Tests(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }
        
        public static void Run(string[] args, TextWriter textWriter = null)
        {
            // dotnet app.dll -s NumberOfTestWorkers=4 -t -r localhost:6379
            
            var tests = new Tests(textWriter ?? Console.Out);

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(tests.Start);
        }

        private void Start(Options options)
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            var testPackage = new TestPackage(assembly.Location);
            foreach (var setting in options.GetSettings())
            {
                testPackage.AddSetting(setting.Key, setting.Value);
            }
            
            var testEngine = TestEngineActivator.CreateInstance();
            var filterService = testEngine.Services.GetService<ITestFilterService>();
            var testFilterBuilder = filterService.GetTestFilterBuilder();
            if (!string.IsNullOrEmpty(options.Filter))
            {
                testFilterBuilder.SelectWhere(options.Filter);
            }
            var testFilter = testFilterBuilder.GetFilter();

            using (var testRunner = testEngine.GetRunner(testPackage))
            {
                var testEventListener = GetEventTestListener(options);
                
                if (options.Explore)
                {
                    var result = testRunner.Explore(testFilter);
                    testEventListener.OnTestEvent(result.OuterXml);
                }
                else
                {
                    testRunner.Run(testEventListener, testFilter);
                }
            }
        }

        private ITestEventListener GetEventTestListener(Options options)
        {
            if (!string.IsNullOrEmpty(options.RedisHost))
            {
                var redis = ConnectionMultiplexer.Connect(options.RedisHost);
                var subscriber = redis.GetSubscriber();

                textWriter = new RedisQueueWriter(subscriber, options.QueueName);
            }

            ITestEventListener testEventListener = new DefaultEventListener(textWriter);
            if (options.Explore)
            {
                return testEventListener;
            }

            if (options.Console)
            {
                return new ConsoleEventListener(textWriter);
            }
            
            if (options.TeamCity)
            {
                return new TeamCityEventListener(textWriter, new TeamCityInfo());
            }

            return testEventListener;
        }
    }
}
