using System;
using FluentAssertions;
using NetMQ;
using NUnit.Framework;

namespace Incubation.ZeroMQ.Tests.Basics
{
    public class HelloWorld
    {
        [Test]
        public void HelloWorldExample()
        {
            Console.Title = "NetMQ HelloWorld";
            using (var context = NetMQContext.Create())
            using (var server = context.CreateResponseSocket())
            using (var client = context.CreateRequestSocket())
            {
                server.Bind("tcp://localhost:5556");
                client.Connect("tcp://localhost:5556");
                client.Send("Hello");
                var msgToServer = server.ReceiveString();
                Console.WriteLine("From Client: {0}", msgToServer);
                server.Send("Hi Back");
                var msgToClient = client.ReceiveString();
                Console.WriteLine("From Server: {0}", msgToClient);
                var messages = new[]
                {
                    msgToServer,
                    msgToClient
                };
                messages.Should().Equal("Hello", "Hi Back");
            }
        }
    }
}
