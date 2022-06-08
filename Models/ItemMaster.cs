using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class ItemMaster
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int UnitType { get; set; }
        public float Price { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
