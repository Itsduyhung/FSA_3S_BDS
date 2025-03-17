using System.ComponentModel.DataAnnotations;

namespace FSA_3S.Models.Requests
{
    public class ContractRequest
    {
        [Required]
        public int? RealEstateId { get; set; }

        [Required]
        public int? CustomerId { get; set; }

        [Required]
        public string? ContractType { get; set; }

        [Required]
        public string? ContractStatus { get; set; }

        [Required]
        public DateOnly? StartDate { get; set; }

        [Required]
        public DateOnly? EndDate { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
    }
}