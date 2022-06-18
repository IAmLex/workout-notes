using RabbitMQ.Client;
using UserService.Contexts;
using UserService.Interfaces;
using UserService.RabbitMQ;
using UserService.Services;

namespace UserService
{
    class Program
{
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Injections
            builder.Services.AddSingleton<IMessageProducer, RabbitMQProducer>();
            builder.Services.AddSingleton<UserService_>();
            builder.Services.AddSingleton<UserContext>();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            var userContext = builder.Services.BuildServiceProvider().GetService<UserContext>();
            userContext?.Database.EnsureCreated();

            app.Run();
        }
    }
}
