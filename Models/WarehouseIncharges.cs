using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class WarehouseIncharges
    {
        [Key]
        public int id { get; set; } 
        public int WareHouseid { get; set; }
        public string PersonName { get; set; }
        public Int64 MobileNo { get; set; }
        public string Emailid { get; set; }
    }
}
