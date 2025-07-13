using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeFixSolutions.Shared.Models
{
    [Table("technicians")]
    public class Technician
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("specialization")]
        public string Specialization { get; set; } = string.Empty;

        [Column("experience_years")]
        public int? ExperienceYears { get; set; }

        [Column("is_available")]
        public bool IsAvailable { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}