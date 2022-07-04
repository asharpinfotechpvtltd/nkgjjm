using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class JobDescription
    {
        [Key]
        public int id { get; set; }
        public string JobWorkid { get; set; }
        public string Particular { get; set; }
        public string Unit { get; set; }
        public double Rate { get; set; }
    }
}
