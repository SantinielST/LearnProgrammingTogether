using Microsoft.AspNetCore.Mvc;

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
    }
}
