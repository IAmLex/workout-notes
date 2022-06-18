using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WorkoutService.Contexts;
using WorkoutService.Models;
using WorkoutService.Types;

namespace WorkoutService.Workers
{
    public class RabbitMQWorker : BackgroundService
    {
        private readonly ILogger<RabbitMQWorker> _logger;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private readonly WorkoutContext _workoutContext;
        private readonly IConfiguration _configuration;

        public RabbitMQWorker(ILogger<RabbitMQWorker> logger, WorkoutContext workoutContext, IConfiguration configuration)
        {
            _logger = logger;
            _workoutContext = workoutContext;
            _configuration = configuration;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _factory = new ConnectionFactory()
            {
                HostName = _configuration.GetSection("RabbitMQ:Hostname").Value,
                UserName = _configuration.GetSection("RabbitMQ:Username").Value,
                Password = _configuration.GetSection("RabbitMQ:Password").Value,
                DispatchConsumersAsync = true
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "user-exchange",
                                     type: ExchangeType.Topic,
                                     durable: true);

            _channel.QueueDeclare(queue: "user-queue", 
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            var routingKeys = new[]
            {
                RoutingKeyType.UserCreated,
                RoutingKeyType.UserUpdated,
                RoutingKeyType.UserDeleted
            };
            foreach (var routingKey in routingKeys)
            {
                _channel.QueueBind(queue: "user-queue",
                                   exchange: "user-exchange",
                                   routingKey: routingKey);
            }

            _channel.BasicQos(prefetchSize: 0,
                              prefetchCount: 1,
                              global: false);

            _logger.LogInformation("RabbitMQWorker started");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"Received message: {message} with routingkey: {eventArgs.RoutingKey}");

                var user = JsonConvert.DeserializeObject<User>(message);
                if (user != null)
                {
                    switch (eventArgs.RoutingKey)
                    {
                        case RoutingKeyType.UserCreated:
                            _workoutContext.Users.Add(user);
                            _workoutContext.SaveChanges();

                            _logger.LogInformation($"User {user.Username} added to database");

                            break;
                        case RoutingKeyType.UserUpdated:
                            User? userToUpdate = _workoutContext.Users.Find(user.Id);
                            if (userToUpdate == null)
                            {
                                // add user
                                _workoutContext.Users.Add(user);

                                _logger.LogInformation($"User {user.Username} with id {user.Id} didn't exist in database, added to database");
                            }
                            else
                            {
                                // update user
                                userToUpdate.Username = user.Username;
                                userToUpdate.FirstName = user.FirstName;
                                userToUpdate.LastName = user.LastName;

                                _logger.LogInformation($"User {user.Username} updated in database");
                            }

                            _workoutContext.SaveChanges();

                            break;
                        case RoutingKeyType.UserDeleted:
                            User? userToDelete = _workoutContext.Users.Find(user.Id);

                            if (userToDelete != null)
                            {
                                _workoutContext.Users.Remove(userToDelete);
                                _workoutContext.SaveChanges();

                                _logger.LogInformation($"User {user.Username} deleted from database");
                            }
                            else
                            {
                                _logger.LogInformation($"User {user.Username} with id {user.Id} doesn't exist in database, nothing to delete");
                            }

                            break;
                    }
                }

                await Task.Delay(1000);
            };

            _channel.BasicConsume(queue: "user-queue",
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