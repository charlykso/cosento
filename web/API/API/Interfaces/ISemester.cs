using API.DTO;
using API.Models;

namespace API.Interfaces
{
    public interface ISemester
    {
        public Task<IEnumerable<Semester>> GetSemesters();
        public Task<Semester> GetSemester(string id);
        public Task<string> UpdateSemester(string id, Semester_DTO semester);
        public Task<string> DeleteSemester(string id);
        public Task<string> CreateSemester(Semester_DTO semester);
    }
}
