using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Indent
    {
        [Key]
        public int Id { get; set; }
        public string Jobworkid { get; set; }
        public string Status { get; set; }
        public Int64 ItemCode { get; set; }
        public Int64 IndentMasterid { get; set; }
        public double Demand { get; set; }
    }
}
