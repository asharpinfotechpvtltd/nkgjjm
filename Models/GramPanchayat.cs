using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class GramPanchayats
    {
        [Key]
        public int Id { get; set; }
        public string GramPanchayat { get; set; }
        public string Block { get; set; }
    }
}
