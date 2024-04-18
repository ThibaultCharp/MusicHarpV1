using MusicHarpV1.Models;

namespace PresentationLayer.Models
{
    public class LinkSongToArtistViewModel
    {
        public int SongId { get; set; }

        public ArtistViewModel artistViewModel { get; set; }

        public SongViewModel songViewModel { get; set; }
    }
}
