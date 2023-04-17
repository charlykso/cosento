using API.DTO;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class LecturerServices : ILecturer
    {
        private readonly IMapper? _imapper;
        private readonly Cosento_DBContext? _dbContext;
        public LecturerServices(IMapper? imapper, Cosento_DBContext? dbContext)
        {
            _imapper = imapper;
            _dbContext = dbContext;
        }

        public async Task<bool> CheckLecturerEmail(string email)
        {
            var exists = await _dbContext!.Lecturers.FirstOrDefaultAsync(e => e.Email == email);
            if (exists == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> CreateLecturer(Lecturer_DTO lecturer)
        {
            try
            {
                var emailExists = await CheckLecturerEmail(lecturer.Email!);
                if (emailExists)
                {
                    return "Email already exists";
                }
                var newLecturer = _imapper!.Map<Lecturer>(lecturer);

                var Id = Guid.NewGuid();
                newLecturer.Id = Id.ToString();
                newLecturer.Created_at = DateTime.Now;
                newLecturer.Updated_at = DateTime.Now;
                newLecturer.SearchString = lecturer.Firstname!.ToUpper() + " "
                    + lecturer.Lastname!.ToUpper() + " " + lecturer.Email!.ToUpper() + " "
                    + lecturer.Gender!.ToUpper() + " " + lecturer.Title!.ToUpper() + " "
                    + lecturer.Marital_status!.ToUpper();
                if (lecturer.Role == null)
                {
                    newLecturer.Role = "Lecturer";
                }
                else
                {
                    newLecturer.Role = lecturer.Role;
                }


                var lect = await _dbContext!.Lecturers.AddAsync(newLecturer);
                if (lect == null)
                {
                    throw new Exception("Something went wrong");
                }
                await _dbContext.SaveChangesAsync();
                return "Lecturer created";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteLecturer(string id)
        {
            try
            {
                var lecturer = await _dbContext!.Lecturers.Include(c => c.Lecturer_Courses).SingleOrDefaultAsync(l => l.Id == id);
                
                if (lecturer != null)
                {
                    lecturer.Lecturer_Courses!.Clear();
                    _dbContext.Remove(lecturer);
                    await _dbContext.SaveChangesAsync();
                    return "Deleted successfuly";
                }
                throw new Exception("Lecturer not found");
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<Lecturer> GetLecturer(string id)
        {
            try
            {
                var lecturer = await _dbContext!.Lecturers.Where(l => l.Id == id)
                    .Include(c => c.Lecturer_Courses)!.ThenInclude(mc => mc.Course)
                    .FirstOrDefaultAsync();

                if (lecturer == null)
                {
                    Console.WriteLine("Lecturer not found");
                    return null!;
                }
                return lecturer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<IEnumerable<Lecturer>> GetLecturers()
        {
            try
            {
                var lecturers = await _dbContext!.Lecturers
                    .OrderByDescending(l => l.Created_at)
                    .Include(c => c.Lecturer_Courses)!.ThenInclude(c => c.Course)
                    .ToListAsync();

                if (lecturers.Count == 0)
                {
                    return null!;
                }
                return lecturers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<string> UpdateLecturer(string id, Lecturer_DTO lecturer)
        {
            try
            {
                var editLecturer = await _dbContext!.Lecturers.FindAsync(id);
                if (editLecturer == null)
                {
                    throw new Exception("Lecturer not found");
                }
                editLecturer.Firstname = lecturer.Firstname;
                editLecturer.Lastname = lecturer.Lastname;
                editLecturer.Email = lecturer.Email;
                editLecturer.Gender = lecturer.Gender;
                editLecturer.PhoneNumber = lecturer.PhoneNumber;
                editLecturer.Title = lecturer.Title;
                editLecturer.Marital_status = lecturer.Marital_status;
                editLecturer.Updated_at = DateTime.Now;
                editLecturer.Role = lecturer.Role;
                editLecturer.SearchString = lecturer.Firstname!.ToUpper() + " "
                    + lecturer.Lastname!.ToUpper() + " " + lecturer.Email!.ToUpper() + " "
                    + lecturer.Gender!.ToUpper() + " " + lecturer.Title!.ToUpper() + " "
                    + lecturer.Marital_status!.ToUpper();

                _dbContext.Lecturers.Attach(editLecturer);
                _dbContext.SaveChanges();

                return "Lecturer updated";
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (ex.Message);
            }
        }
    }
}
