using LearnProgrammingTogether.Helpers;
using LearnProgrammingTogether.Interfaces;
using LearnProgrammingTogether.Models;
using LearnProgrammingTogether.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace LearnProgrammingTogether.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGroupRepository _groupRepository;

        public HomeController(ILogger<HomeController> logger, IGroupRepository groupRepository)
        {
            _logger = logger;
           _groupRepository = groupRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeViewModel = new HomeViewModel();

            try
            {
                string url = "https://ipinfo.io/";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.NativeName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.Region = ipInfo.Region;

                if (homeViewModel.City != null)
                {
                    homeViewModel.Groups = await _groupRepository.GetGroupByCity(homeViewModel.City);
                }
              
                return View(homeViewModel);
            }
            catch
            {
                homeViewModel.Groups = null;
            }

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}