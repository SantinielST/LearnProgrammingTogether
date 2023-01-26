using LearnProgrammingTogether.Interfaces;
using LearnProgrammingTogether.Models;
using LearnProgrammingTogether.Services;
using LearnProgrammingTogether.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnProgrammingTogether.Controllers
{
    public class TechnologyController : Controller
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _contextAccessor;

        public TechnologyController(ITechnologyRepository technologyRepository,  IPhotoService photoService, IHttpContextAccessor contextAccessor)
        {
            _technologyRepository = technologyRepository;
            _photoService = photoService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var technologies = await _technologyRepository.GetAll();
            return View(technologies);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var technology = await _technologyRepository.GetByIdAsync(id);
            return View(technology);
        }

        public IActionResult Create()
        {
            var currentUserId = _contextAccessor.HttpContext.User.GetUserId();
            var createTechnologyViewModel = new CreateTechnologyViewModel()
            {
                AppUserId = currentUserId,
            };
            return View(createTechnologyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTechnologyViewModel technologyVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(technologyVM.Image);
                var technology = new Technology()
                {
                    Title = technologyVM.Title,
                    Description = technologyVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = technologyVM.AppUserId,
                    Adress = new Adress()
                    {
                        Street = technologyVM.Adress.Street,
                        City = technologyVM.Adress.City,
                        Region = technologyVM.Adress.Region,
                        Country = technologyVM.Adress.Country,
                    }
                };

                _technologyRepository.Add(technology);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Ошибка загрузки фото");
            }

            return View(technologyVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var technology = await _technologyRepository.GetByIdAsync(id);
            if (technology == null)
            {
                return View("Error");
            }

            var technologyVM = new EditTechnologyViewModel()
            {
                Title = technology.Title,
                Description = technology.Description,
                AdressId = (int)technology.AdressId,
                Address = technology.Adress,
                Url = technology.Image,
                TechnologyCategory = technology.TechnologyCategory
            };
            return View(technologyVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTechnologyViewModel technologyVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Редактирование не удалось");
                return View("Edit", technologyVM);
            }

            var userTechnology = await _technologyRepository.GetByIdAsyncNoTracking(id);

            if (userTechnology != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userTechnology.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Не удалось удалить фото");
                    return View(technologyVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(technologyVM.Image);
                var technology = new Technology()
                {
                    Id = id,
                    Title = technologyVM.Title,
                    Description = technologyVM.Description,
                    Image = photoResult.Url.ToString(),
                    AdressId = technologyVM.AdressId,
                    Adress = technologyVM.Address,
                    TechnologyCategory = technologyVM.TechnologyCategory
                };
                _technologyRepository.Update(technology);
                return RedirectToAction("Index");
            }
            else
            {
                return View(technologyVM);
            }
        }
    }
}
