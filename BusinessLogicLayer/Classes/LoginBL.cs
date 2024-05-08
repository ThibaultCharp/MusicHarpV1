using BusinessLogicLayer.Repo_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Classes
{
    public class LoginBL
    {
        private readonly ILoginRepository loginRepository;

        public LoginBL(ILoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        public (bool, int) Login(string username, string password)
        {
            return loginRepository.Login(username, password);
        }
    }
}
