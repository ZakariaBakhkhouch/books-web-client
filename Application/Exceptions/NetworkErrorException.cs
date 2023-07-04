using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class NetworkErrorException : Exception
    {
        public NetworkErrorException()
        {
            Message = "Please check your internet connection.";
        }

        public NetworkErrorException(string message) : base(message)
        {
            Message = message;
        }

        public new string Message { get; set; }
    }
}
