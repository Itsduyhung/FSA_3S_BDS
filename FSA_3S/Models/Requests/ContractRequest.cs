using FSA_3S.Enum;
using FSA_3S.Helpers;
using FSA_3S.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace FSA_3S.Models.Requests
{
    public class ContractRequest : IValidatableObject
    {
        [Required(ErrorMessage = "RealEstateId is required.")]
        public required int RealEstateId { get; set; }

        //[Required(ErrorMessage = "CustomerId is required.")]
        //[Range(1, int.MaxValue, ErrorMessage = "CustomerId must be greater than zero.")]
        //public required int CustomerId { get; set; }

        //[Required(ErrorMessage = "SellerId is required.")]
        //[Range(1, int.MaxValue, ErrorMessage = "SellerId must be greater than zero.")]
        //public required int SellerId { get; set; }

        [Required(ErrorMessage = "ContractType is required.")]
        public ContractTypeEnum ContractType { get; set; }

        [Required(ErrorMessage = "ContractStatus is required.")]
        public ContractStatusEnum ContractStatus { get; set; }

        [Required(ErrorMessage = "PaymentStatus is required.")]
        public StatusPaymentEnum StatusPayment { get; set; }

        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required.")]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "EndDate must be greater than StartDate.")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "At least one clause is required.")]
        public required List<int> ClauseIds { get; set; }

        [Required(ErrorMessage = "Buyer information is required.")]
        public required PersonInfo Buyer { get; set; }

        [Required(ErrorMessage = "Seller information is required.")]
        public required PersonInfo Seller { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Buyer == Seller)
            {
                yield return new ValidationResult(
                    "CustomerId and SellerId cannot be the same.",
                    [nameof(Buyer), nameof(Seller)]
                );
            }
        }
    }
}