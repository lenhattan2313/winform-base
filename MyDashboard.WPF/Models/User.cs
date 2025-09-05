using System.ComponentModel.DataAnnotations;

namespace MyDashboard.WPF.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        
        [MaxLength(50)]
        public string Role { get; set; }
    }
}
