using Microsoft.AspNetCore.Mvc;

namespace ShoelessJoe.App.Controllers
{
    public abstract class ControllerHelper : Controller
    {
        public int UserId
        {
            get
            {
                return int.Parse(User.Claims.FirstOrDefault(a => a.Type == "UserId").Value);
            }
        }

        protected void SetSuccessMessage(string message)
        {
            ViewBag.Success = message;
        }

        protected void SetErrorMessage(string message)
        {
            ViewBag.Error = message;
        }

        protected void SetTempSuccessMessage(string message)
        {
            TempData["Success"] = message;
        }

        protected void GetTempSuccessMessage()
        {
            if (TempData["Success"] is not null)
            {
                ViewBag.Success = TempData["Success"];
            }
        }
    }
}
