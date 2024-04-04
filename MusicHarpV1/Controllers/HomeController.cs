using Microsoft.AspNetCore.Mvc;
using MusicHarpV1.Models;
using System.Diagnostics;
using BusinessLogicLayer.Classes;
using DataLogicLayer.Entitys;

namespace MusicHarpV1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            SongBusinessLogic songBusinessLogic = new SongBusinessLogic();
            List<Song> songs = songBusinessLogic.GetAllSongs();

            HomeViewModel songViewModel = new HomeViewModel();
            songViewModel.songList = songs;

            return View(songViewModel);
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
