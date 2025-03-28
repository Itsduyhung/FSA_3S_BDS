namespace FSA_3S.DTOs
{
    public class AuditDTO
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? EntityType { get; set; }
    }
}