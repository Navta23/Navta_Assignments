using Microsoft.AspNetCore.Mvc;
using StateManagementInAsp.netcore.Models;

namespace StateManagementInAsp.netcore.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var cookieOptions = new CookieOptions
                //{
                //    Expires = DateTime.Now.AddMinutes(1) //set cookie

                //};
                //Response.Cookies.Append("UserName", model.Username, cookieOptions);
                //return RedirectToAction("Welcome");

                //instead of using cookies we will use session
                HttpContext.Session.SetString("UserName", model.Username);
                return RedirectToAction("Welcome");
            }
            return View(model);
        }

        public IActionResult Welcome()
        {
            //if (Request.Cookies.ContainsKey("UserName"))
            //{
            //    string username = Request.Cookies["UserName"];
            //    ViewBag.UserName = username;
            //}
            var username = HttpContext.Session.GetString("UserName");
            if (!String.IsNullOrEmpty(username))
            {
                ViewBag.UserName = username;
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            //Response.Cookies.Delete("UserName");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login");
        }
    }
}
