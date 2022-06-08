using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        public string WarehouseName { get; set; }
        public string Location { get; set; }
        public int State { get; set; }
        public int City { get; set; }
    }
}
