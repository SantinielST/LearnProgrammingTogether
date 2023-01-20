﻿using LearnProgrammingTogether.Interfaces;
using LearnProgrammingTogether.Models;
using LearnProgrammingTogether.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearnProgrammingTogether.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IPhotoService _photoService;

        public GroupController(IGroupRepository groupRepository, IPhotoService photoService)
        {
            _groupRepository = groupRepository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupRepository.GetAll();
            return View(groups);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            return View(group);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupViewModel groupVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(groupVM.Image);
                var group = new Group()
                {
                    Title = groupVM.Title,
                    Description = groupVM.Description,
                    Image = result.Url.ToString(),
                    Adress = new Adress()
                    {
                        Street = groupVM.Adress.Street,
                        City = groupVM.Adress.City,
                        Region = groupVM.Adress.Region,
                        Country = groupVM.Adress.Country
                    }
                };

                _groupRepository.Add(group);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Ошибка загрузки фото");
            }

            return View(groupVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            if (group == null)
            {
                return View("Error");
            }

            var groupVM = new EditGroupViewModel()
            {
                Title = group.Title,
                Description = group.Description,
                AdressId = (int)group.AdressId,
                Adress = group.Adress,
                Url = group.Image,
                GroupCategory = group.GroupCategory
            };
            return View(groupVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditGroupViewModel groupVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Редактирование не удалось");
                return View("Edit", groupVM);
            }

            var userGroup = await _groupRepository.GetByIdAsyncNoTracking(id);

            if (userGroup != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userGroup.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Не удалось удалить фото");
                    return View(groupVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(groupVM.Image);
                var group = new Group()
                {
                    Id = id,
                    Title = groupVM.Title,
                    Description = groupVM.Description,
                    Image = photoResult.Url.ToString(),
                    AdressId = groupVM.AdressId,
                    Adress = groupVM.Adress,
                };
                _groupRepository.Update(group);
                return RedirectToAction("Index");
            }
            else
            {
                return View(groupVM);
            }
        }
    }
}
