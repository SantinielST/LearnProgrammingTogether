using LearnProgrammingTogether.Interfaces;
using LearnProgrammingTogether.Models;
using Microsoft.EntityFrameworkCore;

public class GroupRepository : IGroupRepository
{
    private readonly ApplicationDbContext _context;

    public GroupRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Group>> GetAll()
    {
        return await _context.Groups.ToListAsync();
    }

    public async Task<Group> GetByIdAsync(int id)
    {
        return await _context.Groups.Include(a => a.Adress).FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Group> GetByIdAsyncNoTracking(int id)
    {
        return await _context.Groups.Include(a => a.Adress).AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<Group>> GetGroupByCity(string city)
    {
        return await _context.Groups.Where(g => g.Adress.City.Contains(city)).ToListAsync();
    }

    public bool Add(Group group)
    {
        _context.Add(group);
        return Save();
    }

    public bool Delete(Group group)
    {
        _context.Remove(group);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool Update(Group group)
    {
        _context.Update(group);
        return Save();
    }
}