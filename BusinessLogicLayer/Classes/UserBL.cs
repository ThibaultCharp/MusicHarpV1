using BusinessLogicLayer.EntityDTO_s;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.Repo_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Classes
{
    public class UserBL
    {
        private readonly IUserRepository repository;

        public UserBL(IUserRepository loginRepository)
        {
            this.repository = loginRepository;
        }

        public (bool, int) Login(string username, string password)
        {
            return repository.Login(username, password);
        }

        public UserDTO SignUp(User user)
        {
            UserDTO userDTO = new UserDTO(user);
            repository.SignUp(userDTO);
            return userDTO;
        }
    }
}
