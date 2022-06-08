using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Districts
    {
        [Key]
        public int id { get; set; }
        public string District { get; set; }
    }
}
