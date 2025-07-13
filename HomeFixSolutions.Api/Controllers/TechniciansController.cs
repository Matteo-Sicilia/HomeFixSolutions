using Microsoft.AspNetCore.Mvc;
using HomeFixSolutions.Shared.Services.Interfaces;
using HomeFixSolutions.Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFixSolutions.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TechniciansController : ControllerBase
{
    private readonly ILogger<TechniciansController> _logger;
    private readonly ITechnicianService _technicianService;

    public TechniciansController(
        ILogger<TechniciansController> logger,
        ITechnicianService technicianService)
    {
        _logger = logger;
        _technicianService = technicianService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailableTechnicians()
    {
        _logger.LogInformation("Orchestrating request to retrieve all available technicians.");
        var technicians = await _technicianService.GetAvailableTechniciansAsync();
        
        var technicianDtos = technicians.Select(t => new TechnicianDto
        {
            Id = t.Id,
            Name = t.Name,
            Specialization = t.Specialization,
            IsAvailable = t.IsAvailable
        }).ToList();

        return Ok(technicianDtos);
    }
}
