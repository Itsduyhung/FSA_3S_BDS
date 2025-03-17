using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("mappingusernotification")]
    public class MappingUserNotificationEntity
    {
        [Key]
        [Column("mappingUserNotificationId")]
        public int MappingUserNotificationId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        [ForeignKey("NotificationId")]
        public int NotificationId { get; set; }
        public NotificationEntity Notification { get; set; } = null!;
    }
}