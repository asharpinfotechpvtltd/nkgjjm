using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class PoVehicleDetail
    {
        [Key]
        public int id { get; set; }
        public string PoNo { get; set; }
        public string ChallanNumber { get; set; }
        public DateTime Challan_InvoiceDate { get; set; }
        public string VehicleNumber { get; set; }
        public string LR_BiltyNo { get; set; }
        public string TransporterName { get; set; }
        public string SupportedDocument { get; set; }
    }
}
