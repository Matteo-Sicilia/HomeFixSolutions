using Microsoft.AspNetCore.Mvc;
using HomeFixSolutions.Shared.Services.Interfaces;
using HomeFixSolutions.Shared.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFixSolutions.Api.Controllers;

[ApiController]
[Route("api/requests")] // Changed route
public class ServiceRequestsController : ControllerBase
{
    private readonly ILogger<ServiceRequestsController> _logger;
    private readonly IServiceRequestService _serviceRequestService;

    public ServiceRequestsController(
        ILogger<ServiceRequestsController> logger,
        IServiceRequestService serviceRequestService)
    {
        _logger = logger;
        _serviceRequestService = serviceRequestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Retrieving all service requests.");
        var serviceRequests = await _serviceRequestService.GetAllServiceRequestsAsync();

        var dtoList = serviceRequests.Select(sr => new ServiceRequestListDto
        {
            Id = sr.Id,
            Description = sr.Description,
            Address = sr.Address,
            ServiceType = sr.ServiceType,
            Status = sr.Status,
            CreatedAt = sr.CreatedAt
        }).ToList();

        return Ok(dtoList);
    }

    [HttpPut("{id}/assign")]
    public async Task<IActionResult> AssignTechnician(int id, [FromBody] AssignTechnicianRequestDto requestDto)
    {
        _logger.LogInformation("Assigning technician {TechnicianId} to request {RequestId}", requestDto.TechnicianId, id);

        var updatedRequest = await _serviceRequestService.AssignTechnicianAsync(id, requestDto.TechnicianId, requestDto.ScheduledAt);

        if (updatedRequest == null)
        {
            return NotFound(new { Message = "Service request or technician not found." });
        }

        var response = new AssignTechnicianResponseDto
        {
            Id = updatedRequest.Id,
            Message = $"Request assigned to technician {updatedRequest.TechnicianId} successfully."
        };

        return Ok(response);
    }
}
