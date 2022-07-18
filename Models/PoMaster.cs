using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class PoMaster
    {
        [Key]
        public int Id { get; set; }
        public string Pono { get; set; }
        public DateTime date { get; set; }
        public string Buyer { get; set; }
        public string? Grn { get; set; }
        public int Supplier { get; set; }
        public string? Challan_Invoicenumber { get; set; }
        public DateTime? Challan_InvoiceDate { get; set; }
        public string? VehicleNumber { get; set; }
        public string? LR_BiltyNo { get; set; }
        public string? TransporterName { get; set; }

    }
}
