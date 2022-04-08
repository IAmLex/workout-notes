using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WorkoutService.Contexts;
using WorkoutService.Models;

namespace WorkoutService.Workers
{
    public class RabbitMQWorker : BackgroundService
    {
        private readonly ILogger<RabbitMQWorker> _logger;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "user";
        private readonly UserContext _userContext;

        public RabbitMQWorker(ILogger<RabbitMQWorker> logger, UserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _factory = new ConnectionFactory()
            {
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

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (model, eventArgs) =>
            {
                _logger.LogInformation("Message received");

                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"Received message: {message}");

                var user = JsonConvert.DeserializeObject<User>(message);

                if (user != null)
                {
                    _userContext.Users.Add(user);
                    _userContext.SaveChanges();

                    _logger.LogInformation($"User {user.Username} added to database");
                }
                else
                {
                    _logger.LogError("User is null");
                }

                await Task.Delay(1000);
            };

            _channel.BasicConsume(queue: QueueName,
                                  autoAck: true,
                                  consumer: consumer);

            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();

            _logger.LogInformation("RabbitMQWorker stopped");

            return base.StopAsync(cancellationToken);
        }
    }
}