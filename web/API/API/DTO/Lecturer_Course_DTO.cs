using API.Models;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class Lecturer_Course_DTO
    {
        [Required]
        [MaxLength(60)]
        public string? LecturerId { get; set; }

        [Required]
        [MaxLength(60)]
        public string? CourseId { get; set; }
    }
}
