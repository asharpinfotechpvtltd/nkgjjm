using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class WarehouseIncharges
    {
        [Key]
        public int id { get; set; } 
        public int WareHouseid { get; set; }
        public int UserId { get; set; }
    }
}
