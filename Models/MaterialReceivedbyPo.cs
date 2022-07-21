using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class MaterialReceivedbyPo
    {
        [Key]
        public int Id { get; set; }
        public string PoNo { get; set; }
        public Int64 ItemId { get; set; }
        public double RcvdQty { get; set; }
        public double Challanqty { get; set; }
        public string Challan_Invoicenumber { get; set; }
        public int Warehouse { get; set; }


    }
}
