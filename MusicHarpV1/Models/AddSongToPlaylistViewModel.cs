namespace PresentationLayer.Models
{
    public class AddSongToPlaylistViewModel
    {
        public int sId {  get; set; }
        public PlaylistViewModel playlistViewModel { get; set; }
        public PlaylistSongsViewModel playlistSongsViewModel { get; set; }
    }
}
