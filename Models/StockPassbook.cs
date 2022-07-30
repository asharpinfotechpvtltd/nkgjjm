using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class StockPassbook
    {
        [Key]
        public int Id { get; set; }
        public int IndentMaster { get; set; }
        public Int64 ItemCode { get; set; }
        public double Inward { get; set; }
        public double Outward { get; set; }
        public Int64 Warehouseid { get; set; }
        public DateTime Date { get; set; }
        public double Freeze { get; set; }
        public double ReturnedFreshstock { get; set; }
        public double Damgedstock { get; set; }
    }
}
