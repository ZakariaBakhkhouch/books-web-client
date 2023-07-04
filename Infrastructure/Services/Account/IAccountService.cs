using Domain.Models.Account;
using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Account
{
    public interface IAccountService
    {
        Task<Tuple<Common.CallStatus, LoginResponse>> LoginAsync(LoginModel model);
    }
}
