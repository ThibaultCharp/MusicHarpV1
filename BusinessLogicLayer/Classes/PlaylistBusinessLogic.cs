using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.EntityDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Repo_Interfaces;
using BusinessLogicLayer.ErrorHandling;

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
            List<Playlist> playlists = new List<Playlist>();
            try 
            {
                playlistDTOs = repository.GetSelectedPlaylists(user_id);
                playlists = playlistDTOs.Select(dto => new Playlist(dto)).ToList();
            }

            catch (DatabaseErrorExeption ex)
            {
                throw new DatabaseErrorExeption("An error occurred while fetching playlists from the database.", ex);
            }
            return playlists;

        }

        public ServiceResponse CreateNewPlaylist(Playlist playlist, int? user_id)
        {
            ServiceResponse response = new ServiceResponse() { Success = false };

            if (playlist.Name.Length >= 30 && playlist.Name.Length!= 0)
            {
                response.ErrorMessage = ("Title must be shorter than 30 characters");
                return response;
            }
            else
            {
                PlaylistDTO playlistDTO = new PlaylistDTO(playlist);
                repository.CreateNewPlaylist(playlistDTO, user_id);
                response.Success = true;
            }
            return response;
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
