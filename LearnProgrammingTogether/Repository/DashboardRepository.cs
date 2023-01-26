using LearnProgrammingTogether.Interfaces;
using LearnProgrammingTogether.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnProgrammingTogether.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Group>> GetAllUserGroups()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userGroups = _applicationDbContext.Groups.Where(g => g.AppUser.Id == currentUser);
            return userGroups.ToList();
        }

        public async Task<List<Technology>> GetAllUserTechnology()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userTechnologies = _applicationDbContext.Technologies.Where(t => t.AppUser.Id == currentUser);
            return userTechnologies.ToList();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _applicationDbContext.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _applicationDbContext.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _applicationDbContext.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _applicationDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
