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
            new Employee {EmployeeId=101, EmpName="Rohan", Salary=50000, ImageUrl="/Images/mvcimg1.jpg",DeptId=30},
            new Employee {EmployeeId=102, EmpName="Arushi", Salary=60000, ImageUrl="/Images/mvcimg2.jpg",DeptId=20},
            new Employee {EmployeeId=103, EmpName="Angela", Salary=85000, ImageUrl="/Images/mvcimg3.jpg",DeptId=10},
            new Employee {EmployeeId=104, EmpName="Rohit", Salary=55000, ImageUrl="/Images/mvcimg4.jpg",DeptId=10},
        };
        List<Dept> deptlist = new List<Dept>()
        {
            new Dept{DeptId = 10, DeptName="Sales"},
            new Dept{DeptId = 20, DeptName="HR"},
            new Dept{DeptId = 30, DeptName="Software"},
        };

        public IActionResult collectionOfDepts()
        {
            return View(deptlist);
        }
        public IActionResult EmpsInDept(int deptid)
        {
            var emplistinDept = emplist.Where(x => x.DeptId == deptid).ToList();
            return View(emplistinDept);      
        }

        public IActionResult mixedObjectPassing(int empid)
        {
            var query1 = deptlist.ToList();
            Employee emp = emplist.
                Where(x => x.EmployeeId == empid).FirstOrDefault();
            var query2 = emp;
            EmpDeptViewModel obj = new EmpDeptViewModel()
            {
                deptlist=query1,
                emp=query2,
                date=DateTime.Now
            };
            return View(obj);
        }
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
        public IActionResult Details(int id)
        {
            var emp = emplist.FirstOrDefault(e => e.EmployeeId == id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
        public IActionResult searchEmp(int id) //when you will browse it write http://localhost:5000/Home/searchEmp?id=102
        {
            Employee emp = (from e in emplist where(e.EmployeeId == id) select e).FirstOrDefault();
            return View(emp);
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
