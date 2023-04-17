using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Models
{
    public class Cosento_DBContext : DbContext
    {
        public Cosento_DBContext(DbContextOptions<Cosento_DBContext> options) : base(options)
        {

        }

        public DbSet<Level> Levels { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Lecturer> Lecturers { get; set; } = null!;
        public DbSet<Semester> Semesters { get; set; } = null!;
        public DbSet<Lecturer_Course> Lecturer_Courses { get; set; } = null!;
    }
}
