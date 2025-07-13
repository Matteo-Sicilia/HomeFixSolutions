using Microsoft.EntityFrameworkCore;
using HomeFixSolutions.Shared.Data;
using HomeFixSolutions.Shared.Models;
using HomeFixSolutions.Shared.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFixSolutions.Shared.Services
{
    public class TechnicianService : ITechnicianService
    {
        private readonly ApplicationDbContext _context;

        public TechnicianService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Technician>> GetAllTechniciansAsync()
        {
            return await _context.Technicians.ToListAsync();
        }

        public async Task<List<Technician>> GetAvailableTechniciansAsync()
        {
            return await _context.Technicians.Where(t => t.IsAvailable).ToListAsync();
        }

        public async Task<Technician?> GetTechnicianByIdAsync(int id)
        {
            return await _context.Technicians.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
