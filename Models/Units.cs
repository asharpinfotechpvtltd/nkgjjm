using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Units
    {
        [Key]
        public int UnitId { get; set; }

        public string UnitName { get; set; }
    }
}
