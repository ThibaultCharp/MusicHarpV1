using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.EntityDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Repo_Interfaces;

namespace BusinessLogicLayer.Classes
{
    public class PlaylistBusinessLogic
    {
        List<PlaylistDTO> playlistDTOs = new List<PlaylistDTO>();
        List<SongDTO> songDTOs = new List<SongDTO>();

        private readonly IPlaylistRepository repository;

        public PlaylistBusinessLogic(IPlaylistRepository playlistRepository)
        {
            repository = playlistRepository;
        }

        public List<Playlist> GetSelectedPlaylists(int? user_id)
        {
            playlistDTOs = repository.GetSelectedPlaylists(user_id);
            List<Playlist> playlists = playlistDTOs.Select(dto => new Playlist(dto)).ToList();
            return playlists;
        }

        public PlaylistDTO CreateNewPlaylist(Playlist playlist, int? user_id)
        {
            PlaylistDTO playlistDTO = new PlaylistDTO(playlist);
            repository.CreateNewPlaylist(playlistDTO, user_id);
            return playlistDTO;
        }

        public void DeletePlaylist(int id)
        {
            repository.DeletePlaylist(id);
        }

        public Playlist EditPlaylist(int id, int? user_id)
        {  
            PlaylistDTO playlistDTO = repository.GetWantedPlaylist(id, user_id);
            return new Playlist(playlistDTO); 
        }

        public void SaveEditedPlaylist(Playlist playlist)
        {
            PlaylistDTO playlistDTO = new PlaylistDTO(playlist);
            repository.SaveEditedPlaylist(playlistDTO);
        }

        public List<Song> GetSongsFromPlaylist(int id)
        {
            songDTOs = repository.GetSongsInPlaylist(id);
            List<Song> songs = songDTOs.Select(dto => new Song(dto)).ToList();
            return songs;
        }
    }
}
