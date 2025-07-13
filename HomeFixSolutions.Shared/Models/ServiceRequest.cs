using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeFixSolutions.Shared.Models
{
    [Table("service_requests")]
    public class ServiceRequest
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("address")]
        public string Address { get; set; } = string.Empty;

        [Column("service_type")]
        public string ServiceType { get; set; } = string.Empty;

        [Column("status")]
        public string Status { get; set; } = string.Empty;

        [Column("scheduled_at")]
        public DateTime? ScheduledAt { get; set; }

        [Column("technician_id")]
        public int? TechnicianId { get; set; }

        [ForeignKey("TechnicianId")]
        public Technician? Technician { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}