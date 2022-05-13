using WorkoutService.Contexts;
using WorkoutService.Interfaces;
using WorkoutService.Repositories;
using WorkoutService.Services;
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

            builder.Services.AddSingleton<WorkoutContext>();

            builder.Services.AddSingleton<WorkoutService_>();
            builder.Services.AddSingleton<ExerciseService>();

            builder.Services.AddSingleton<IWorkoutRepository, WorkoutRepository>();
            builder.Services.AddSingleton<IExerciseRepository, ExerciseRepository>();

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

            var userContext = builder.Services.BuildServiceProvider().GetService<WorkoutContext>();
            userContext?.Database.EnsureCreated();

            app.Run();
        }
    }
}
