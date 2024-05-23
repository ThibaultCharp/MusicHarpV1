using BusinessLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.EntityDTO_s
{
    public class PlaylistDTO
    {
        public PlaylistDTO() 
        {
            User = new UserDTO();
        }

        public PlaylistDTO(Playlist playlist) 
        { 
            Id = playlist.Id;
            Name = playlist.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public UserDTO User { get; set; }

    }
}
