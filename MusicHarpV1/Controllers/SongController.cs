using DataLogicLayer;
using DataLogicLayer.DAL;
using Microsoft.AspNetCore.Mvc;
using MusicHarpV1.Models;
using System.Diagnostics;
using System.Reflection.Metadata;
using MusicHarpV1.Controllers;
using BusinessLogicLayer.Classes;
using DataLogicLayer.Entitys;

namespace PresentationLayer.Controllers
{
	public class SongController : Controller
	{

		private readonly ILogger<SongController> _logger;

		public SongController(ILogger<SongController> logger)
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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}
