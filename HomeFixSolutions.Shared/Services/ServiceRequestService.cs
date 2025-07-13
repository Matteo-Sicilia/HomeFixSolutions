using Microsoft.EntityFrameworkCore;
using HomeFixSolutions.Shared.Data;
using HomeFixSolutions.Shared.Models;
using HomeFixSolutions.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeFixSolutions.Shared.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly ApplicationDbContext _context;

        public ServiceRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceRequest> CreateServiceRequestAsync(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Add(serviceRequest);
            await _context.SaveChangesAsync();
            return serviceRequest;
        }

        public async Task<List<ServiceRequest>> GetAllServiceRequestsAsync()
        {
            return await _context.ServiceRequests.Include(sr => sr.Technician).ToListAsync();
        }

        public async Task<ServiceRequest?> GetServiceRequestByIdAsync(int id)
        {
            return await _context.ServiceRequests.Include(sr => sr.Technician).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ServiceRequest?> AssignTechnicianAsync(int requestId, int technicianId, DateTime scheduledAt)
        {
            var serviceRequest = await GetServiceRequestByIdAsync(requestId);
            if (serviceRequest == null)
            {
                return null;
            }

            var technician = await _context.Technicians.FirstOrDefaultAsync(t => t.Id == technicianId);
            if (technician == null)
            {
                return null;
            }

            serviceRequest.TechnicianId = technicianId;
            serviceRequest.ScheduledAt = scheduledAt;
            serviceRequest.Status = "assigned";

            await _context.SaveChangesAsync();
            return serviceRequest;
        }

        public async Task<ServiceRequest?> ProcessServiceRequestFromQueueAsync(int requestId)
        {
            var requestInDb = await _context.ServiceRequests.FindAsync(requestId);
            if (requestInDb != null && requestInDb.Status == "pending")
            {
                requestInDb.Status = "Validated"; // Simulate background processing
                await _context.SaveChangesAsync();
                return requestInDb;
            }
            return null;
        }
    }
}