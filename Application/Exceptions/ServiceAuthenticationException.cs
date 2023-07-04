using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ServiceAuthenticationException : Exception
    {
        public ServiceAuthenticationException()
        {
            Message = "Please sign in to continue.";
        }

        public ServiceAuthenticationException(string message) : base(message)
        {
            Message = message;
        }

        public new string Message { get; set; }
    }
}
