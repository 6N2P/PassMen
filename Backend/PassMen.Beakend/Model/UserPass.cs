using System.ComponentModel.DataAnnotations;

namespace PassMen.Beakend.Model
{
    public class UserPass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
    
}
