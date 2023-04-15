using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Course
    {
        [MaxLength(60)]
        public string? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Course_code { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Course_title { get; set; }

        [MaxLength(60)]
        public string? LevelId { get; set; }
        public Level? level { get; set; }

        public int Unit { get; set; }
        public string? SearchString { get; set; }

        [MaxLength(60)]
        public string? SemesterId { get; set; }
        public Semester? Semester { get; set; }

        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public List<Lecturer_Course>? lecturer_Courses { get; set; }
    }
}
