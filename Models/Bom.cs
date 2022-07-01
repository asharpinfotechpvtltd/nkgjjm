using Nkgjjm.Classes;
using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Bom
    {
        
        [Key]
        public int Id { get; set; }
        public string JobWorkId { get; set; }
        public int RawMaterialId { get; set; }
        public double Qty { get; set; }
        public double Tolerance  { get; set; }
        public double Finalqty { get; set; }
        public DateTime? AssignedDate { get; set; } 
    }
}
