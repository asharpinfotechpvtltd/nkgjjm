using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Designation
    {
        [Key]
        public int Id { get; set; } 
        public string DesignationName { get; set; }
    }
}
