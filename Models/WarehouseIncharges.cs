using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class WarehouseIncharges
    {
        [Key]
        public int id { get; set; }       
        public string Employeeid { get; set; }
        public int WareHouseid { get; set; }
    }
}
