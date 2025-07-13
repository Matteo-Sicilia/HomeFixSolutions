using HomeFixSolutions.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeFixSolutions.Shared.Services.Interfaces
{
    public interface IServiceRequestService
    {
        Task<ServiceRequest> CreateServiceRequestAsync(ServiceRequest serviceRequest);
        Task<List<ServiceRequest>> GetAllServiceRequestsAsync();
        Task<ServiceRequest?> GetServiceRequestByIdAsync(int id);
        Task<ServiceRequest?> AssignTechnicianAsync(int requestId, int technicianId, DateTime scheduledAt);
        Task<ServiceRequest?> ProcessServiceRequestFromQueueAsync(int requestId);
    }
}