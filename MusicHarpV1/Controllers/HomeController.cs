using Microsoft.AspNetCore.Mvc;
using MusicHarpV1.Models;
using System.Diagnostics;
using BusinessLogicLayer.Classes;
using DataLogicLayer.Entitys;
using PresentationLayer.Models;
using DataLogicLayer.DAL;
using System.Reflection.Metadata;

namespace MusicHarpV1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        SongBusinessLogic songBusinessLogic = new SongBusinessLogic();
        PlaylistBusinessLogic playlistBusinessLogic = new PlaylistBusinessLogic();

        public IActionResult Index()
        {
            List<Song> songs = songBusinessLogic.GetAllSongs();

            SongViewModel songViewModel = new SongViewModel();
            songViewModel.songList = songs;

            return View(songViewModel);
        }

        

        public IActionResult Playlist(Playlist playlistName)
        {
            List<Playlist> playlists = playlistBusinessLogic.GetSelectedPlaylists();

            PlaylistViewModel playlistViewModel = new PlaylistViewModel();
            playlistViewModel.PlaylistList = playlists;
            return View(playlistViewModel);
        }


        [HttpPost]
        public IActionResult CreatePlaylist(Playlist playlist)
        {
            playlistBusinessLogic.CreateNewPlaylist(playlist);

            return RedirectToAction("Playlist");
        }

        [HttpPost]
        public IActionResult DeletePlaylist(int id)
        {
            playlistBusinessLogic.DeletePlaylist(id);

            return RedirectToAction("Playlist");
        }



        public IActionResult EditPlaylist(int id) 
        {
            

            EditPlaylistViewModel playlistViewModel = new EditPlaylistViewModel();
            playlistViewModel.playlist = playlistBusinessLogic.EditPlaylist(id);

            return View(playlistViewModel);
        }

        public IActionResult SaveEditedPlaylist(Playlist playlist) 
        {
            playlistBusinessLogic.SaveEditedPlaylist(playlist);
            return RedirectToAction("Playlist");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
