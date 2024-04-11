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

        //public IActionResult Index()
        //{
        //    List<Song> songs = songBusinessLogic.GetAllSongs();

        //    SongViewModel songViewModel = new SongViewModel();
        //    songViewModel.songList = songs;

        //    return View(songViewModel);
        //}

        public IActionResult Index(string input)
        {
            SongViewModel songViewModel = new SongViewModel();
            List<Song> songs;
            if (string.IsNullOrEmpty(input))
            {
                songs = songBusinessLogic.GetAllSongs();
            }
            else
            {
                songs = songBusinessLogic.GetSearchedSongs(input);
            }

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


        
        public IActionResult CreatePlaylist(Playlist playlist)
        {
            playlistBusinessLogic.CreateNewPlaylist(playlist);

            return RedirectToAction("Playlist");
        }

        
        public IActionResult DeletePlaylist(int id)
        {
            playlistBusinessLogic.DeletePlaylist(id);

            return RedirectToAction("Playlist");
        }
        public IActionResult PlaylistSongs(int id)
        {
            PlaylistSongsViewModel playlistSongsViewModel = new PlaylistSongsViewModel();
            playlistSongsViewModel.songs = playlistBusinessLogic.GetSongsFromPlaylist(id);
            return View(playlistSongsViewModel);
        }


        public ActionResult SearchedSongs(string input) 
        {
            
            SongViewModel songViewModel = new SongViewModel();
            List<Song> songs = songBusinessLogic.GetSearchedSongs(input);

            songViewModel.songList = songs;

            return View(songViewModel);
        }


        public IActionResult EditPlaylist(int id) 
        {
            EditPlaylistViewModel editPlaylistViewModel = new EditPlaylistViewModel();
            editPlaylistViewModel.playlist = playlistBusinessLogic.EditPlaylist(id);

            return View(editPlaylistViewModel);
        }

        public IActionResult SaveEditedPlaylist(Playlist playlist) 
        {
            playlistBusinessLogic.SaveEditedPlaylist(playlist);
            return RedirectToAction("Playlist");
        }

        public IActionResult SelectPlaylistToAddSong(int sId)
        {
            AddSongToPlaylistViewModel addSongToPlaylistViewModel = new AddSongToPlaylistViewModel();
            addSongToPlaylistViewModel.sId = sId;
            List<Playlist> playlists = playlistBusinessLogic.GetSelectedPlaylists();

            PlaylistViewModel playlistViewModel = new PlaylistViewModel();
            playlistViewModel.PlaylistList = playlists;
            addSongToPlaylistViewModel.playlistViewModel = playlistViewModel;   
            return View(addSongToPlaylistViewModel);
        }

        public IActionResult AddSongToPlaylist(int pId, int sId)
        {
            songBusinessLogic.InsertSongInPlaylist(pId, sId);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
