using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ErrorHandling
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}