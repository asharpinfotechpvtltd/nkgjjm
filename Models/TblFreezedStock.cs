using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class TblFreezedStock
    {
        [Key]
        public int Id { get; set; }
        public Int64 ItemCode { get; set; }
        public int WarehouseId { get; set; }
        public double Qty { get; set; }
        public string JobWorkId { get; set; }
        public int IndentMasterId { get; set; }
        public DateTime AddedDate { get; set; }

    }
}
