using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class ItemMaster
    {
        
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int UnitType { get; set; }
        public string? HsnCode { get; set; }
        public string? Description { get; set; }
        public DateTime AddedDate { get; set; }
        public double Gst { get; set; }
        public double CGst { get; set; }
        public double SGst { get; set; }
        public double IGst { get; set; }
        [Key]
        public Int64 ItemCode { get; set; }
        public double? OpeningStock { get; set; }
    }
}
