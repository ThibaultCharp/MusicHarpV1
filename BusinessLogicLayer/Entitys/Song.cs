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
        public Song() 
        {
            Artist = new Artist();
        }

        public Song(SongDTO songDTO) : this()
        {
            Id = songDTO.Id;
            Artist.Name = songDTO.Artist.Name;
            SongName = songDTO.SongName;
            SongUrl = songDTO.SongUrl;
        }


        public int Id { get; set; }
        public Artist Artist { get; set; }
        public string SongName { get; set; }
        public string SongUrl { get; set; }
    }
}
