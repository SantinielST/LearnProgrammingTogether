using CloudinaryDotNet.Actions;
using LearnProgrammingTogether.Interfaces;
using LearnProgrammingTogether.Models;
using LearnProgrammingTogether.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearnProgrammingTogether.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboard;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboard, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _dashboard = dashboard;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        public void MapUserEdit(AppUser user, EditUserViewModel editUserViewModel, ImageUploadResult imageUploadResult)
        {
            user.Id = editUserViewModel.Id;
            user.NickName = editUserViewModel.NickName;
            user.StudyLang = editUserViewModel.StudyLang;
            user.TypeFramework = editUserViewModel.TypeFramework;
            user.Level = editUserViewModel.Level;
            user.ProfileImageUrl = imageUploadResult.Url.ToString();
        }

        public async Task<IActionResult> Index()
        {
            var userTechnologies = await _dashboard.GetAllUserTechnology();
            var userGroups = await _dashboard.GetAllUserGroups();
            var dashboardVM = new DashboardViewModel()
            {
                Technologies = userTechnologies,
                Groups = userGroups
            };
            return View(dashboardVM);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboard.GetUserById(currentUserId);

            if (user == null)
            {
                return View("Error");
            }

            var editUserViewModel = new EditUserViewModel()
            {
                Id = currentUserId,
                NickName = user.NickName,
                StudyLang = user.StudyLang,
                TypeFramework = user.TypeFramework,
                ProfileImageUrl = user.ProfileImageUrl,
                Level= user.Level
            };
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserViewModel editUserVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Изменение не удалось");
                return View("EditUserProfile", editUserVM);
            }

            var user = await _dashboard.GetUserByIdNoTracking(editUserVM.Id);

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editUserVM.Image);
                MapUserEdit(user, editUserVM, photoResult);

                _dashboard.Update(user);

                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Фото не удалено");
                    return View(editUserVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(editUserVM.Image);

                MapUserEdit(user, editUserVM, photoResult);

                _dashboard.Update(user);

                return RedirectToAction("Index");
            }
        }
    }
}
