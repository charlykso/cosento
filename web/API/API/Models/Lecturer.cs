using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Lecturer
    {
        [MaxLength(60)]
        public string? Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^([A-Za-z-.']+)$", ErrorMessage = "format not accepted")]
        public string? Firstname { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^([A-Za-z-.']+)$", ErrorMessage = "format not accepted")]
        public string? Lastname { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? Office { get; set; } = null;

        [Required]
        [MaxLength(20)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Marital_status { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }
        public string? SearchString { get; set; }
        public string? PhoneNumber { get; set; }

        [MaxLength(10)]
        public string Role { get; set; } = "Lecturer";

        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        public List<Lecturer_Course>? Lecturer_Courses { get; set; }
    }
}
