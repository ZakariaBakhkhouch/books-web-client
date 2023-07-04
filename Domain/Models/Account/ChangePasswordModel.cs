using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Account
{
    public class ChangePasswordModel
    {
        
       [JsonProperty("email")] public string? Email { get; set; }
    }
}
