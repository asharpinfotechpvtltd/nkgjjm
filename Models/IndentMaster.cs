using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class IndentMaster
    {
        [Key]
        public int Id { get; set; }
        public string Jobworkid { get; set; }
        public DateTime Date { get; set; }
        public string WarehouseInchargeStatus { get; set; }
        public string Hostatus { get; set; }
        public int VillageInchargeEmail { get; set; }
        public int WareHouseid { get; set; }
    }
}
