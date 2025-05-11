using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
                _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            LoginRequestDTO login = new LoginRequestDTO();
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO login)
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            RegistrationRequestDTO login = new ();
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequestDTO registration)
        {
           APIResponse result = await _authService.RegisterAsync<APIResponse>(registration);
            if (result != null && result.IsSuccess) 
            {
                RedirectToAction("Login");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            return View();
        } 
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
