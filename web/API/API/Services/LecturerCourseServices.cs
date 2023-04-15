using API.DTO;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class LecturerCourseServices
    {
        private readonly Cosento_DBContext? _dbContext;

        public LecturerCourseServices(Cosento_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> CreateLecturerCourse(Lecturer_Course_DTO l_course)
        {
            try
            {
                var myNewCourse = new Lecturer_Course();
                var Id = Guid.NewGuid();
                myNewCourse.Id = Id.ToString();
                myNewCourse.CourseId = l_course.CourseId;
                myNewCourse.LecturerId = l_course.LecturerId;
                myNewCourse.Created_at = DateTime.Now;
                myNewCourse.Updated_at = DateTime.Now;

                var myCourse = await _dbContext!.AddAsync(myNewCourse);
                await _dbContext.SaveChangesAsync();
                return "Course allocated";
            }
            catch (Exception ex)
            {

                return (ex.Message);
            }
        }

        public async Task<string> DeleteLecturerCourse(string id)
        {
            try
            {
                var myCourse = await _dbContext!.Lecturer_Courses.FindAsync(id);
                if (myCourse == null)
                {
                    return ($"{id} Not found");
                }
                _dbContext.Remove(myCourse);
                _dbContext.SaveChanges();
                return ("Deleted successfully");
            }
            catch (Exception ex)
            {

                return (ex.Message);
            }
        }

        public async Task<Lecturer_Course> GetLecturerCourse(string id)
        {
            try
            {
                var myCourse = await _dbContext!.Lecturer_Courses.Where(c => c.Id == id).Include(l => l.Lecturer).Include(c => c.Course).FirstOrDefaultAsync();
                if (myCourse == null)
                {
                    Console.WriteLine($"{id} not found");
                    return null!;
                }
                return myCourse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<IEnumerable<Lecturer_Course>> GetLecturerCourses()
        {
            try
            {
                var myCourse = await _dbContext!.Lecturer_Courses.Include(l => l.Lecturer).Include(c => c.Course).OrderBy(c => c.Course).ToListAsync();
                if (myCourse == null)
                {
                    return null!;
                }
                return myCourse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<string> UpdateLecturerCourse(string id, Lecturer_Course_DTO l_course)
        {
            try
            {
                var myCourse = await _dbContext!.Lecturer_Courses.FindAsync(id);
                if (myCourse == null)
                {
                    return "Not found";
                }
                myCourse.LecturerId = l_course.LecturerId;
                myCourse.CourseId = l_course.CourseId;
                myCourse.Updated_at = DateTime.Now;

                _dbContext.Lecturer_Courses.Attach(myCourse);
                _dbContext.SaveChanges();
                return "Updated successfully";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
