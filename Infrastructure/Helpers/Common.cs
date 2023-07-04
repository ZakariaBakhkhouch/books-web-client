using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public static class Common
    {
        public enum CallStatus
        {
            None,
            Success,
            Error,
            Exception,
            NotFound,
            Unauthorized
        }
    }
}
