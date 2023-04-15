using API.Models;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class Level_Semester_DTO
    {
        [Required]
        [MaxLength(60)]
        public string? LevelId { get; set; }

        [Required]
        [MaxLength(60)]
        public string? SemesterId { get; set; }
    }
}
