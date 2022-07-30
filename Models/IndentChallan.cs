using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class IndentChallan
    {
        [Key]
        public int Id { get; set; }
        public string Jobworkid { get; set; }
        public int IndentMasterId { get; set; }
        public string Hofile { get; set; }
        public string? VillageinchargeFile { get; set; }
    }
}
