using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        public string WarehouseName { get; set; }      
        public int District { get; set; }
       
    }
}
