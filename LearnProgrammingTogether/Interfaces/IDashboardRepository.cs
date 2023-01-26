using LearnProgrammingTogether.Models;

namespace LearnProgrammingTogether.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Technology>> GetAllUserTechnology();
        Task<List<Group>> GetAllUserGroups();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByIdNoTracking(string id);

        bool Update(AppUser user);
        bool Save();
    }
}
