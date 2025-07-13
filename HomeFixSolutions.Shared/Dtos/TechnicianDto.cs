namespace HomeFixSolutions.Shared.Dtos
{
    public class TechnicianDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}