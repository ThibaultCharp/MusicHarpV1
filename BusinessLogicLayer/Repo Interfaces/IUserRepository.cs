using BusinessLogicLayer.EntityDTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repo_Interfaces
{
    public interface IUserRepository
    {
        public (bool, int, string, string) Login(string username, string password);
        public void SignUp(UserDTO user);
    }
}
