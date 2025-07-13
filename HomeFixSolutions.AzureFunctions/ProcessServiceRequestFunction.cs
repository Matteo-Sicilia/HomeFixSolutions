using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using HomeFixSolutions.Shared.Models;
using HomeFixSolutions.Shared.Services.Interfaces;
using System.Threading.Tasks;
using System;

namespace HomeFixSolutions.AzureFunctions;

public class ProcessServiceRequestFunction
{
    private readonly ILogger<ProcessServiceRequestFunction> _logger;
    private readonly IServiceRequestService _serviceRequestService;

    public ProcessServiceRequestFunction(ILogger<ProcessServiceRequestFunction> logger, IServiceRequestService serviceRequestService)
    {
        _logger = logger;
        _serviceRequestService = serviceRequestService;
    }

    [Function("ProcessServiceRequest")]
    public async Task Run([ServiceBusTrigger("esame", Connection = "ServiceBusConnectionString")] string message)
    {
        _logger.LogInformation($"Processing service request message: {message}");

        try
        {
            var serviceRequest = JsonSerializer.Deserialize<ServiceRequest>(message, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (serviceRequest != null)
            {
                var updatedRequest = await _serviceRequestService.ProcessServiceRequestFromQueueAsync(serviceRequest.Id);
                if(updatedRequest != null)
                {
                    _logger.LogInformation($"Service Request {updatedRequest.Id} processed successfully, status updated to {updatedRequest.Status}");
                }
                else
                {
                    _logger.LogWarning($"Service Request {serviceRequest.Id} could not be processed or was already processed.");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing service request message");
            throw; // Let Function runtime handle retry logic
        }
    }
}
