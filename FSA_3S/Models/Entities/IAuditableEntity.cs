namespace FSA_3S.Models.Entities
{
    public interface IAuditableEntity
    {
        int? CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}