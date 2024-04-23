using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.EntityDTO_s;


namespace BusinessLogicLayer.Entitys
{
    public class Song
    {
        public Song() { }

        public Song(SongDTO songDTO) 
        {
            Id = songDTO.Id;
            ArtistName = songDTO.ArtistName;
            SongName = songDTO.SongName;
            SongUrl = songDTO.SongUrl;
        }


        public int Id { get; set; }
        public string ArtistName { get; set; }
        public string SongName { get; set; }
        public string SongUrl { get; set; }
    }
}
