using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Challan
    {
        [Key]
        public int id { get; set; }
        public string Pono { get; set; }
        public string ChallanName { get; set; }
    }
}
