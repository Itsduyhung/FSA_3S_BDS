using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("mappingrealestatecustomer")]
    public class MappingRealEstateCustomerEntity
    {
        [Key]
        [Column("mappingRealEstateCustomerId")]
        public int MappingRealEstateCustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;

        [ForeignKey("PropertyId")]
        public int PropertyId { get; set; }
        public RealEstateEntity Property { get; set; } = null!;

        [Column("likes")]
        [StringLength(5)]
        public string? Likes { get; set; }

        [Column("comment")]
        public string? Comment { get; set; }
    }
}