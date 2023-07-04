using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public class GlobalSettings
    {
        public GlobalSettings()
        {
            BaseApiEndpoint = "https://books-web-api.azurewebsites.net/";
        }

        public static GlobalSettings Instance { get; } = new GlobalSettings();
        public string BaseApiEndpoint { get; set; }
    }
}
