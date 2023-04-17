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

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public List<Course>? Courses { get; set; }
    }
}
