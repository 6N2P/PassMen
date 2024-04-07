using System.ComponentModel.DataAnnotations;

namespace PassMen.Beakend.Model
{
    public class Website
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IdUser { get; set; }
        [Required]
        public string Url { get; set; } = null!;
        [Required]
        public string Login { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
