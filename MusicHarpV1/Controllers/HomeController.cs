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
        ArtistBusinessLogic artistBusinessLogic = new ArtistBusinessLogic();

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

        public IActionResult Playlist()
        {
            List<Playlist> playlists = playlistBusinessLogic.GetSelectedPlaylists();

            PlaylistViewModel playlistViewModel = new PlaylistViewModel();
            playlistViewModel.PlaylistList = playlists;
            return View(playlistViewModel);
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


        public IActionResult CreatePlaylist(Playlist playlist)
        {
            playlistBusinessLogic.CreateNewPlaylist(playlist);

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
            return RedirectToAction("Index");
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
