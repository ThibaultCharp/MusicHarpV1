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
	public class PlaylistController : Controller
	{

		private readonly ILogger<PlaylistController> _logger;


        private readonly PlaylistRepository _playlistRepository;

        public PlaylistController()
        {
            // Initialize your repository with the appropriate connection
            _playlistRepository = new PlaylistRepository();
        }

        [HttpPost]
        public IActionResult CreatePlaylist(Playlist playlistName)
        {
            try
            {
                // Create a new playlist using your repository
                _playlistRepository.CreateNewPlaylist(playlistName);

                // Redirect to a success page or display a success message
                return RedirectToAction("Index"); // Example: Redirect back to the index page
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., log, display an error page, etc.)
                // You can customize this part based on your project's error handling strategy
                return View("Error"); // Example: Redirect to an error page
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}
