using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDashboard.WPF.Models
{
    [Table("Alarms")]
    public class AlarmRecord
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Column("DateTime")]
        public DateTime DateTime { get; set; }
        
        [Required]
        [MaxLength(50)]
        [Column("Line")]
        public string Line { get; set; }
        
        [Required]
        [MaxLength(500)]
        [Column("Message")]
        public string Message { get; set; }
    }
}
