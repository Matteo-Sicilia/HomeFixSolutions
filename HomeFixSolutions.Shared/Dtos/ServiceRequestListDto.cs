using System;

namespace HomeFixSolutions.Shared.Dtos
{
    public class ServiceRequestListDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}