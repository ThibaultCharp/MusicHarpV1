using DataLogicLayer.Entitys;

namespace MusicHarpV1.Models
{
    public class SongViewModel
    {
        public List<Song> songList { get; set;}

        public List<Song>? SearchedSongList { get; set;}
        public string input { get; set; }

        public int songInput { get; set; }

        public int sId { get; set; }
    }
}