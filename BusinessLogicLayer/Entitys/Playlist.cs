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
        public Playlist() 
        {
            User = new User();
        }

        public Playlist(PlaylistDTO playlistDTO) : this()
        {
            Id = playlistDTO.Id;
            Name = playlistDTO.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public User User { get; set; }
    }
}
