using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using UserService.Interfaces;

namespace UserService.RabbitMQ
{
    class RabbitMQProducer : IMessageProducer
    {
        private readonly ILogger<RabbitMQProducer> _logger;
        private readonly IConfiguration _configuration;

        public RabbitMQProducer(ILogger<RabbitMQProducer> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void SendMessage<T>(T message, string routingKey)
        {
            _logger.LogInformation($"Sending message {message} with routing key {routingKey}");

            ConnectionFactory factory = new ConnectionFactory() { 
                HostName = _configuration.GetSection("RabbitMQ:Hostname").Value,
                UserName = _configuration.GetSection("RabbitMQ:Username").Value,
                Password = _configuration.GetSection("RabbitMQ:Password").Value,
            };

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "user-exchange", 
                                        type: ExchangeType.Topic,
                                        durable: true);

                string messageBody = JsonConvert.SerializeObject(message);
                byte[] body = Encoding.UTF8.GetBytes(messageBody);

                channel.BasicPublish(exchange: "user-exchange",
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}