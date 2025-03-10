using System.ComponentModel.DataAnnotations;

namespace FSA_3S.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public String Role {  get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
}
