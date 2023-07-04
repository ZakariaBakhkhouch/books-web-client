﻿using Domain.Models.Account;
using Domain.Models.Books;
using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly WebApiHelpers _webApiHelper;
        public string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ6YWthcmlhIGJha2hvdWNoIiwianRpIjoiNDU5ZThlMWUtNzdiNi00N2NkLWFhN2YtZDU2MGM0MDJhOTc3IiwiZW1haWwiOiJ6YWthYmFraG9AZ21haWwuY29tIiwidWlkIjoiMDI0MjljMWMtN2EyMS00NmVjLTkxN2EtOGE4N2U5NmUzOWUwIiwicm9sZXMiOlsiVXNlciIsIkFkbWluIl0sImV4cCI6MTY4ODI5MTc1MiwiaXNzIjoiU2VjdXJlQXBpIiwiYXVkIjoiU2VjdXJlQXBpVXNlciJ9.fTA-JJ0iQmygQlmKDoeuDY8sbJIYXxMlg53glMlvSSY";

        public AccountService(WebApiHelpers webApiHelper)
        {
            _webApiHelper = webApiHelper;
        }

        public Task<Tuple<Common.CallStatus, LoginResponse>> LoginAsync(LoginModel model)
        {
            return _webApiHelper.PostAsync<LoginResponse>("api/Account/Login", model);
        }
        
    }
}
