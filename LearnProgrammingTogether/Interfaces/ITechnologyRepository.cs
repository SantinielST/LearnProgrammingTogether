using LearnProgrammingTogether.Models;

namespace LearnProgrammingTogether.Interfaces
{
    public interface ITechnologyRepository
    {
        Task<IEnumerable<Technology>> GetAll();
        Task<Technology> GetByIdAsync(int id);
        Task<Technology> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Technology>> GetAllTechnologiesByCity(string city);

        bool Add(Technology technology);
        bool Update(Technology technology);
        bool Delete(Technology technology);
        bool Save();
    }
}
