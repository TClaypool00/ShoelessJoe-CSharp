using Microsoft.AspNetCore.Mvc;
using ShoelessJoe.App.Models;
using ShoelessJoe.App.Models.MultiModels;
using ShoelessJoe.Core.Interfaces;

namespace ShoelessJoe.App.Controllers
{
    public class RegisterController : ControllerHelper
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;

        public RegisterController(IUserService userService, IPasswordService passwordService)
        {
            _userService = userService;
            _passwordService = passwordService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
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

            model.LoginViewModel = new LoginViewModel
            {
                Email = model.RegisterViewModel.Email
            };

            SetSuccessMessage(_userService.UserCreatedMessage);

            return IndexView(model);

        }

        private ActionResult IndexView(RegisterIndexViewModel model)
        {
            return View("Index", model);
        }
    }
}
