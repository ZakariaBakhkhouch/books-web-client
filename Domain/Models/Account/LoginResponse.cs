using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Account
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? Email { get; set; }
        public DateTime Time { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public List<string>? Roles { get; set; }
        public string? Data { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
