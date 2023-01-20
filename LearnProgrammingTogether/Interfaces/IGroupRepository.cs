using LearnProgrammingTogether.Models;

namespace LearnProgrammingTogether.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAll();
        Task<Group> GetByIdAsync(int id);
        Task<Group> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Group>> GetGroupByCity(string city);

        bool Add(Group group);
        bool Update(Group group);
        bool Delete(Group group);
        bool Save();
    }
}
