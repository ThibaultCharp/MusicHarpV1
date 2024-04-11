using DataLogicLayer.DAL;
using DataLogicLayer.Entitys;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Classes
{
    public class PlaylistBusinessLogic
    {
        List<Playlist> playlists = new List<Playlist>();

        private PlaylistRepository repository = new PlaylistRepository();

        public List<Playlist> GetSelectedPlaylists()
        {
            playlists = repository.GetSelectedPlaylists();
            return playlists;
        }

        public Playlist CreateNewPlaylist(Playlist playlist)
        {
            repository.CreateNewPlaylist(playlist);
            return playlist;
        }

        public void DeletePlaylist(int id)
        {
            repository.DeletePlaylist(id);
        }

        public Playlist EditPlaylist(int id)
        {    
            return repository.GetWantedPlaylist(id); 
        }

        public void SaveEditedPlaylist(Playlist playlist)
        {
            repository.SaveEditedPlaylist(playlist);
        }

        public List<Song> GetSongsFromPlaylist(int id)
        {
            return repository.GetSongsInPlaylist(id);
        }
    }
}
