using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnProgrammingTogether.Controllers
{
    public class Technology : Controller
    {
        private readonly ApplicationDbContext _context;

        public Technology(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var technologies = _context.Technologies.ToList();
            return View(technologies);
        }

        public IActionResult Detail(int id)
        {
            var technology = _context.Technologies.Include(a => a.Adress).FirstOrDefault(t => t.Id == id);
            return View(technology);
        }
    }
}
