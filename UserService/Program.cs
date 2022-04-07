using UserService.Contexts;
using UserService.Interfaces;
using UserService.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injections
builder.Services.AddSingleton<IMessageProducer, RabbitMQProducer>();
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

// Make sure database is created
using (var context = new UserContext()) 
{
    context.Database.EnsureCreated();
}

app.Run();
