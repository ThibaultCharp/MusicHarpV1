using BusinessLogicLayer.Entitys;
namespace MusicHarpV1.Models
{
    public class SongViewModel
    {
        public List<Song> songList { get; set;}
        
        public User user { get; set; }
    }
}