using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("payment")]
    public class PaymentEntity
    {
        [Key]
        [Column("paymentId")]
        public int PaymentId { get; set; }

        [ForeignKey("ContractId")]
        public int ContractId { get; set; }
        public ContractEntity Contract { get; set; } = null!;

        [Column("amount")]
        public float Amount { get; set; }

        [Column("paymentDate")]
        public DateTime PaymentDate { get; set; }

        [Column("paymentStatus")]
        [StringLength(20)]
        public string? PaymentStatus { get; set; }
    }
}