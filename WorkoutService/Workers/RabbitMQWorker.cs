using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WorkoutService.Workers
{
    public class RabbitMQWorker : BackgroundService
    {
        private readonly ILogger<RabbitMQWorker> _logger;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "user-queue"; // TODO: rename 'user' to 'user-queue' in UserService project

        public RabbitMQWorker(ILogger<RabbitMQWorker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _factory = new ConnectionFactory() { 
                HostName = "localhost",
                DispatchConsumersAsync = true
            };

            _connection = _factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            _channel.BasicQos(0, 1, false);

            _logger.LogInformation("RabbitMQWorker started");
            System.Console.WriteLine("RabbitMQWorker started");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };

            _channel.BasicConsume(queue: "user",
                                 autoAck: true,
                                 consumer: consumer);

            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();

            _logger.LogInformation("RabbitMQWorker stopped");

            return base.StopAsync(cancellationToken);
        }
    }
}