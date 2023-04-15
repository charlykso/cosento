using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Level_Semester
    {
        [MaxLength(60)]
        public string? Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string? LevelId { get; set; }
        public Level? Level { get; set; }

        [Required]
        [MaxLength(60)]
        public string? SemesterId { get; set; }
        public Semester? Semester { get; set; }

        public DateTime? Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
