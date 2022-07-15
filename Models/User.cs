using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int Designation { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}
