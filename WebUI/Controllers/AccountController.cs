using Domain.Models.Account;
using Infrastructure.Services.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IAccountService _accountService { get; set; }
        public UserManager<ApplicationUser> _userManager { get; set; }
        public SignInManager<ApplicationUser> _signInManager { get; set; }

        public AccountController(IAccountService accountService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.LoginAsync(model);
                if (result.Item2.IsSuccess)
                    await _signInManager.SignInAsync(result.Item2.User, false);
            }
            return View();
        }
    }
}
