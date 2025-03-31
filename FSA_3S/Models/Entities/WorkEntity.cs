// WorkEntity.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FSA_3S.Models.Entities
{
    [Table("work")]
    public class WorkEntity
    {
        [Key]
        [Column("work_id")]
        public int WorkId { get; set; }

        [Required]
        [Column("userId")]
        public int UserId { get; set; }

        [Required]
        [Column("time_of_work")]
        public DateTime TimeOfWork { get; set; }

        [MaxLength(500)]
        [Column("des_work")]
        public string? DesWork { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public UserEntity? User { get; set; }
    }
}