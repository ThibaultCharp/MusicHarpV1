using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.EntityDTO_s;

namespace BusinessLogicLayer.Entitys
{
    public class Artist
    {
        public Artist() { }
        public Artist(ArtistDTO artistDTO) 
        {
            Id = artistDTO.Id;
            Name = artistDTO.Name;
        }


        public int Id { get; set; }
        public string Name { get; set; }

    }
}
