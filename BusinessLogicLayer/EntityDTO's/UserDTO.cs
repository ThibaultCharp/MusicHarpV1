using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Entitys;


namespace BusinessLogicLayer.EntityDTO_s
{
    public class UserDTO
    {
        public UserDTO() { }

        public UserDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Password = user.Password;
            ProfilePicture = user.ProfilePicture;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
    }
}
