using BusinessLogicLayer.EntityDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repo_Interfaces
{
    public interface IPlaylistRepository
    {
        List<PlaylistDTO> GetSelectedPlaylists(int? user_id);
        PlaylistDTO GetWantedPlaylist(int id, int? user_id);
        void CreateNewPlaylist(PlaylistDTO playlist, int? user_id);
        void DeletePlaylist(int id);
        void SaveEditedPlaylist(PlaylistDTO playlist);
        List<SongDTO> GetSongsInPlaylist(int id);

    }
}
