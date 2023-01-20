using LearnProgrammingTogether.Interfaces;
using LearnProgrammingTogether.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnProgrammingTogether.Repository
{
    public class TechnologyRepository : ITechnologyRepository
    {
        private readonly ApplicationDbContext _context;

        public TechnologyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Technology>> GetAll()
        {
            return await _context.Technologies.ToListAsync();
        }

        public async Task<IEnumerable<Technology>> GetAllTechnologiesByCity(string city)
        {
            return await _context.Technologies.Where(t => t.Adress.City.Contains(city)).ToListAsync();
        }

        public async Task<Technology> GetByIdAsync(int id)
        {
            return await _context.Technologies.Include(a => a.Adress).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Technology> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Technologies.Include(a => a.Adress).AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public bool Add(Technology technology)
        {
            _context.Add(technology);
            return Save();
        }

        public bool Delete(Technology technology)
        {
            _context.Remove(technology);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Technology technology)
        {
            _context.Update(technology);
            return Save();
        }
    }
}
