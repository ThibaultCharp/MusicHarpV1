using BusinessLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.EntityDTO_s
{
    public class ArtistDTO
    {
        public ArtistDTO() { }

        public ArtistDTO(Artist artist) 
        {
            Id = artist.Id;
            Name = artist.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
