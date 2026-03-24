using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StateManagementInAsp.netcore.Models;

namespace StateManagementInAsp.netcore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private int a = 0;

        [HttpPost]
        public IActionResult SetA()
        {
            a = 10;
            ViewBag.AValue = "A has been set to 10";
            return View("Index");
        }

        [HttpPost]
        public IActionResult GetA()
        {
            ViewBag.AValue = $"A is currently : {a}";
            return View("Index");
        }
        public IActionResult Index()
        {
            TempData["myKey"] = "Data from Index method";
            return View();
        }
        /*now let us go with temp data demo now which is again server side state management .

Now remember that view bag and view data etc are not doing statemanagement they are for one single request and response 

so make that thing clear 

so for one subsquent request i need tempdata here when i want to send data from one action to another action method and it can be of different controller as well okay ..

tempdata internally use session to store data .

keep is used to send or to save data it wont read so if  want both means i have to use peek
*/
        public IActionResult Index2()
        {
            //ViewBag.MyKey = TempData["myKey"];
            //TempData.Keep("myKey"); //if comment it out then it will not pass to index 3
            HttpContext.Session.Remove("myKey");
            TempData.Peek("myKey");
            return View();
        }
        public IActionResult Index3()
        {
            TempData.Peek("myKey");
            ViewBag.MyKey = TempData["myKey"];
            return View();
        }

        public IActionResult Index4()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
