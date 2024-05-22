using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ErrorHandling
{
    public class DatabaseErrorExeption : Exception
    {
        public DatabaseErrorExeption(string message, Exception exception) { }
    }
}
