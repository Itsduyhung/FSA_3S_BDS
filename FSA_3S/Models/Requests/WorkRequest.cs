using FSA_3S.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FSA_3S.Models.Requests
{
    public class WorkRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime TimeOfWork { get; set; }

        [MaxLength(500)]
        public string? DesWork { get; set; }
    }
}