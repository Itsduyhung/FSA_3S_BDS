using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("contract")]
    public class ContractEntity
    {
        [Key]
        [Column("contractId")]
        public int ContractId { get; set; }

        [ForeignKey("RealEstateId")]
        public int? RealEstateId { get; set; }
        public RealEstateEntity? RealEstate { get; set; }

        [ForeignKey("CustomerId")]
        public int? CustomerId { get; set; }
        public CustomerEntity? Customer { get; set; }

        [Column("contracttype")]
        public string? ContractType { get; set; }

        [Column("contractstatus")]
        public string? ContractStatus { get; set; }

        [Column("startdate")]
        public DateOnly? StartDate { get; set; }

        [Column("enddate")]
        public DateOnly? EndDate { get; set; }

        [ForeignKey("CreatedBy")]
        public DateTime CreatedBy { get; set; }
        public UserEntity? Creator { get; set; }

        [ForeignKey("UpdatedBy")]
        public int? UpdatedBy { get; set; }
        public UserEntity? Updater { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }
}