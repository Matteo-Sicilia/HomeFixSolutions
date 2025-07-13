using System;

namespace HomeFixSolutions.Shared.Dtos
{
    public class AssignTechnicianRequestDto
    {
        public int TechnicianId { get; set; }
        public DateTime ScheduledAt { get; set; }
    }
}
