using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class VillageWithVillageIncharge
    {
        [Key]
        public int id { get; set; }
        public int VillageInchargeId { get; set; }
        public Int64 VillageCode { get; set; }
    }
}
