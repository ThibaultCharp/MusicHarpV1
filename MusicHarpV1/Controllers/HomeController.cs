using Microsoft.AspNetCore.Mvc;
using MusicHarpV1.Models;
using System.Diagnostics;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Entitys;
using PresentationLayer.Models;
using DataLogicLayer.DAL;
using System.Reflection.Metadata;
using BusinessLogicLayer.Repo_Interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using MySql.Data.MySqlClient;
using Azure;
using BusinessLogicLayer.ErrorHandling;

namespace MusicHarpV1.Controllers
{
    public class HomeController : Controller
    {
        private readonly SongBusinessLogic songBusinessLogic;
        private readonly PlaylistBusinessLogic playlistBusinessLogic;
        private readonly ArtistBusinessLogic artistBusinessLogic;
        private readonly UserBL userBusinessLogic;

        public HomeController() 
        {
            songBusinessLogic = new SongBusinessLogic(new SongRepository());
            playlistBusinessLogic = new PlaylistBusinessLogic(new PlaylistRepository());
            artistBusinessLogic = new ArtistBusinessLogic(new ArtistRepostitory());
        }
        


        public IActionResult HomePage(string input)
        {
            SongViewModel songViewModel = new SongViewModel();
            string ProfilePicture = HttpContext.Session.GetString("ProfilePicture");
            string Name = HttpContext.Session.GetString("Username");
            
            List<Song> songs;
            if (string.IsNullOrEmpty(input))
            {
                songs = songBusinessLogic.GetAllSongs();
            }
            else
            {
                songs = songBusinessLogic.GetSearchedSongs(input);
            }

            songViewModel.user = new User { ProfilePicture = ProfilePicture, Name = Name };
            songViewModel.songList = songs;
            return View(songViewModel);
        }

       
        public IActionResult Index()
        {
            ErrorViewModel errorViewModel = new ErrorViewModel();

            return View(errorViewModel);

        }


        public IActionResult Playlist(int? user_id)
        {
            try
            {
                user_id = HttpContext.Session.GetInt32("User_Id");

                List<Playlist> playlists = playlistBusinessLogic.GetSelectedPlaylists(user_id);

                PlaylistViewModel playlistViewModel = new PlaylistViewModel();
                playlistViewModel.PlaylistList = playlists;

                if (TempData["ErrorMessage"] != null)
                {
                    ViewBag.ErrorMessage = TempData["ErrorMessage"];
                }

                return View(playlistViewModel);
            }

            catch (DatabaseErrorExeption ex) 
            {
                return View(new DatabaseErrorExeption("sdasc", ex));
            }
           

        }

        public IActionResult Artist() 
        {
            List<Artist> artists = artistBusinessLogic.GetAllArtists();
            ArtistViewModel artistViewModel = new ArtistViewModel();
            artistViewModel.artists = artists;
            return View(artistViewModel);
        }
        public IActionResult SongsWithoutArtist()
        {
            List<Song> songs = songBusinessLogic.GetSongsWithoutArtist();

            SongsWithoutArtistViewModel songsWithoutArtistViewModel = new SongsWithoutArtistViewModel();
            songsWithoutArtistViewModel.songsWithoutArtist = songs;
            return View(songsWithoutArtistViewModel);
        }


        public IActionResult CreatePlaylist(Playlist playlist, int? user_id)
        {
            user_id = HttpContext.Session.GetInt32("User_Id");
            
            var response = playlistBusinessLogic.CreateNewPlaylist(playlist, user_id);

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            return RedirectToAction("Playlist");
        }

        public IActionResult CreateSong(Song song)
        {
            songBusinessLogic.CreateNewSong(song);
            return RedirectToAction("SongsWithoutArtist");
        }

        public ActionResult CreateArtist(Artist artist)
        {
            artistBusinessLogic.CreateNewArtist(artist);
            return RedirectToAction("Artist");
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

        public IActionResult ArtistSongs(int id)
        {
            ArtistSongsViewModel artistSongsViewModel = new ArtistSongsViewModel();
            artistSongsViewModel.songs = artistBusinessLogic.GetSongsFromArtist(id);
            return View(artistSongsViewModel);
        }

        public IActionResult EditPlaylist(int id, int? user_id) 
        {
            user_id = HttpContext.Session.GetInt32("User_Id");

            EditPlaylistViewModel editPlaylistViewModel = new EditPlaylistViewModel();
            editPlaylistViewModel.playlist = playlistBusinessLogic.EditPlaylist(id, user_id);

            return View(editPlaylistViewModel);
        }

        public IActionResult SaveEditedPlaylist(Playlist playlist) 
        {
            playlistBusinessLogic.SaveEditedPlaylist(playlist);
            return RedirectToAction("Playlist");
        }

        public IActionResult SelectPlaylistToAddSong(int sId, int? user_id)
        {
            user_id = HttpContext.Session.GetInt32("User_Id");


            AddSongToPlaylistViewModel addSongToPlaylistViewModel = new AddSongToPlaylistViewModel();
            addSongToPlaylistViewModel.sId = sId;
            List<Playlist> playlists = playlistBusinessLogic.GetSelectedPlaylists(user_id);

            PlaylistViewModel playlistViewModel = new PlaylistViewModel();
            playlistViewModel.PlaylistList = playlists;
            addSongToPlaylistViewModel.playlistViewModel = playlistViewModel;   
            return View(addSongToPlaylistViewModel);
        }


        public IActionResult SelectArtistToLinkSong(int SongId)
        {
            LinkSongToArtistViewModel linkSongToArtistViewModel = new LinkSongToArtistViewModel();
            linkSongToArtistViewModel.SongId = SongId;
            List<Artist> artists = artistBusinessLogic.GetAllArtists();

            ArtistViewModel artistViewModel = new ArtistViewModel();
            artistViewModel.artists = artists;
            linkSongToArtistViewModel.artistViewModel = artistViewModel;
            return View(linkSongToArtistViewModel);
        }

        public IActionResult LinkSongToArtist(int ArtistId, int SongId)
        {
            songBusinessLogic.LinkSongToArtist(ArtistId, SongId);
            return RedirectToAction("HomePage");
        }

        public IActionResult AddSongToPlaylist(int pId, int sId)
        {
            songBusinessLogic.InsertSongInPlaylist(pId, sId);
            return RedirectToAction("HomePage");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
