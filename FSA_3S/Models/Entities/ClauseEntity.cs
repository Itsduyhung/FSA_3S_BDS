using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("clause")]
    public class ClauseEntity
    {
        [Key]
        [Column("clauseId")]
        public int ClauseId { get; set; }

        [ForeignKey("ContractId")]
        public int ContractId { get; set; }
        public ContractEntity Contract { get; set; } = null!;

        [Column("clauseNumber")]
        public int ClauseNumber { get; set; }

        [Column("clauseContent")]
        public string? ClauseContent { get; set; }

        [Column("clauseType")]
        public string? ClauseType { get; set; }
    }
}