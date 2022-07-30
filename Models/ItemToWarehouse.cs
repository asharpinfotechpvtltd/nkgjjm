using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class ItemToWarehouse
    {
        [Key]
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public Int64 ItemId { get; set; }
        public double Qty { get; set; }
        public DateTime Date { get; set; }
    }
}
