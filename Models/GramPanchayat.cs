using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class GramPanchayats
    {
        [Key]
        public int Id { get; set; }
        public int? District { get; set; }
        public int? Block { get; set; }
        public string? GramPanchayat { get; set; }
       
    }
}
