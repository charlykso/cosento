using API.DTO;
using API.Models;

namespace API.Interfaces
{
    public interface ILevel
    {
        public Task<IEnumerable<Level>> GetLevels();
        public Task<Level> GetLevel(string id);
        public Task<string> UpdateLevel(string id, Level_DTO level);
        public Task<string> DeleteLevel(string id);
        public Task<string> CreateLevel(Level_DTO level);
    }
}
