using System.ComponentModel.DataAnnotations;

namespace MyDashboard.WPF.Models
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
