using System.ComponentModel.DataAnnotations;

namespace PassMen.Beakend.Model
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IdUser { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
