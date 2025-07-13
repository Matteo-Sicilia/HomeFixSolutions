using Microsoft.EntityFrameworkCore;
using Azure.Messaging.ServiceBus;
using HomeFixSolutions.Shared.Data;
using HomeFixSolutions.Shared.Services;
using HomeFixSolutions.Shared.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add custom services
builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();
builder.Services.AddScoped<ITechnicianService, TechnicianService>();


// Add Service Bus
builder.Services.AddSingleton<ServiceBusClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("ServiceBusConnectionString");
    var clientOptions = new ServiceBusClientOptions()
    {
        TransportType = ServiceBusTransportType.AmqpWebSockets
    };
    return new ServiceBusClient(connectionString, clientOptions);
});

builder.Services.AddSingleton<ServiceBusSender>(sp =>
{
    var client = sp.GetRequiredService<ServiceBusClient>();
    return client.CreateSender("esame");
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
