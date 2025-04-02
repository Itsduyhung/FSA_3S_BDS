using FSA_3S.Enum;
using System.ComponentModel.DataAnnotations;

namespace FSA_3S.Models.Requests
{
    public class RealEstateRequest
    {
        [StringLength(50, ErrorMessage = "Tên bất động sản không được vượt quá 50 ký tự.")]
        public required string RealEstateName { get; set; }

        [EnumDataType(typeof(RealEstateTypeEnum), ErrorMessage = "Loại bất động sản không hợp lệ.")]
        public RealEstateTypeEnum RealEstateType { get; set; }

        [EnumDataType(typeof(ApprovalStatusEnum), ErrorMessage = "Trạng thái phê duyệt không hợp lệ.")]
        public ApprovalStatusEnum ApprovalStatus { get; set; }

        [EnumDataType(typeof(RealEstateStatusEnum), ErrorMessage = "Trạng thái không hợp lệ.")]
        public RealEstateStatusEnum RealEstateStatus { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Giá phải là một số dương.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Người bán không được để trống.")]
        public int Seller { get; set; }

        [Required(ErrorMessage = "Ngày bán đang trống.")]
        public DateTime SaleDate { get; set; }

        public string? Coordinate { get; set; }
        public IFormFile? ImagePath { get; set; }

        [StringLength(250, ErrorMessage = "Địa chỉ không được vượt quá 250 ký tự.")]
        public string? Address { get; set; }

        [StringLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự.")]
        public string? Description { get; set; }
    }
}