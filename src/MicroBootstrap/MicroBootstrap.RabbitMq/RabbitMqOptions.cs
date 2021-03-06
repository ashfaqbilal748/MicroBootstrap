using System.Collections.Generic;
using RawRabbit.Configuration;

namespace MicroBootstrap.RabbitMq
{
    public class RabbitMqOptions : RawRabbitConfiguration
    {
        public int Retries { get; set; }
        public int RetryInterval { get; set; }
        public MessageProcessorOptions MessageProcessor { get; set; }
        public IEnumerable<string> HostNames { get; set; }
        public string ConventionsCasing { get; set; }
        public new QueueOptions Queue { get; set; }
        public new ExchangeOptions Exchange { get; set; }
        public class MessageProcessorOptions
        {
            public bool Enabled { get; set; }
            public string Type { get; set; }
            public int MessageExpirySeconds { get; set; }
        }

        public class QueueOptions : GeneralQueueConfiguration
        {
            public string Template { get; set; }
            public bool Declare { get; set; }
        }

        public class ExchangeOptions : GeneralExchangeConfiguration
        {
            public string Name { get; set; }
            public bool Declare { get; set; }
        }

    }
}