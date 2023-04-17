using API.DTO;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class SemesterServices : ISemester
    {
        private readonly Cosento_DBContext _dbContext;
        public SemesterServices(Cosento_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> CreateSemester(Semester_DTO semester)
        {
            try
            {
                var newSemester = new Semester();
                var Id = Guid.NewGuid();
                newSemester.Id = Id.ToString();
                newSemester.Section = semester.Section;
                newSemester.TotalCredictLoad = semester.TotalCredictLoad;
                newSemester.Created_at = DateTime.Now;
                newSemester.Updated_at = DateTime.Now;

                await _dbContext.Semesters.AddAsync(newSemester);
                _dbContext.SaveChanges();

                return "New semester created";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteSemester(string id)
        {
            try
            {
                var semester = await _dbContext.Semesters.Where(x => x.Id == id)
                    .Include(c => c.Courses)
                    .FirstOrDefaultAsync();
                if (semester == null)
                {
                    return "Not found";
                }

                semester.Courses!.Clear();
                _dbContext.Semesters.Remove(semester);
                await _dbContext.SaveChangesAsync();

                return "Semester deleted successfuly";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<Semester> GetSemester(string id)
        {
            try
            {
                var semester = await _dbContext.Semesters.Where(s => s.Id == id)
                    .Include(c => c.Courses)
                    .FirstOrDefaultAsync();
                if (semester == null)
                {
                    return null!;
                }
                return semester;
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task<IEnumerable<Semester>> GetSemesters()
        {
            try
            {
                var semesters = await _dbContext.Semesters
                    .Include(c => c.Courses)
                    .ToListAsync();
                if (semesters.Count == 0)
                {
                    return null!;
                }
                return semesters;
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task<string> UpdateSemester(string id, Semester_DTO semester)
        {
            try
            {
                var editSemester = await _dbContext.Semesters.FindAsync(id);
                if (editSemester == null)
                {
                    return null!;
                }
                editSemester.TotalCredictLoad = semester.TotalCredictLoad;
                editSemester.Section = semester.Section;
                editSemester.Updated_at = DateTime.Now;

                _dbContext.Semesters.Attach(editSemester);
                await _dbContext.SaveChangesAsync();

                return "Semester updated successfuly";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
