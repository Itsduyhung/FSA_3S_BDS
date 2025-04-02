using FSA_3S.Enum;

public class MappingUserAppointmentResponse
{
    public int MappingUserAppointmentId { get; set; }
    public int UserId { get; set; }
    public string? FullName { get; set; } // Tên người dùng
    public int AppointmentId { get; set; }
    public string? AppointmentTitle { get; set; } // Tiêu đề của cuộc hẹn
    public string? CustomerName { get; set; } // Tên của khách hàng
    //public string? Title { get; set; }
    public ApprovalStatusEnum ApprovalStatus { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public int CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}