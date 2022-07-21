using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class IndentMaster
    {
        [Key]
        public int Id { get; set; }
        public string Jobworkid { get; set; }
        public DateTime Date { get; set; }
        public bool WarehouseInchargeStatus { get; set; }
        public bool SiteInchargeStatus { get; set; }
        public bool Hostatus { get; set; }
        public string VillageInchargeEmail { get; set; }
        public int WareHouseid { get; set; }
    }
}
