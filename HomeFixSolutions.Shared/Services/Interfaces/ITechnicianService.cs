using HomeFixSolutions.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeFixSolutions.Shared.Services.Interfaces
{
    public interface ITechnicianService
    {
        Task<List<Technician>> GetAllTechniciansAsync();
        Task<List<Technician>> GetAvailableTechniciansAsync();
        Task<Technician?> GetTechnicianByIdAsync(int id);
    }
}
