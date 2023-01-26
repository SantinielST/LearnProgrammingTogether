using LearnProgrammingTogether.Interfaces;
using LearnProgrammingTogether.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearnProgrammingTogether.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            var result = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    NickName = user.NickName,
                    StudyLang = user.StudyLang,
                    Level = user.Level,
                    TypeFramework = user.TypeFramework,
                    ProfileImageUrl = user.ProfileImageUrl
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                NickName = user.NickName,
                StudyLang = user.StudyLang,
                Level = user.Level,
                TypeFramework = user.TypeFramework
            };
            return View(userDetailViewModel);
        }

    }
}
