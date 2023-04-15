using API.DTO;
using API.Models;

namespace API.Interfaces
{
    public interface ILecturer
    {
        public Task<IEnumerable<Lecturer>> GetLecturers();
        public Task<Lecturer> GetLecturer(string id);
        public Task<string> UpdateLecturer(string id, Lecturer_DTO lecturer);
        public Task<string> DeleteLecturer(string id);
        public Task<string> CreateLecturer(Lecturer_DTO lecturer);
        public Task<bool> CheckLecturerEmail(string email);
    }
}
