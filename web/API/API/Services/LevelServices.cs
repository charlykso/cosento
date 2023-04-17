using API.DTO;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class LevelServices : ILevel
    {
        private readonly Cosento_DBContext? _dbContext;
        public LevelServices(Cosento_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> CreateLevel(Level_DTO level)
        {
            try
            {
                var newLevel = new Level();
                var Id = Guid.NewGuid();
                newLevel.Id = Id.ToString();
                newLevel.Name = level.Name;
                newLevel.Created_at = DateTime.Now;
                newLevel.Updated_at = DateTime.Now;

                await _dbContext!.Levels.AddAsync(newLevel);
                _dbContext.SaveChanges();

                return "Level created";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<string> DeleteLevel(string id)
        {
            try
            {
                var level = await _dbContext!.Levels.SingleOrDefaultAsync(l => l.Id == id);
                if (level == null)
                {
                    return "Level not found";
                }
                _dbContext.Remove(level);
                _dbContext.SaveChanges();

                return "Level deleted successfuly";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<Level> GetLevel(string id)
        {
            try
            {
                var level = await _dbContext!.Levels.Where(l => l.Id == id)!
                    .Include(c => c.Courses)
                    .FirstOrDefaultAsync();
                if (level == null)
                {
                    return null!;
                }
                return level;
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task<IEnumerable<Level>> GetLevels()
        {
            try
            {
                var levels = await _dbContext!.Levels
                    .Include(c => c.Courses)!.ThenInclude(s => s.Semester)
                    .ToListAsync();
                if (levels.Count == 0)
                {
                    return null!;
                }
                return levels;
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task<string> UpdateLevel(string id, Level_DTO level)
        {
            try
            {
                var editLevel = await _dbContext!.Levels.FindAsync(id);
                if (editLevel == null)
                {
                    return "Level not found";
                }
                editLevel.Name = level.Name;
                editLevel.Updated_at = DateTime.Now;

                _dbContext.Levels.Attach(editLevel);
                await _dbContext.SaveChangesAsync();

                return "Updated successfuly";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
