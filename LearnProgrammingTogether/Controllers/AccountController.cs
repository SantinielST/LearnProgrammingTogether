using LearnProgrammingTogether.Data;
using LearnProgrammingTogether.Models;
using LearnProgrammingTogether.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnProgrammingTogether.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var appUser = await _userManager.FindByEmailAsync(loginVM.Email);

            if (appUser != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(appUser, loginVM.Password);
                if (checkPassword)
                {
                    var result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Ошибка пароля. Пожалуйста, попробуйте снова";
                return View(loginVM);
            }
            TempData["Error"] = "Ошибка введённых данных. Пожалуйста, попробуйте снова";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var appUser = await _userManager.FindByEmailAsync(registerVM.Email);
            if (appUser != null)
            {
                TempData["Error"] = "Данный email адрес уже был зарегистрирован";
                return View(registerVM);
            }
            
            var newAppUser = new AppUser()
            {
                Email= registerVM.Email,
                UserName = registerVM.UserName,
                NickName = registerVM.Nickname,
                StudyLang= registerVM.StudyLang,
                TypeFramework = registerVM.TypeFramework,
                Level = registerVM.Level
            };
            var newAppUserResponse = await _userManager.CreateAsync(newAppUser, registerVM.Password);

            if (newAppUserResponse.Succeeded) 
            {
                await _userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }
            else
            {
                TempData["Error"] = "Пароль не подходит!";
                return View(registerVM);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
