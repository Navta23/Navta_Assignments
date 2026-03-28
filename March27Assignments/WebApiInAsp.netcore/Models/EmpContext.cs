using Microsoft.EntityFrameworkCore;
namespace WebApiInAsp.netcore.Models
{
    public class EmpContext: DbContext
    {
        public EmpContext(DbContextOptions<EmpContext>
            dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Employee> Employees { get; set; }

    }
}
