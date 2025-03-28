using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    public class AuditEntity
    {
        [Key]
        public int AuditId { get; set; }

        public string EntityType { get; set; } = null!;
        public int? ContractId { get; set; }
        public int? RealEstateId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ContractId))]
        public ContractEntity? Contract { get; set; }

        [ForeignKey(nameof(RealEstateId))]
        public RealEstateEntity? RealEstate { get; set; }
    }
}
