using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("notification")]
    public class NotificationEntity
    {
        [Key]
        [Column("notificationId")]
        public int NotificationId { get; set; }

        [Column("content")]
        public required string Content { get; set; }

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Column("notificationType")]
        [StringLength(50)]
        public string? NotificationType { get; set; }

        [Column("relatedId")]
        [StringLength(50)]
        public string? RelatedId { get; set; }
    }
}