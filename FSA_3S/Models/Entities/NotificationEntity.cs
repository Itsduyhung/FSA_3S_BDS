using FSA_3S.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models
{
    [Table("Notifications")] // Định nghĩa tên bảng trong DB
    public class NotificationEntity
    {
        [Key]
        [Column("IdNotification")]
        public int NotificationId { get; set; }

        //[Required]
        [ForeignKey("Sender")]
        [Column("SenderId")]
        public int? SenderId { get; set; }
        public virtual UserEntity? Sender { get; set; }

        [Required]
        [ForeignKey("Receiver")]
        [Column("ReceiverId")]
        public int? ReceiverId { get; set; }
        public virtual UserEntity? Receiver { get; set; }  // Reference đến bảng User

        [Required]
        [Column("Title")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [Column("Message")]
        public string Message { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("IsRead")]
        public bool IsRead { get; set; } = false;
    }
}