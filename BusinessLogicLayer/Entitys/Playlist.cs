using BusinessLogicLayer.EntityDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entitys
{
    public class Playlist
    {
        public Playlist() { }

        public Playlist(PlaylistDTO playlistDTO) 
        {
            Id = playlistDTO.Id;
            Name = playlistDTO.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
