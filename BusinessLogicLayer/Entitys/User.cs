using BusinessLogicLayer.EntityDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entitys
{
    public class User
    {
        public User() { }

        public User(UserDTO userDTO) 
        {
            Id = userDTO.Id;
            Name = userDTO.Name;
            Password = userDTO.Password;
            ProfilePicture = userDTO.ProfilePicture;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
    }
}
