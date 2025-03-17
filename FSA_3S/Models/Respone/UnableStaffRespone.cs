using FSA_3S.Enum;

public class UnableStaffResponse
{
    public bool Success { get; set; }
    public UserStatusEnum? Status { get; set; }

    // Constructor không tham số
    public UnableStaffResponse() { }

    // Constructor chỉ nhận success
    public UnableStaffResponse(bool success)
    {
        Success = success;
    }

    public UnableStaffResponse(bool success, UserStatusEnum? newStatus)
    {
        Success = success;
        Status = newStatus;
    }
}