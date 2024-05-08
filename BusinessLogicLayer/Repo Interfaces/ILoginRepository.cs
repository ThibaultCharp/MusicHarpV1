using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repo_Interfaces
{
    public interface ILoginRepository
    {
        public (bool, int) Login(string username, string password);
    }
}
