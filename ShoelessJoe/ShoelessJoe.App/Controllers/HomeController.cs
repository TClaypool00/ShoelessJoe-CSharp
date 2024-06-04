using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoelessJoe.App.Models;
using System.Diagnostics;
using ShoelessJoe.Core.Interfaces;

namespace ShoelessJoe.App.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShoeService _shoeService;

        public HomeController(ILogger<HomeController> logger, IShoeService shoeService)
        {
            _logger = logger;
            _shoeService = shoeService;
        }

        public async Task<ActionResult> Index()
        {
            var coreShoes = await _shoeService.GetShoesAsync();
            var shoeModels = new List<ShoeViewModel>();

            if (coreShoes.Count > 0)
            {
                for (int i = 0; i < coreShoes.Count; i++)
                {
                    shoeModels.Add(Mapper.MapShoe(coreShoes[i]));
                }
            }

            return View(shoeModels);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Register");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}