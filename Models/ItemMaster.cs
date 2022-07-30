using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class ItemMaster
    {

        [Key]
        public Int64 ItemCode { get; set; }
        public string ItemName { get; set; }
        public int UnitType { get; set; }      
        public string? Description { get; set; }
        public DateTime AddedDate { get; set; }
       

       
       
    }
}
