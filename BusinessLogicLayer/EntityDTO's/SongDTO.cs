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
        public SongDTO() { }

        public SongDTO(Song song) 
        {
            Id = song.Id;
            ArtistName = song.ArtistName;
            SongName = song.SongName;
            SongUrl = song.SongUrl;
        }

        public int Id { get; set; }
        public string ArtistName { get; set; }
        public string SongName { get; set; }
        public string SongUrl { get; set; }
    }
}
