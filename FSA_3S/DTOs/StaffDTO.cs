using FSA_3S.Enum;

namespace FSA_3S.DTOs
{
    public class StaffDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string CCCD { get; set; }
        public string Role { get; set; }
        public DateTime CreateDate { get; set; }
        public UserStatusEnum Status { get; set; }
    }
}