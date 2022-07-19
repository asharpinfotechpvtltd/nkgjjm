using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class PoMaster
    {
        [Key]
        public int Id { get; set; }
        public string Pono { get; set; }
        public DateTime date { get; set; }
        public string Buyer { get; set; }       
        public int Supplier { get; set; }
        

    }
}
