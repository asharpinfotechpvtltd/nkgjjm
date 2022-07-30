using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class VillageIncharges
    {
        [Key]
        public int Id { get; set; }       
        public int WarehouseId { get; set; }
        public int VillageInchargeId { get; set; }
        
       

    }
}
