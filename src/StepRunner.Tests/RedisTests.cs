using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack.Messaging;
using ServiceStack.Redis;

namespace StepRunner.Tests
{
    public class Log
    {
        public string Message { get; set; }
    }

    [TestFixture]
    public class RedisTests
    {
        [Test]
        public void RedisTest()
        {
            //  docker run -p 6379:6379 -d redis
            
            var redisManager = new RedisManagerPool("localhost:6379");
            IMessageFactory redisMqFactory = new RedisMessageFactory(redisManager);
            var mqClient = redisMqFactory.CreateMessageQueueClient();

            var queueName = mqClient.GetTempQueueName();
            var msg1 = new Message<Log>(new Log {Message = "Message1"});
            mqClient.Publish(queueName, msg1);
            var msg2 = new Message<Log>(new Log {Message = "Message2"});
            mqClient.Publish(queueName, msg2);
            
            var log1 = mqClient.Get<Log>(queueName);
            log1.GetBody().Message.Should().Be("Message1");
            var log2 = mqClient.Get<Log>(queueName);
            log2.GetBody().Message.Should().Be("Message2");
            var log3 = mqClient.Get<Log>(queueName, TimeSpan.FromSeconds(1));
            log3.Should().BeNull();
            
            mqClient.Dispose();
        }
    }
}
