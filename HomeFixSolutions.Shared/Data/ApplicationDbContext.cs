using Microsoft.EntityFrameworkCore;
using HomeFixSolutions.Shared.Models;

namespace HomeFixSolutions.Shared.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Technician> Technicians { get; set; }
    }
}