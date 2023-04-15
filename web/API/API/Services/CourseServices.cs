using API.DTO;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class CourseServices : ICourse
    {
        private readonly Cosento_DBContext? _dbContext;
        private readonly IMapper? _imapper;

        public CourseServices(Cosento_DBContext? dbContext, IMapper? imapper)
        {
            _dbContext = dbContext;
            _imapper = imapper;
        }

        public async Task<bool> CheckCourseExist(string course_code)
        {
            var exists = await _dbContext!.Courses.FirstOrDefaultAsync(c => c.Course_code!.ToLower() == course_code.ToLower());
            if (exists == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> CreateCourse(Course_DTO course)
        {
            try
            {
                var exists = await CheckCourseExist(course.Course_code!);
                if (exists)
                {
                    return "Course already created";
                }

                var newCourse = _imapper!.Map<Course>(course);
                var Id = Guid.NewGuid();
                newCourse.Id = Id.ToString();
                newCourse.Created_at = DateTime.Now;
                newCourse.Updated_at = DateTime.Now;
                newCourse.SearchString = course.Course_code!.ToUpper() + " "
                    + course.Course_title!.ToUpper();

                var addCourse = await _dbContext!.Courses.AddAsync(newCourse);
                if (addCourse == null)
                {
                    throw new Exception("Something went wrong");
                }
                await _dbContext.SaveChangesAsync();
                return "Course created";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<string> DeleteCourse(string id)
        {
            try
            {
                var course = await _dbContext!.Courses.Include(l => l.lecturer_Courses).SingleOrDefaultAsync(c => c.Id == id);
                if (course == null)
                {
                    throw new Exception("Course not found");
                }
                course.lecturer_Courses!.Clear();
                _dbContext!.Remove(course);
                _dbContext.SaveChanges();

                return "Deleted successfuly";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<Course> GetCourse(string id)
        {
            try
            {
                var course = await _dbContext!.Courses.Where(c => c.Id == id)
                    .Include(lc => lc.lecturer_Courses)!.ThenInclude(l => l.Lecturer)
                    .FirstOrDefaultAsync();
                if (course == null)
                {
                    return null!;
                }
                return course;
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            try
            {
                var courses = await _dbContext!.Courses.OrderBy(c => c.Course_code)
                    .Include(lc => lc.lecturer_Courses)!.ThenInclude(l => l.Lecturer)
                    .ToListAsync();

                if (courses.Count == 0)
                {
                    return null!;
                }
                return courses;
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task<string> UpdateCourse(string id, Course_DTO editCourse)
        {
            try
            {
                var course = await _dbContext!.Courses.FindAsync(id);
                if (course == null)
                {
                    return ("Course not found");
                }
                course.Course_code = editCourse.Course_code;
                course.Course_title = editCourse.Course_title;
                course.Updated_at = DateTime.Now;
                course.Unit = editCourse.Unit;
                course.SearchString = editCourse.Course_code!.ToUpper() + " "
                    + editCourse.Course_title!.ToUpper();

                _dbContext!.Courses.Attach(course);
                await _dbContext.SaveChangesAsync();

                return "Course updated";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
