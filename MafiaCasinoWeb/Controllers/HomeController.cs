using BusinessLogic.Services;
using DAL.Models;
using DAL.Models.VM;
using DAL.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MafiaCasinoWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
	{
        private readonly UserService _userService;

        public HomeController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
		public IActionResult SignIn(string ReturnUrl)
		{
			var user = HttpContext.User.Identity.IsAuthenticated;
			if (user)
			{
				return RedirectToAction("Index");
			}
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginUserVM model, string ReturnUrl)
        {
            var valdator = new LoginUserValidation();
            var validationresult = await valdator.ValidateAsync(model);
            if (validationresult.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result.Success)
                {
                    if(ReturnUrl == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return Redirect(ReturnUrl);
                }
                ViewBag.AuthError = result.Message;
                return View(model);
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterUserVM model)
        {
            //var validator = new RegisterUserValidation();
            //var validationresult = await validator.ValidateAsync(model);
            //if (validationresult.IsValid)
            //{
            //    var result = await _userService.RegisterUserAsync(model);
            //    if (result.Success)
            //    {
            //        return RedirectToAction("SignIn", "Admin");
            //    }

            //    ViewBag.AuthError = result.Message;
            //    return View(model);
            //}
            //ViewBag.AuthError = validationresult.Errors.First();
            //return View(model);

            var result = await _userService.RegisterUserAsync(model);
            if (result.Success)
            {
                return RedirectToAction(nameof(SignIn));
            }

            ViewBag.AuthError = result.Message;
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            var result = await _userService.LogoutUserAsync();
            if (result.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Admin");
        }


		public async Task<IActionResult> Profile()
		{

			var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _userService.GetUserProfileAsync(userId);
			if (result.Success)
			{
				return View(result.Payload);
			}
			return RedirectToAction("Index", "Home");
		}
	}
}
