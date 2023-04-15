using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Lecturer_Course
    {
        [MaxLength(60)]
        public string? Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string? LecturerId { get; set; }
        public Lecturer? Lecturer { get; set; }

        [Required]
        [MaxLength(60)]
        public string? CourseId { get; set; }
        public Course? Course { get; set; }

        public DateTime? Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
