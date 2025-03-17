using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("report")]
    public class ReportEntity
    {
        [Key]
        [Column("reportId")]
        public int ReportId { get; set; }

        public int? NumberOfContracts { get; set; }
        public int? NumberOfRealEstates { get; set; }
        public int? NumberOfAppointments { get; set; }
    }
}