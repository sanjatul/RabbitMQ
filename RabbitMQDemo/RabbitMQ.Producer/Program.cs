using RabbitMQ.Client;
using RabbitMQ.Producer;
public static class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };
        using var connection=factory.CreateConnection();
        using var channel = connection.CreateModel();
        //QueueProducer.Publish(channel);
        DirectExchangePublisher.Publish(channel);
    }
}