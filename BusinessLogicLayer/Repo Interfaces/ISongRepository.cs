using BusinessLogicLayer.EntityDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repo_Interfaces
{
    public interface ISongRepository
    {
        List<SongDTO> GetSongsWithoutArtist();
        List<SongDTO> GetAllSongs();
        List<SongDTO> GetSearchedSongs(string input);
        void InsertSongInPlaylist(int pId, int sId);
        void LinkSongToArtist(int ArtistId, int SongId);
        void CreateNewSong(SongDTO song);


    }
}
