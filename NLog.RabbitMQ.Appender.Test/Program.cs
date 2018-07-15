using Ariane.Common;
using Ariane.Common.Types;
using EasyNetQ;
using EasyNetQ.Topology;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLog.RabbitMQ.Appender.Test
{
    public class Service1
    {

    }
    public class Service2
    {

    }
    class Program
    {
        private static IBus bus = RabbitHutch.CreateBus("host=localhost;port=5672;virtualHost=/;username=admin;password=admin;requestedHeartbeat=0");
        static Logger s1Log = LogManager.GetLogger("Service1");
        static Logger s2Log = LogManager.GetLogger("Service2");

        static void Main(string[] args) 
        {
            var logger = LogManager.GetCurrentClassLogger(typeof(Program));

            

            //foreach (var q in Constants.Queues)
            {
                // dont create queue if not exist (passive true)
                //var queue = bus.Advanced.QueueDeclare(q, passive: true);
                //var queue = bus.Advanced.QueueDeclare("App1");
                //bus.Advanced.Bind(bus.Advanced.ExchangeDeclare("Ariane", ExchangeType.Topic), queue, "*aa");
                //bus.Advanced.Consume<LogMessageBase>(queue, (mess, info) =>
                //{
                //    var x = mess.Body;
                //    Console.WriteLine($"RABBIT MQ => Time: {x.TimeStamp}, Message: {x.Message}, Level: {x.Level}");
                //});
            }

            //bus.Subscribe<LogMessageBase>("App1", (x) => {
            //    Console.WriteLine($"RABBIT MQ Martin => Time: {x.TimeStamp}, Message: {x.Message}, Level: {x.Level}");
            //}, x => x.WithTopic("*.Martin"));
            var m1 = new List<LogMessageBase>();
            var m2 = new List<LogMessageBase>();
            var createSubscribers = true;
            if (createSubscribers)
            {
                //bus.Subscribe<LogMessageBase>("App22222", (x) =>
                //{
                //    Console.WriteLine($"RABBIT MQ 1. => Time: {x.TimeStamp}, Message: {x.Message}, Level: {x.Level}");
                //    m1.Add(x);
                //}, x => x.WithTopic("*.*.*.Test.Program"));
                //bus.Subscribe<LogMessageBase>("App1122222", (x) =>
                //{
                //    Console.WriteLine($"RABBIT MQ 2. => Time: {x.TimeStamp}, Message: {x.Message}, Level: {x.Level}");
                //    m2.Add(x);
                //}, x => x.WithTopic("#.Test.*"));

                //bus.Subscribe<LogMessageBase>("s11", (x) =>
                //{
                //    Console.WriteLine($"RABBIT MQ Service1. => Time: {x.TimeStamp}, Message: {x.Message}, Level: {x.Level}");

                //}, x => x.WithTopic("Service1"));
                //bus.Subscribe<LogMessageBase>("s22", (x) =>
                //{
                //    Console.WriteLine($"RABBIT MQ Service2. => Time: {x.TimeStamp}, Message: {x.Message}, Level: {x.Level}");

                //}, x => x.WithTopic("Service2"));

                // all
                bus.Subscribe<LogMessageBase>("all", (x) =>
                {
                    Console.WriteLine($"RABBIT MQ ALL. => Time: {x.TimeStamp}, Message: {x.Message}, Level: {x.Level}");

                }, x => { x.WithTopic("#");
                });
            }

            int xcount = 10000;
            while (xcount > 0)
            {
                logger.Trace("Trace ...");
                logger.Debug("Debug ...");
                logger.Info("Info ...");
                logger.Error("Error ...");
                logger.Fatal("Fatal ...");

                s1Log.Info("s1 - info");

                s2Log.Info("s2 - info");

                Thread.Sleep(1000);
                xcount--;
            }
            m1 = m1.OrderBy(x => x.TimeStamp).ToList();
            m2 = m2.OrderBy(x => x.TimeStamp).ToList();

            Task.Run(()=>Thread.Sleep(10000)).Wait();
            for (int i = 0; i < m2.Count; i++)
            {
                Console.WriteLine($"diff in s: { (m1[i].TimeStamp - m2[i].TimeStamp).Seconds }");
            }
            Console.WriteLine($"m1: {m1.Count}, m2: {m2.Count}");
            Console.Read();

        }
    }
}
