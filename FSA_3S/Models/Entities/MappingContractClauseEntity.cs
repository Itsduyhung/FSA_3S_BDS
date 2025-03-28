using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("mappingcontractclause")]
    [PrimaryKey(nameof(ContractId), nameof(ClauseId))]
    public class MappingContractClauseEntity
    {
        public int ContractId { get; set; }
        public ContractEntity Contract { get; set; } = null!;

        public int ClauseId { get; set; }
        public ClauseEntity Clause { get; set; } = null!;
    }
}