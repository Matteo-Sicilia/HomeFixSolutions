using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Azure.Messaging.ServiceBus;
using HomeFixSolutions.Shared.Data;
using HomeFixSolutions.Shared.Services;
using HomeFixSolutions.Shared.Services.Interfaces;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) => {
        // Add Entity Framework
        // Read the setting directly by its name
        var connectionString = context.Configuration["DefaultConnection"];
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Add custom services
        services.AddScoped<IServiceRequestService, ServiceRequestService>();
        services.AddScoped<ITechnicianService, TechnicianService>();
        
        // Add Service Bus
        services.AddSingleton<ServiceBusClient>(sp =>
        {
            // Read the setting directly by its name
            var sbConnectionString = context.Configuration["ServiceBusConnectionString"];
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            return new ServiceBusClient(sbConnectionString, clientOptions);
        });

        services.AddSingleton<ServiceBusSender>(sp =>
        {
            var client = sp.GetRequiredService<ServiceBusClient>();
            return client.CreateSender("esame"); 
        });

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();