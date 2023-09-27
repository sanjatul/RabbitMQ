﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class FanoutExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl",30000 }
            };
            channel.ExchangeDeclare("demo-fanout-exchange",
                      ExchangeType.Fanout, arguments: ttl);
            var count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! count {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                var propertises=channel.CreateBasicProperties();
                propertises.Headers = new Dictionary<string, object>
                {
                    { "account","new"}
                };
                channel.BasicPublish("demo-fanout-exchange","account.init", propertises, body);
                count++;
                Thread.Sleep(2000);
            }
        }
    }
}
