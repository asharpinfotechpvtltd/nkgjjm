using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Pochild
    {
        [Key]
        public int Id { get; set; }
        public string Pono { get; set; }
        public Int64 Itemid { get; set; }
        public double Qty { get; set; }
       
        public string WarehouseName { get; set; }

    }
}
