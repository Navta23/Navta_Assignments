using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApiInAsp.netcore.Models;

namespace WebApiInAsp.netcore.Controllers
{
    [Route("api/[controller]")] //url 
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly IEmployee _employeeService;
        public EmpController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAll(int page=1, int pageSize=5)
        {
            return Ok(await _employeeService.GetAllEmployeesAsync(page, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if(employee == null)
                return NotFound("Employee not found");
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> Add([FromForm]Employee emp, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var added = await _employeeService.AddEmployeeAsync(emp, image);
            return Ok(added);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<List<Employee>>> Update(int id, [FromForm]EmployeeUpdateDto employeeDto, IFormFile? image)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // map dto to entity
            var employee = new Employee
            {
                Id = id, // take from route only
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Age = employeeDto.Age,
                ImagePath = employeeDto.ImagePath
            };

            var updated = await _employeeService.UpdateEmployeeAsync(employee, image);
            if (updated == null)
                return NotFound("Employee not found to update");

            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> Delete(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);
            if(deleted == null)
                return NotFound("Employee not found to delete");
            return Ok(deleted);
        }
    }
}
