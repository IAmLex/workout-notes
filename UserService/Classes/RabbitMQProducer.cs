using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using UserService.Interfaces;

namespace UserService.RabbitMQ
{
    class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "user",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string messageBody = JsonConvert.SerializeObject(message);
                byte[] body = Encoding.UTF8.GetBytes(messageBody);

                channel.BasicPublish(exchange: "",
                                     routingKey: "user",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}