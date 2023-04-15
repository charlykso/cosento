using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Semester
    {
        [MaxLength(60)]
        public string? Id { get; set; }

        [MaxLength(50)]
        public string? Section { get; set; }

        public int TotalCredictLoad { get; set; }

        [MaxLength(60)]
        public string? LevelId { get; set; }
        public Level? level { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public List<Course>? Courses { get; set; }
        public List<Level_Semester>? Level_Semesters { get; set; }
    }
}
