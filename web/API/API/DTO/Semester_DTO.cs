using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class Semester_DTO
    {
        [MaxLength(50)]
        public string? Section { get; set; }

        public int TotalCredictLoad { get; set; }

        [MaxLength(60)]
        public string? LevelId { get; set; }
    }
}
