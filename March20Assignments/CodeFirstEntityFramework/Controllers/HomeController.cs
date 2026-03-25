using System.Diagnostics;
using CodeFirstEntityFramework.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstEntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EventContext _context;
        public HomeController(ILogger<HomeController> logger, EventContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult dislayemp()
        {

            var employeees = _context.employees.ToList();
            return View(employeees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Returns the Create.cshtml view
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("dislayemp"); // Redirect to a list or confirmation page
            }

            return View(employee); // Return view with validation errors
        }
        public IActionResult Details(int id)
        {
            var employee = _context.employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _context.employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(employee);
                _context.SaveChanges();
                return RedirectToAction("displayemp");
            }

            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _context.employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }


        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.employees.Find(id);
            if (employee != null)
            {
                _context.employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("dislayemp");
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
