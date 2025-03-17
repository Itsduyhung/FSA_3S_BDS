using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("customer")]
    public class CustomerEntity
    {
        [Key]
        [Column("customerId")]
        public int CustomerId { get; set; }

        [Required]
        [Column("fullname")]
        [StringLength(50)]
        public required string FullName { get; set; }

        [Required]
        [Column("email")]
        [StringLength(50)]
        public required string Email { get; set; }

        [Column("phonenumber")]
        [StringLength(13)]
        public string? PhoneNumber { get; set; }

        [Column("address")]
        [StringLength(250)]
        public string? Address { get; set; }

        [Column("gender")]
        [StringLength(10)]
        public string? Gender { get; set; }

        [Column("cccd")]
        [StringLength(13)]
        public string? CCCD { get; set; }

        [Column("customertype")]
        [StringLength(10)]
        public string? CustomerType { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("createdat")]
        public DateOnly CreatedAt { get; set; }

        public List<MappingRealEstateCustomerEntity>? MappingRealEstateCustomers { get; set; }
        public List<ContractEntity>? Contracts { get; set; }
        public List<AppointmentEntity>? Appointments { get; set; }
    }
}