using DataLogicLayer;
using DataLogicLayer.DAL;
using Microsoft.AspNetCore.Mvc;
using MusicHarpV1.Models;
using System.Diagnostics;
using System.Reflection.Metadata;
using MusicHarpV1.Controllers;
using BusinessLogicLayer.Classes;
using DataLogicLayer.Entitys;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
	public class PlaylistController : Controller
	{


		PlaylistBusinessLogic playlistBusinessLogic = new PlaylistBusinessLogic();
        private readonly PlaylistRepository _playlistRepository;

        public PlaylistController()
        {
            _playlistRepository = new PlaylistRepository();
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}
