using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ariane.Common
{
    public static class Constants
    {
        public static string[] Queues = new string []{ "Ariane.Logging.Info", "Ariane.Logging.Debug" };
        public static string RabbitMQConnectionName = "RabbitMQConnection";
    }
}
