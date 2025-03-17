using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("realestate")]
    public class RealEstateEntity
    {
        [Key]
        [Column("realEstateId")]
        public int RealEstateId { get; set; }

        [Required]
        [Column("name")]
        [StringLength(50)]
        public required string Name { get; set; }

        [Column("type")]
        [StringLength(10)]
        public string? Type { get; set; }

        [Column("status")]
        [StringLength(10)]
        public string? Status { get; set; }

        [Column("price")]
        public float Price { get; set; }

        [Column("bedrooms")]
        public int? Bedrooms { get; set; }

        [Column("bathrooms")]
        public int? Bathrooms { get; set; }

        [Column("image_path")]
        [StringLength(255)]
        public string? ImagePath { get; set; }

        [Column("address")]
        [StringLength(250)]
        public string? Address { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [ForeignKey("CreatedBy")]
        public DateTime CreatedBy { get; set; }
        public UserEntity? Creator { get; set; }

        [ForeignKey("UpdatedBy")]
        public int? UpdatedBy { get; set; }
        public UserEntity? Updater { get; set; }

        [Column("createdat")]
        public DateOnly? CreatedAt { get; set; }

        [Column("updatedat")]
        public DateOnly? UpdatedAt { get; set; }

        public List<MappingRealEstateCustomerEntity>? MappingRealEstateCustomers { get; set; }
        public List<ContractEntity>? Contracts { get; set; }
    }
}