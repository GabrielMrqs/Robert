using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RobertMQ.API.Services
{
    public class MessageProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "host.docker.internal",
                UserName = "user",
                Password = "pass",
                VirtualHost = "/"
            };

            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "test",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var jsonString = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish(exchange: "",
                                 routingKey: "test",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
