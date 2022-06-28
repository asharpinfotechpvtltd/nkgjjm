using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Suppliers
    {
        [Key]
        public int Id { get; set; }
        public string SupplierName { get; set; }

        public string ContactNo { get; set; }
        public string Email { get; set; }
        public int StateCode { get; set; }
        public string GstNo { get; set; }
        public string Address { get; set; }
        public DateTime AddedDate { get; set; }
        public bool Status { get; set; }
    }
}

