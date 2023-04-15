using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class Lecturer_DTO
    {
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

        public string? PhoneNumber { get; set; }

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

        [MaxLength(10)]
        public string Role { get; set; } = "Lecturer";
    }
}
