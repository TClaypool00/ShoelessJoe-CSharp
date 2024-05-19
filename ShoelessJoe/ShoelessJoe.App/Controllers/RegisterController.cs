using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ShoelessJoe.App.Attributes;
using ShoelessJoe.App.Models;
using ShoelessJoe.App.Models.MultiModels;
using ShoelessJoe.Core.Interfaces;
using System.Security.Claims;

namespace ShoelessJoe.App.Controllers
{
    [Unauthorized]
    public class RegisterController : ControllerHelper
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IJWTService _jwtService;

        public RegisterController(IUserService userService, IPasswordService passwordService, IJWTService jwtService)
        {
            _userService = userService;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return IndexView(model);
            }

            if (await _userService.UserExistsByEmailAsync(model.RegisterViewModel.Email))
            {
                SetErrorMessage(_userService.EmailAlreadyExistsMessage(model.RegisterViewModel.Email));

                return IndexView(model);
            }

            if (await _userService.UserExistsByPhoneNumbAsync(model.RegisterViewModel.PhoneNumb))
            {
                SetErrorMessage(_userService.PhoneNumbExistsMessage(model.RegisterViewModel.PhoneNumb));

                return IndexView(model);
            }

            if (!_passwordService.PasswordMeetsRequirements(model.RegisterViewModel.Password))
            {
                SetErrorMessage(_passwordService.PasswordDoesNotMeetRequirementsMessage);

                return IndexView(model);
            }

            var coreUser = Mapper.MapUser(model.RegisterViewModel);

            coreUser.Password = _passwordService.HashPassword(model.RegisterViewModel.Password);

            await _userService.AddUserAsync(coreUser);

            ModelState.Clear();

            model.LoginViewModel = new LoginViewModel
            {
                Email = model.RegisterViewModel.Email
            };

            SetSuccessMessage(_userService.UserCreatedMessage);

            return IndexView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(RegisterIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return IndexView(model);
            }

            if (!await _userService.UserExistsByEmailAsync(model.LoginViewModel.Email))
            {
                SetErrorMessage(_userService.IncorrectEmailMessage);

                return IndexView(model);
            }

            var user = await _userService.GetUserByEmailAsync(model.LoginViewModel.Email);
            var claims = _jwtService.GetClaims(user);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperies = new AuthenticationProperties
            {
                AllowRefresh = true,
                IssuedUtc = DateTimeOffset.UtcNow,
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperies);

            return RedirectToAction("Index", "Home");
        }

        private ActionResult IndexView(RegisterIndexViewModel model)
        {
            return View("Index", model);
        }
    }
}
