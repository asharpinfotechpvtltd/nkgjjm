using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class VillageIncharges
    {
        [Key]
        public int Id { get; set; }       
        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public Int64 ContactNo { get; set; }
        public string Email { get; set; }
       

    }
}
