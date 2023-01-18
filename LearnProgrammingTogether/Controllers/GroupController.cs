using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnProgrammingTogether.Controllers
{
    public class GroupController : Controller
    {
        private readonly ApplicationDbContext _contex;

        public GroupController(ApplicationDbContext contex)
        {
            _contex = contex;
        }

        public IActionResult Index()
        {
            var groups = _contex.Groups.ToList();
            return View(groups);
        }

        public IActionResult Detail(int id)
        {
            var group = _contex.Groups.Include(a => a.Adress).FirstOrDefault(g => g.Id == id);
            return View(group);
        }
    }
}
