using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Contractors
    {
        [Key]
        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public Int64 ContractorContact { get; set; }
        public string CompanyName { get; set; }
        public DateTime AddedDate{ get; set; }
        public string Status{ get; set; }
    }
}
