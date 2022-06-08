using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Int64 ContactNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
