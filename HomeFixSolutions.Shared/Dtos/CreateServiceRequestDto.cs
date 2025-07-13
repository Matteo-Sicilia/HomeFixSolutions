namespace HomeFixSolutions.Shared.Dtos
{
    public class CreateServiceRequestDto
    {
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
    }
}