using Microsoft.EntityFrameworkCore;
using WebApiInAsp.netcore.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApiInAsp.netcore
{
    public class EmployeeService : IEmployee
    {
        private readonly EmpContext _context;
        private readonly IWebHostEnvironment _env;
        public EmployeeService(EmpContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<Employee> AddEmployeeAsync(Employee employee, IFormFile image)
        {
            if(image!=null && image.Length > 0)
            {
                var imageName = Guid.NewGuid().ToString()+Path.GetExtension(image.FileName); //get the extension like png, jpeg
                var imagePath = Path.Combine(_env.WebRootPath, "uploads", imageName); // uploads is a folder
                Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);
                using var stream = new FileStream(imagePath, FileMode.Create); // using is used so that we do not need to close the file stream like fs.Close()
                await image.CopyToAsync(stream);
                employee.ImagePath = "/uploads/"+ imageName;
            }
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee == null)
            {
                return null;
            }
            DeleteImageFile(employee.ImagePath);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            employee.ImagePath = null;
            return employee;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {

            return await _context.Employees.
                 Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        private void DeleteImageFile(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            var fullPath = Path.Combine
                (_env.WebRootPath, imagePath.TrimStart('/').Replace
                ('/', Path.DirectorySeparatorChar));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        private string SaveImageToUploads(IFormFile image)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fullPath = Path.Combine(uploadPath, imageName);
            using var stream = new FileStream(fullPath, FileMode.Create);
            image.CopyTo(stream);

            return "/uploads/" + imageName;
        }
        public async Task<Employee?> UpdateEmployeeAsync(Employee employee, IFormFile? image) // ? -> you can either update the image or not  your will
        {
            var existing = await _context.Employees.FindAsync(employee.Id);
            if (existing == null) return null;

            existing.FirstName = employee.FirstName;
            existing.LastName = employee.LastName;
            existing.Email = employee.Email;
            existing.Age = employee.Age;

            if (image != null && image.Length > 0)
            {
                DeleteImageFile(existing.ImagePath);
                existing.ImagePath = SaveImageToUploads(image);
            }


            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
