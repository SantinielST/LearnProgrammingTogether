using LearnProgrammingTogether.Interfaces;
using LearnProgrammingTogether.Models;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<AppUser>> GetAllUsers()
    {
        return await _applicationDbContext.Users.ToListAsync();
    }

    public async Task<AppUser> GetUserById(string id)
    {
        return await _applicationDbContext.Users.FindAsync(id);
    }

    public bool Add(AppUser user)
    {
        throw new NotImplementedException();
    }

    public bool Delete(AppUser user)
    {
        throw new NotImplementedException();
    }

    public bool Save()
    {
        var saved = _applicationDbContext.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool Update(AppUser user)
    {
        _applicationDbContext.Update(user); 
        return Save();
    }
}