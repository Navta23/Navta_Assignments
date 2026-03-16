namespace MVCExample.Models
{
    public class Dept
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        //Master-side collection
        public List<Employee> Employees { get; set; } = new List<Employee>();


    }
}
