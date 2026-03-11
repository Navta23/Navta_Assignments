using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCExample.Models;

namespace MVCExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public string sampledemo1()
        {
            return "Ravi";
        }
        public string sampledemo2(int age, string name)
        {
            return "The name " + name + " and having age " + age;
        }

        /*right click on sampledemo3 -> add view -> add razor view 
         * -> you will get a sampledemo3cs.html file in Home folder */
        public IActionResult sampledemo3()
        {
            int age = 21;
            string name = "navya";
            ViewBag.Name = name;
            ViewBag.Age = age;
            ViewData["Message"] = "Welcome to Asp.net core learning.";
            ViewData["year"] = DateTime.Now.Year;
            return View();
        }

        Employee obj = new Employee()
        {
            EmployeeId = 101,
            EmpName = "Navya",
            Salary = 50000
        };
        List<Employee> emplist = new List<Employee>()
        {
            new Employee {EmployeeId=101, EmpName="Rohan", Salary=50000, ImageUrl="/Images/mvcimg1.jpg"},
            new Employee {EmployeeId=101, EmpName="Arushi", Salary=60000, ImageUrl="/Images/mvcimg2.jpg"},
            new Employee {EmployeeId=101, EmpName="Angela", Salary=85000, ImageUrl="/Images/mvcimg3.jpg"},
            new Employee {EmployeeId=101, EmpName="Rohit", Salary=55000, ImageUrl="/Images/mvcimg4.jpg"},
        };
        public IActionResult collectionOfObjectsPassing()
        {
            return View(emplist);
        }
        public IActionResult singleObjectPassing()
        {
            return View(obj);
        }
        public IActionResult Display()
        {
            return View();
        }
        public IActionResult Index()
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
