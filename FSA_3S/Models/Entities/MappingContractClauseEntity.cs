using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FSA_3S.Models.Entities
{
    [Table("mappingcontractclause")]
    public class MappingContractClauseEntity
    {
        [Key, Column("contractId", Order = 0)]
        public int ContractId { get; set; }
        public ContractEntity Contract { get; set; } = null!;

        [Key, Column("clauseId", Order = 1)]
        public int ClauseId { get; set; }
        public ClauseEntity Clause { get; set; } = null!;
    }
}