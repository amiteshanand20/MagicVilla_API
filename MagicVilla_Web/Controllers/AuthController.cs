using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            APIResponse result = await _authService.LoginAsync<APIResponse>(login);
            if(result != null && result.IsSuccess)
            {
                LoginResponseDTO  model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(result.Result));
                HttpContext.Session.SetString(SD.SessionToken,model.Token);
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", result.ErrorMessages.FirstOrDefault());
                return View(login);
            }
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
           await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken, "");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
