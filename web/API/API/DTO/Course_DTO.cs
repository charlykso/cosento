using API.Models;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class Course_DTO
    {
        [Required]
        [MaxLength(50)]
        public string? Course_code { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Course_title { get; set; }

        [MaxLength(60)]
        public string? LevelId { get; set; }

        public int Unit { get; set; }
        public string? SearchString { get; set; }

        [MaxLength(60)]
        public string? SemesterId { get; set; }
    }
}
