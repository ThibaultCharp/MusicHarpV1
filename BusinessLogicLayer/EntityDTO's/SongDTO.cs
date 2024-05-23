using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Entitys;

namespace BusinessLogicLayer.EntityDTO_s
{
    public class SongDTO
    {
        public SongDTO() 
        {
            Artist = new ArtistDTO();
        }

        public SongDTO(Song song) : this()
        {
            Id = song.Id;
            Artist.Name = song.Artist.Name;
            SongName = song.SongName;
            SongUrl = song.SongUrl;
        }

        public int Id { get; set; }
        public ArtistDTO Artist { get; set; }
        public string SongName { get; set; }
        public string SongUrl { get; set; }
    }
}
