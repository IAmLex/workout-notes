using WorkoutService.Contexts;
using WorkoutService.Workers;

namespace WorkoutService
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

            // Singletons
            builder.Services.AddSingleton<UserContext>();

            // Hosted services
            builder.Services.AddHostedService<RabbitMQWorker>();

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

            // FIXME: Kan dit beter? Singleton?
            using (var context = new UserContext())
            {
                context.Database.EnsureCreated();
            }

            app.Run();
        }
    }
}
