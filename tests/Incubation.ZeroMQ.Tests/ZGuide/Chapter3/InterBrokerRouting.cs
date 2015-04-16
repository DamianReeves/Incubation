using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using NUnit.Framework;

namespace Incubation.ZeroMQ.Tests.ZGuide.Chapter3
{
    [TestFixture]
    public class InterBrokerRouting
    {
        [TestFixture]
        public class StateFlow
        {
            [Test,TestCaseSource(typeof(StateFlowPrototypeExamples))]
            public void StateFlowPrototype(string[] peer1Args, string[] peer2Args, string[] peer3Args)
            {
                var peer1 = Task.Factory.StartNew(() => Peering(peer1Args), TaskCreationOptions.LongRunning);
                var peer2 = Task.Factory.StartNew(() => Peering(peer2Args), TaskCreationOptions.LongRunning);
                var peer3 = Task.Factory.StartNew(() => Peering(peer3Args), TaskCreationOptions.LongRunning);
                Task.WaitAll(peer1, peer2, peer3);
            }

            private void Peering(string[] args)
            {
                var self = args[0];
                Console.WriteLine("{0}: preparing broker as {0}", self);
                using (var context = NetMQContext.Create())
                using (var backend = context.CreatePublisherSocket())
                using (var frontend = context.CreateSubscriberSocket())
                {
                    var endpoint = "tcp://127.0.0.1:" + GetPort(self);
                    Console.WriteLine("{0}-BE: binding to {1}", self, endpoint);
                    // Bind backend to endpoint
                    backend.Bind(endpoint);
                    frontend.Subscribe(string.Empty);
                    // Connect frontend to all peers
                    for (int i = 1; i < args.Length; ++i)
                    {
                        string peer = args[i];
                        Console.WriteLine("{0}-I: connecting to state backend at {1}", self, peer);
                        frontend.Connect("tcp://127.0.0.1:" + GetPort(peer));
                    }
                    var rbd = new Random();
                    using (var poller = new Poller())
                    {
                        poller.AddSocket(frontend);
                        frontend.ReceiveReady += (s, a) =>
                        {
                            
                        };
                    }
                }
            }

            private Int16 GetPort(string name)
            {
                var hash = (Int16) name[2];
                if (hash < 1024)
                {
                    hash += 1024;
                }
                return hash;
            }

            public class StateFlowPrototypeExamples:IEnumerable<TestCaseData>
            {
                public IEnumerator<TestCaseData> GetEnumerator()
                {
                    yield return new TestCaseData(new[] { "DC1", "DC2", "DC3" }, new[] { "DC2", "DC1", "DC3" }, new[] { "DC3", "DC1", "DC2" });
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }
            }
        }        
    }
}
