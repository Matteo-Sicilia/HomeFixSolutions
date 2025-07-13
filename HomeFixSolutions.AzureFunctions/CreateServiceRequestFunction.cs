using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Azure.Messaging.ServiceBus;
using HomeFixSolutions.Shared.Dtos;
using HomeFixSolutions.Shared.Models;
using HomeFixSolutions.Shared.Services.Interfaces;

namespace HomeFixSolutions.AzureFunctions
{
    public class CreateServiceRequestFunction
    {
        private readonly ILogger<CreateServiceRequestFunction> _logger;
        private readonly IServiceRequestService _serviceRequestService;
        private readonly ServiceBusSender _sender;

        public CreateServiceRequestFunction(
            ILogger<CreateServiceRequestFunction> logger,
            IServiceRequestService serviceRequestService,
            ServiceBusSender sender)
        {
            _logger = logger;
            _serviceRequestService = serviceRequestService;
            _sender = sender;
        }

        [Function("CreateServiceRequest")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "requests")] HttpRequestData req)
        {
            _logger.LogInformation("HTTP trigger function processed a request to create a service request.");

            string? requestBody = await req.ReadAsStringAsync();
            
            if (string.IsNullOrEmpty(requestBody))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            var requestDto = JsonSerializer.Deserialize<CreateServiceRequestDto>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (requestDto == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            var serviceRequest = new ServiceRequest
            {
                Description = requestDto.Description,
                Address = requestDto.Address,
                ServiceType = requestDto.ServiceType
            };

            var createdRequest = await _serviceRequestService.CreateServiceRequestAsync(serviceRequest);

            // Send message to Service Bus for background processing
            var messageBody = JsonSerializer.Serialize(createdRequest);
            var message = new ServiceBusMessage(messageBody);
            await _sender.SendMessageAsync(message);

            var response = req.CreateResponse(HttpStatusCode.Created);
            var responseDto = new CreateServiceRequestResponseDto
            {
                Id = createdRequest.Id,
                Message = "Service request created successfully."
            };
            await response.WriteAsJsonAsync(responseDto);

            return response;
        }
    }
}