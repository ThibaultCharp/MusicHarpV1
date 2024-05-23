using BusinessLogicLayer.EntityDTO_s;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.Repo_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHarpUnitTest
{
    public class FakePlaylistDAL : IPlaylistRepository
    {
        List<PlaylistDTO> playlists = new List<PlaylistDTO>();

        UserDTO user1 = new UserDTO { Id = 1, Name = "Test", Password = "password", ProfilePicture = "swedrfgyhuj" };
        UserDTO user2 = new UserDTO { Id = 2, Name = "Test", Password = "password", ProfilePicture = "swedrfgyhuj" };

        public FakePlaylistDAL()
        {
            playlists.Add(new PlaylistDTO { Id = 1, Name = "P1", User = user1 });
            playlists.Add(new PlaylistDTO { Id = 2, Name = "P2", User = user2 });
            playlists.Add(new PlaylistDTO { Id = 3, Name = "P3", User = user2 });
            playlists.Add(new PlaylistDTO { Id = 4, Name = "P4", User = user1 });
            playlists.Add(new PlaylistDTO { Id = 5, Name = "P5", User = user1 });
        }

        public List<PlaylistDTO> GetSelectedPlaylists(int? user_id)
        {
            if (user_id == null)
                return new List<PlaylistDTO>();

            return playlists.Where(p => p.User.Id == user_id).OrderByDescending(p => p.Id).ToList();
        }

        public PlaylistDTO GetWantedPlaylist(int id, int? user_id)
        {
            return playlists.FirstOrDefault(p => p.Id == id && p.User.Id == user_id);
        }

        public void CreateNewPlaylist(PlaylistDTO playlist, int? user_id)
        {
            playlist.Id = playlists.Max(p => p.Id) + 1;
            playlist.User.Id = user_id ?? playlist.User.Id;
            playlists.Add(playlist);
        }

        public void DeletePlaylist(int id)
        {
            var playlistToRemove = playlists.FirstOrDefault(p => p.Id == id);
            if (playlistToRemove != null)
                playlists.Remove(playlistToRemove);
        }

        public void SaveEditedPlaylist(PlaylistDTO playlist)
        {
            var existingPlaylist = playlists.FirstOrDefault(p => p.Id == playlist.Id);
            if (existingPlaylist != null)
            {
                existingPlaylist.Name = playlist.Name;
                existingPlaylist.User = playlist.User;
            }
        }

        public List<SongDTO> GetSongsInPlaylist(int id)
        {
            return new List<SongDTO>(); // Return an empty list for simplicity
        }
    }
}
