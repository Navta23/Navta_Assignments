namespace MVCExample.Models
{
    public class Employee
    {
        public int EmployeeId {  get; set; }
        public string? EmpName { get; set; } //Nullable Reference
        public int Salary { get; set; }
        public string? ImageUrl { get; set; }
        //FK + reference
        public int DeptId { get; set; }
        public Dept? Dept { get; set; }
    }
}
